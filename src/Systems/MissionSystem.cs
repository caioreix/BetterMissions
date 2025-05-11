using BetterMissions.Common;
using Utils.Database;
using Utils.Logger;
using Utils.VRising.Entities;

namespace BetterMissions.Systems;

public static class Mission {
    public static void Setup() {
        DB.AddCleanActions();
    }

    // ApplyModifiers will return true if the modifiers are applied to the ServantMissionSetting, else return false
    public static bool ApplyModifiers() {
        var missionSettingBuffer = ServantMissionSetting.GetBuffer();
        if (!missionSettingBuffer.IsCreated) {
            return false;
        }

        for (int i = 0; i < missionSettingBuffer.Length; i++) {
            Log.Debug($"RaidStability {i}: {missionSettingBuffer[i].RaidStability}"); // TODO: remove
            Log.Debug($"SuccessRateBonus {i}: {missionSettingBuffer[i].SuccessRateBonus}"); // TODO: remove
            Log.Debug($"MissionLength {i}: {missionSettingBuffer[i].MissionLength}"); // TODO: remove
            Log.Debug($"InjuryChance {i}: {missionSettingBuffer[i].InjuryChance}"); // TODO: remove
            Log.Debug($"LootFactor {i}: {missionSettingBuffer[i].LootFactor}"); // TODO: remove

            var missionSetting = missionSettingBuffer[i];
            var key = getMissionSettingsKey(missionSetting, i, missionSettingBuffer.Length);
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
        return true;
    }

    private static string getMissionSettingsKey(ProjectM.ServantMissionSetting missionSetting, int index = -1, int size = -1) {
        if (index == -1 || size != 5) {
            return missionSetting.MissionLength switch {
                Constants.Reckless1.MissionLength => Constants.Reckless1.Name,
                Constants.Reckless2.MissionLength => Constants.Reckless2.Name,
                Constants.Normal1.MissionLength => Constants.Normal1.Name,
                Constants.Prepared1.MissionLength => Constants.Prepared1.Name,
                Constants.Prepared2.MissionLength => Constants.Prepared2.Name,
                _ => "",
            };
        }

        return index switch {
            Constants.Reckless1.Index => Constants.Reckless1.Name,
            Constants.Reckless2.Index => Constants.Reckless2.Name,
            Constants.Normal1.Index => Constants.Normal1.Name,
            Constants.Prepared1.Index => Constants.Prepared1.Name,
            Constants.Prepared2.Index => Constants.Prepared2.Name,
            _ => "",
        };
    }
}
