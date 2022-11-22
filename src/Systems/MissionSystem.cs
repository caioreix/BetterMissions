using System;
using BetterMissions.Settings;
using Unity.Entities;
using Utils.Logger;
using Utils.Database;

// Alias
using ASM = Utils.VRising.Entities.ActiveServantMission;

namespace BetterMissions.Systems;

public static class Mission {
    public static void Setup() {
        DB.AddCleanActions(
            () => GarbageCollector(Wetstone.API.VWorld.Server.EntityManager),
            () => SyncDatabaseMission(Wetstone.API.VWorld.Server.EntityManager)
        );
    }

    // ReduceAllNewMissionsTimeProgress based on passed values.
    public static void ReduceAllNewMissionsTimeProgress(EntityManager em) {
        var missions = ASM.GetAllBuffers(em);
        foreach (var missionBuffer in missions) {
            for (int i = 0; i < missionBuffer.Length; i++) {
                var mission = missionBuffer[i];
                var key = ASM.GetMissionUID(mission);
                var modifier = Settings.ENV.MissionReduceRate.Value;

                if (BetterMissions.Database.Mission.Progress.TryGetValue(key, out BetterMissions.Database.Mission.ProgressStruct missionProgress)) {
                    handleExistingMission(ref mission, key, missionProgress);
                } else {
                    reduceMissionProgress(ref mission, key, modifier);
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

    public static void SyncDatabaseMission(EntityManager em) {
        var missions = ASM.GetAllBuffers(em);

        foreach (var missionBuffer in missions) {
            for (int i = 0; i < missionBuffer.Length; i++) {
                var mission = missionBuffer[i];
                var key = ASM.GetMissionUID(mission);
                var newEndTimestamp = ASM.GetMissionLengthTimestamp(mission);

                BetterMissions.Database.Mission.Progress.AddOrUpdate(
                    key,
                    new BetterMissions.Database.Mission.ProgressStruct() {
                        EndTimestamp = newEndTimestamp,
                    },
                    (_, oldValue) => new BetterMissions.Database.Mission.ProgressStruct() {
                        EndTimestamp = newEndTimestamp,
                        Modifier = oldValue.Modifier,
                        Synced = oldValue.Synced,
                    }
                );
                Log.Trace($"Mission sync updated: \"{key}\": {newEndTimestamp}");
            }
        }
    }

    // Helpers

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

    private static void reduceMissionProgress(ref ProjectM.ActiveServantMission mission, string key, float reduction) {
        var newMissionLength = ASM.GetMissionLength(mission) / reduction;
        ASM.SetMissionLength(ref mission, newMissionLength);

        var newEndTimestamp = ASM.GetMissionLengthTimestamp(mission);
        BetterMissions.Database.Mission.Progress.TryAdd(
            key,
            new BetterMissions.Database.Mission.ProgressStruct() {
                EndTimestamp = newEndTimestamp,
                Modifier = reduction,
                Synced = true,
            }
        );
        Log.Trace($"Mission added: \"{key}\": {newEndTimestamp}");
    }
}
