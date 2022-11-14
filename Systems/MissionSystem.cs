using System;
using Unity.Entities;
using ProjectM;
using Settings;
using Entities;
using Logger;
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
            var key = missionProgress.Key;
            if (!existMissionKey(em, missionEntities, key)) {
                Database.Mission.Progress.TryRemove(key, out _);
                Log.Trace($"Garbage mission remove: {key}");
            }
        }
    }

    private static bool existMissionKey(EntityManager em, NativeArray<Entity> missionEntities, string key) {
        foreach (var missionEntity in missionEntities) {
            var missionBuffer = em.GetBuffer<ActiveServantMission>(missionEntity);
            foreach (var mission in missionBuffer) {
                if (getMissionKey(mission) == key) {
                    return true;
                }
            }
        }
        return false;
    }

    private static string getMissionKey(ActiveServantMission mission) {
        return mission.MissionID.ToString();
    }

    private static void reduceNewMissionsTimeProgress(EntityManager em, Entity missionEntity, float reduction) {
        var missionBuffer = em.GetBuffer<ActiveServantMission>(missionEntity);

        for (int i = 0; i < missionBuffer.Length; i++) {
            var mission = missionBuffer[i];
            var key = getMissionKey(mission);


            if (missionAlreadyFinished(ref mission, key)) {
                missionBuffer[i] = mission;
                continue;
            }

            reduceMissionProgress(ref mission, key, reduction);
            missionBuffer[i] = mission;
        }
    }

    private static bool missionAlreadyFinished(ref ActiveServantMission mission, string key) {
        var currentTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();

        if (Database.Mission.Progress.TryGetValue(key, out long timestamp)) {
            if (Env.OfflineMissionProgress.Value && currentTimestamp >= timestamp) {
                setMissionLength(ref mission, 0);

                Database.Mission.Progress.TryRemove(key, out _);
                Log.Trace($"Finished mission remove: {key}");
            }
            return true;
        }
        return false;
    }

    private static float getMissionLength(ActiveServantMission mission) {
        return mission.MissionLength;
    }

    private static void setMissionLength(ref ActiveServantMission mission, float value) {
        mission.MissionLength = value;
    }

    private static void reduceMissionProgress(ref ActiveServantMission mission, string key, float reduction) {
        var newMissionLength = getMissionLength(mission) / reduction;
        setMissionLength(ref mission, newMissionLength);

        var newEndTimestamp = DateTimeOffset.Now.AddSeconds(getMissionLength(mission)).ToUnixTimeSeconds();
        Database.Mission.Progress.TryAdd(
            key,
            newEndTimestamp
        );
        Log.Trace($"New mission added: {key}: {newEndTimestamp}");
    }
}
