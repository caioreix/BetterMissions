using System;
using Unity.Entities;
using ProjectM;
using Settings;
using Entities;
using Logger;
using System.Collections.Generic;
using Unity.Collections;

namespace Systems;

public static class Mission {
    // ReduceAllNewMissionsTimeProgress based on passed values
    public static void ReduceAllNewMissionsTimeProgress(EntityManager em, float reduction) {
        var missionEntities = ServantMission.GetEntities(em);

        // Just garbage unused data if the entities are loaded
        if (missionEntities.Length > 0) {
            garbageCollector(em, missionEntities);
        }

        foreach (var missionEntity in missionEntities) {
            reduceNewMissionsTimeProgress(em, missionEntity, reduction);
        }
    }

    private static void garbageCollector(EntityManager em, NativeArray<Entity> missionEntities) {
        foreach (var missionProgress in Database.Mission.Progress) {
            if (!existMissionKey(em, missionEntities, missionProgress)) {

                Database.Mission.Progress.TryRemove(missionProgress.Key, out _);
                Log.Trace($"Garbage mission remove: {missionProgress.Key}");
            }
        }
    }

    private static bool existMissionKey(EntityManager em, NativeArray<Entity> missionEntities, KeyValuePair<string, long> missionProgress) {
        foreach (var missionEntity in missionEntities) {
            var missionBuffer = em.GetBuffer<ActiveServantMission>(missionEntity);
            for (int i = 0; i < missionBuffer.Length; i++) {
                if (missionBuffer[i].MissionID.ToString() == missionProgress.Key) {
                    return true;
                }
            }
        }
        return false;
    }

    private static void reduceNewMissionsTimeProgress(EntityManager em, Entity missionEntity, float reduction) {
        var missionBuffer = em.GetBuffer<ActiveServantMission>(missionEntity);

        for (int i = 0; i < missionBuffer.Length; i++) {
            var mission = missionBuffer[i];
            var currentTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();

            if (missionAlreadyFinished(ref mission, currentTimestamp)) {
                missionBuffer[i] = mission;
                continue;
            }

            reduceMissionProgress(ref mission, reduction);
            missionBuffer[i] = mission;
        }
    }

    private static bool missionAlreadyFinished(ref ActiveServantMission mission, long currentTimestamp) {
        if (Database.Mission.Progress.TryGetValue(mission.MissionID.ToString(), out long timestamp)) {
            if (Env.OfflineMissionProgress.Value && currentTimestamp >= timestamp) {
                mission.MissionLength = 0;

                Database.Mission.Progress.TryRemove(mission.MissionID.ToString(), out _);
                Log.Trace($"Finished mission remove: {mission.MissionID.ToString()}");
            }
            return true;
        }
        return false;
    }

    private static void reduceMissionProgress(ref ActiveServantMission mission, float reduction) {
        mission.MissionLength /= reduction;

        Database.Mission.Progress.TryAdd(
            mission.MissionID.ToString(),
            DateTimeOffset.Now.AddSeconds(mission.MissionLength).ToUnixTimeSeconds()
        );
        Log.Trace($"New mission added: {mission.MissionID.ToString()}: {DateTimeOffset.Now.AddSeconds(mission.MissionLength).ToUnixTimeSeconds()}");
    }
}
