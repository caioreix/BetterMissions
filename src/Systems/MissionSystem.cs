using System;
using BetterMissions.Settings;
using Utils.Logger;
using Utils.Database;
using Utils.VRising.Entities;

// Alias
using ASM = Utils.VRising.Entities.ActiveServantMission;

namespace BetterMissions.Systems;

public static class Mission {
    public static void Setup() {
        DB.AddCleanActions(
            () => GarbageCollector(),
            () => SyncDatabaseProgress()
        );
    }

    public static void ApplyModifiers() {
        var missionSettingBuffer = ServantMissionSetting.GetBuffer();
        for (int i = 0; i < missionSettingBuffer.Length; i++) {
            var missionSetting = missionSettingBuffer[i];

            var key = getMissionSettingsKey(missionSetting.MissionLength);
            if (Database.Mission.Settings.TryGetValue(key, out Database.Mission.Setting ms)
            ) {
                missionSetting.MissionLength = ms.MissionLength;
                missionSetting.SuccessRateBonus = ms.SuccessRateBonus;
                missionSetting.InjuryChance = ms.InjuryChance;
                missionSetting.LootFactor = ms.LootFactor;
            } else {
                missionSetting.MissionLength /= Settings.ENV.MissionLengthModifier.Value;
                missionSetting.SuccessRateBonus /= Settings.ENV.MissionSuccessRateBonusModifier.Value;
                missionSetting.InjuryChance /= Settings.ENV.MissionInjuryChanceModifier.Value;
                missionSetting.LootFactor /= Settings.ENV.MissionLootFactorModifier.Value;
            }

            missionSettingBuffer[i] = missionSetting;
        }
        Log.Trace("Mission modifiers applied");
    }

    // ReduceAllNewMissionsTimeProgress based on passed values.
    public static void ProgressPersistence() {
        var missions = ASM.GetAllBuffers();
        foreach (var missionBuffer in missions) {
            for (int i = 0; i < missionBuffer.Length; i++) {
                var mission = missionBuffer[i];
                var key = ASM.GetMissionUID(mission);

                if (BetterMissions.Database.Mission.Progresses.TryGetValue(key, out BetterMissions.Database.Mission.Progress missionProgress)) {
                    handleExistingMission(ref mission, key, missionProgress);
                } else {
                    saveMissionProgress(ref mission, key);
                }

                missionBuffer[i] = mission;
            }
        }
    }

    public static void GarbageCollector() {
        var missionUIDs = ASM.GetAllBuffersMissionUIDs();

        // Just garbage unused data if the entities are loaded
        if (missionUIDs.Count == 0) {
            return;
        }

        foreach (var activeMission in BetterMissions.Database.Mission.Progresses) {
            var key = activeMission.Key;

            if (!missionUIDs.Contains(key)) {
                BetterMissions.Database.Mission.Progresses.TryRemove(key, out _);
                Log.Trace($"Mission garbage removed: \"{key}\"");
            }
        }
    }

    public static void SyncDatabaseProgress() {
        var missions = ASM.GetAllBuffers();

        foreach (var missionBuffer in missions) {
            for (int i = 0; i < missionBuffer.Length; i++) {
                var mission = missionBuffer[i];
                var key = ASM.GetMissionUID(mission);
                var newEndTimestamp = ASM.GetMissionLengthTimestamp(mission);

                BetterMissions.Database.Mission.Progresses.AddOrUpdate(
                    key,
                    new BetterMissions.Database.Mission.Progress() {
                        EndTimestamp = newEndTimestamp,
                    },
                    (_, oldValue) => new BetterMissions.Database.Mission.Progress() {
                        EndTimestamp = newEndTimestamp,
                        Synced = oldValue.Synced,
                    }
                );
                Log.Trace($"Mission sync updated: \"{key}\": {newEndTimestamp}");
            }
        }
    }

    // Helpers
    private static void handleExistingMission(ref ProjectM.ActiveServantMission mission, string key, BetterMissions.Database.Mission.Progress missionProgress) {
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

            BetterMissions.Database.Mission.Progresses.TryRemove(key, out _);
            Log.Trace($"Mission remove: \"{key}\"");
        }

        missionProgress.Synced = true;
        BetterMissions.Database.Mission.Progresses.AddOrUpdate(key, missionProgress, (_, _) => missionProgress);
        ASM.SetMissionLength(ref mission, newEndMissionLength);
        Log.Trace($"Mission progression updated: \"{key}\": {newEndMissionLength}");
    }

    private static void saveMissionProgress(ref ProjectM.ActiveServantMission mission, string key) {
        var endTimestamp = ASM.GetMissionLengthTimestamp(mission);
        BetterMissions.Database.Mission.Progresses.TryAdd(
            key,
            new BetterMissions.Database.Mission.Progress() {
                EndTimestamp = endTimestamp,
                Synced = true,
            }
        );
        Log.Trace($"Mission added: \"{key}\": {endTimestamp}");
    }

    private static string getMissionSettingsKey(float missionLength) {
        switch (missionLength) {
            case 7200:
                return "Reckless_1";
            case 14400:
                return "Reckless_2";
            case 28800:
                return "Normal_1";
            case 57600:
                return "Prepared_1";
            case 82800:
                return "Prepared_2";
            default:
                return "";
        }
    }
}
