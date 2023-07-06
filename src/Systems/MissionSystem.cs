using Utils.Logger;
using Utils.Database;
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
        return true;
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
