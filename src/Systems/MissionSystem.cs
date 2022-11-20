using System;
using System.Collections.Generic;

using Settings;

using Unity.Entities;

using Utils.Logger;

// Alias
using ASM = Utils.VRising.Entities.ActiveServantMission;

namespace Systems;

public static class Mission {
    // ReduceAllNewMissionsTimeProgress based on passed values.
    public static void ReduceAllNewMissionsTimeProgress(EntityManager em, float reduction) {
        GarbageCollector(em);

        var missions = ASM.GetAllBuffers(em);
        foreach (var missionBuffer in missions) {
            for (int i = 0; i < missionBuffer.Length; i++) {
                var mission = missionBuffer[i];
                var key = ASM.GetMissionUID(mission);

                if (Database.Mission.Progress.TryGetValue(key, out long missionTimestamp)) {
                    handleExistingMission(ref mission, key, missionTimestamp);
                } else {
                    reduceMissionProgress(ref mission, reduction);
                }

                missionBuffer[i] = mission;
            }
        }
    }

    public static void GarbageCollector(EntityManager em) {
        var missionUIDs = ASM.GetAllBuffersMissionUIDs(em);

        // Just garbage unused data if the entities are loaded
        if (missionUIDs.Count == 0) {
            return;
        }

        foreach (var activeMission in Database.Mission.Progress) {
            var key = activeMission.Key;

            if (!missionUIDs.Contains(key)) {
                Database.Mission.Progress.TryRemove(key, out _);
                Log.Trace($"Mission garbage removed: {key}");
            }
        }
    }

    private static void handleExistingMission(ref ProjectM.ActiveServantMission mission, string key, long missionTimestamp) {
        var nowTimestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        if (nowTimestamp <= missionTimestamp) {
            return; // exist but the endtime not reached.
        }

        // If offline mission progress is disabled we need update database timestamp to maintain the integrity.
        if (!ENV.OfflineMissionProgress.Value) {
            var newEndTimestamp = ASM.GetMissionLengthTimestamp(mission);
            Database.Mission.Progress.AddOrUpdate(
                key,
                newEndTimestamp,
                (_, _) => newEndTimestamp
            );
            Log.Trace($"Mission updated: {key}: {missionTimestamp} -> {newEndTimestamp}");
        }

        // Update mission progress and remove from database.
        ASM.SetMissionLength(ref mission, 0);
        if (Database.Mission.Progress.TryRemove(key, out _)) {
            Log.Trace($"Mission remove: {key}");
        }
    }

    private static void reduceMissionProgress(ref ProjectM.ActiveServantMission mission, float reduction) {
        var newMissionLength = ASM.GetMissionLength(mission) / reduction;
        ASM.SetMissionLength(ref mission, newMissionLength);

        var missionKey = ASM.GetMissionUID(mission);
        var newEndTimestamp = ASM.GetMissionLengthTimestamp(mission);
        Database.Mission.Progress.TryAdd(
            missionKey,
            newEndTimestamp
        );
        Log.Trace($"Mission added: {missionKey}: {newEndTimestamp}");
    }
}
