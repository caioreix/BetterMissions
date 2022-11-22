using System;
using BetterMissions.Settings;
using Unity.Entities;
using Utils.Logger;

// Alias
using ASM = Utils.VRising.Entities.ActiveServantMission;

namespace BetterMissions.Systems;

public static class Mission {
    // ReduceAllNewMissionsTimeProgress based on passed values.
    public static void ReduceAllNewMissionsTimeProgress(EntityManager em, float reduction) {
        var missions = ASM.GetAllBuffers(em);
        foreach (var missionBuffer in missions) {
            for (int i = 0; i < missionBuffer.Length; i++) {
                var mission = missionBuffer[i];
                var key = ASM.GetMissionUID(mission);

                if (BetterMissions.Database.Mission.Progress.TryGetValue(key, out BetterMissions.Database.Mission.ProgressStruct missionProgress)) {
                    handleExistingMission(ref mission, key, missionProgress);
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

        foreach (var activeMission in BetterMissions.Database.Mission.Progress) {
            var key = activeMission.Key;

            if (!missionUIDs.Contains(key)) {
                BetterMissions.Database.Mission.Progress.TryRemove(key, out _);
                Log.Trace($"Mission garbage removed: \"{key}\"");
            }
        }
    }

    private static void handleExistingMission(ref ProjectM.ActiveServantMission mission, string key, BetterMissions.Database.Mission.ProgressStruct missionProgress) {
        var nowTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        if (missionProgress.Synced) {
            return; // Mission already synced to the server.
        }
        if (!ENV.OfflineMissionProgress.Value) {
            return;
        }

        // To display the correct mission end time on the missions HUD.
        var newEndMissionLength = missionProgress.EndTimestamp - nowTimestamp;
        if (newEndMissionLength < 0) {
            newEndMissionLength = 0;

            BetterMissions.Database.Mission.Progress.TryRemove(key, out _);
            Log.Trace($"Mission remove: \"{key}\"");
        }

        missionProgress.Synced = true;
        BetterMissions.Database.Mission.Progress.AddOrUpdate(key, missionProgress, (_, _) => missionProgress);
        ASM.SetMissionLength(ref mission, newEndMissionLength);
        Log.Trace($"Mission progression updated: \"{key}\": {newEndMissionLength}");
    }

    private static void reduceMissionProgress(ref ProjectM.ActiveServantMission mission, float reduction) {
        var newMissionLength = ASM.GetMissionLength(mission) / reduction;
        ASM.SetMissionLength(ref mission, newMissionLength);

        var missionKey = ASM.GetMissionUID(mission);
        var newEndTimestamp = ASM.GetMissionLengthTimestamp(mission);
        BetterMissions.Database.Mission.Progress.TryAdd(
            missionKey,
            new BetterMissions.Database.Mission.ProgressStruct() {
                EndTimestamp = newEndTimestamp,
                Modifier = reduction,
                Synced = true,
            }
        );
        Log.Trace($"Mission added: {missionKey}: {newEndTimestamp}");
    }
}
