using System;
using System.Text.Json;
using BetterMissions.Common;
using Utils.Database;
using Utils.Logger;
using Utils.VRising.Entities;

namespace BetterMissions.Systems;

public class Mission {
    public static void Setup() {
        DB.AddCleanActions();
    }

    public static void UpdateUserDataUI(ProjectM.Network.User user) {
        Unity.Entities.DynamicBuffer<ProjectM.ServantMissionSetting> missionSettingBuffer = ServantMissionSetting.GetBuffer();
        if (!missionSettingBuffer.IsCreated || missionSettingBuffer.Length != 5) {
            Log.Error("ServantMissionSetting buffer is not created");
            return;
        }

        string json = JsonSerializer.Serialize(MissionSettingBufferToArray(missionSettingBuffer));
        string wrappedMessage = CustomNetwork.WrapMessage(
            CustomNetwork.MessageType.UpdateClientMissionData,
            json
        );

        CustomNetwork.SendSystemMessageToClient(
            user,
            wrappedMessage
        );
    }

    private static void MissionSettingArrayToBuffer(Models.ServantMissionSetting[] missionSettingArray, ref Unity.Entities.DynamicBuffer<ProjectM.ServantMissionSetting> missionSettingBuffer) {
        for (int i = 0; i < missionSettingArray.Length; i++) {
            var missionSetting = missionSettingBuffer[i];

            missionSetting.RaidStability = missionSettingArray[i].RaidStability;
            missionSetting.MissionLength = missionSettingArray[i].MissionLength;
            missionSetting.SuccessRateBonus = missionSettingArray[i].SuccessRateBonus;
            missionSetting.InjuryChance = missionSettingArray[i].InjuryChance;
            missionSetting.LootFactor = missionSettingArray[i].LootFactor;

            missionSettingBuffer[i] = missionSetting;
        }
    }

    public static void HandleUpdateMissionDataMessage(string message) {
        try {
            var missionSettingBuffer = ServantMissionSetting.GetBuffer();
            if (missionSettingBuffer.IsCreated) {
                MissionSettingArrayToBuffer(
                    JsonSerializer.Deserialize<Models.ServantMissionSetting[]>(message),
                    ref missionSettingBuffer
                );

                string json = JsonSerializer.Serialize(MissionSettingBufferToArray(missionSettingBuffer));
            }
        } catch (Exception e) {
            Log.Error(e);
        }
    }

    // ApplyModifiers will return true if the modifiers are applied to the ServantMissionSetting, else return false
    public static bool ApplyModifiers() {
        var missionSettingBuffer = ServantMissionSetting.GetBuffer();
        if (!missionSettingBuffer.IsCreated) {
            return false;
        }

        for (int i = 0; i < missionSettingBuffer.Length; i++) {
            var missionSetting = missionSettingBuffer[i];
            var key = GetMissionSettingsKey(missionSetting, i, missionSettingBuffer.Length);
            if (Database.Mission.Settings.TryGetValue(key, out Database.Mission.Setting ms)
            ) {
                missionSetting.RaidStability = ms.RaidStability;
                missionSetting.MissionLength = ms.MissionLength;
                missionSetting.SuccessRateBonus = ms.SuccessRateBonus;
                missionSetting.InjuryChance = ms.InjuryChance;
                missionSetting.LootFactor = ms.LootFactor;
            } else {
                if (Settings.ENV.EnableMissionRaidStability.Value) {
                    missionSetting.RaidStability = Settings.ENV.MissionRaidStability.Value;
                }

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

    private static Models.ServantMissionSetting[] MissionSettingBufferToArray(Unity.Entities.DynamicBuffer<ProjectM.ServantMissionSetting> missionSettingBuffer) {
        var array = new Models.ServantMissionSetting[missionSettingBuffer.Length];
        for (int i = 0; i < missionSettingBuffer.Length; i++) {
            array[i] = new Models.ServantMissionSetting {
                MissionLength = missionSettingBuffer[i].MissionLength,
                InjuryChance = missionSettingBuffer[i].InjuryChance,
                SuccessRateBonus = missionSettingBuffer[i].SuccessRateBonus,
                RaidStability = missionSettingBuffer[i].RaidStability,
                LootFactor = missionSettingBuffer[i].LootFactor
            };
        }
        return array;
    }

    private static string GetMissionSettingsKey(ProjectM.ServantMissionSetting missionSetting, int index = -1, int size = -1) {
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
