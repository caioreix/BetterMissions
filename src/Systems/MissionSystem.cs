using System;
using System.Text.Json;
using BetterMissions.Common;
using BetterMissions.Models;
using TMPro;
using Utils.Database;
using Utils.Logger;
using Utils.VRising.Entities;

namespace BetterMissions.Systems;

public class Mission {
    private static MissionData missionData = null;

    public static void Setup() {
        DB.AddCleanActions();
    }

    public static void UnPatchClient() {
        missionData = null;
    }

    public static void UpdateMissionLengthUI(LocalizedText localizedText) {
        if (missionData == null) {
            return;
        }

        switch (localizedText.GetComponent<TextMeshProUGUI>().text) {
            case Constants.Reckless1.LengthString:
                localizedText.GetComponent<TextMeshProUGUI>().text = SecondsToTimeString(missionData.Reckless1.MissionLength);
                break;
            case Constants.Reckless2.LengthString:
                localizedText.GetComponent<TextMeshProUGUI>().text = SecondsToTimeString(missionData.Reckless2.MissionLength);
                break;
            case Constants.Normal1.LengthString:
                localizedText.GetComponent<TextMeshProUGUI>().text = SecondsToTimeString(missionData.Normal1.MissionLength);
                break;
            case Constants.Prepared1.LengthString:
                localizedText.GetComponent<TextMeshProUGUI>().text = SecondsToTimeString(missionData.Prepared1.MissionLength);
                break;
            case Constants.Prepared2.LengthString:
                localizedText.GetComponent<TextMeshProUGUI>().text = SecondsToTimeString(missionData.Prepared2.MissionLength);
                break;
        }
    }

    private static string SecondsToTimeString(float seconds) {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        return $"{time.Hours:D2}:{time.Minutes:D2}:{time.Seconds:D2} "; // Final space just to avoid the text changing twice
    }

    public static void UpdateUserDataUI(ProjectM.Network.User user) {
        var missionSettingBuffer = ServantMissionSetting.GetBuffer();
        if (!missionSettingBuffer.IsCreated || missionSettingBuffer.Length != 5) {
            Log.Error("ServantMissionSetting buffer is not created");
            return;
        }

        var data = new MissionData {
            Reckless1 = new Reckless1 { MissionLength = missionSettingBuffer[0].MissionLength },
            Reckless2 = new Reckless2 { MissionLength = missionSettingBuffer[1].MissionLength },
            Normal1 = new Normal1 { MissionLength = missionSettingBuffer[2].MissionLength },
            Prepared1 = new Prepared1 { MissionLength = missionSettingBuffer[3].MissionLength },
            Prepared2 = new Prepared2 { MissionLength = missionSettingBuffer[4].MissionLength }
        };

        string json = JsonSerializer.Serialize(data);
        string wrappedMessage = CustomNetwork.WrapMessage(
            CustomNetwork.MessageType.UpdateClientMissionData,
            json
        );

        CustomNetwork.SendSystemMessageToClient(
            user,
            wrappedMessage
        );
    }

    public static void HandleClientLoadedMessage(string message) {
        if (!ulong.TryParse(message, out ulong platformId)) {
            Log.Error($"Failed to parse steamId from message: {message}");
            return;
        }

        var missionSettingBuffer = ServantMissionSetting.GetBuffer();
        if (!missionSettingBuffer.IsCreated || missionSettingBuffer.Length != 5) {
            Log.Error("ServantMissionSetting buffer is not created");
            return;
        }

        var data = new MissionData {
            Reckless1 = new Reckless1 { MissionLength = missionSettingBuffer[0].MissionLength },
            Reckless2 = new Reckless2 { MissionLength = missionSettingBuffer[1].MissionLength },
            Normal1 = new Normal1 { MissionLength = missionSettingBuffer[2].MissionLength },
            Prepared1 = new Prepared1 { MissionLength = missionSettingBuffer[3].MissionLength },
            Prepared2 = new Prepared2 { MissionLength = missionSettingBuffer[4].MissionLength }
        };

        string json = JsonSerializer.Serialize(data);
        string wrappedMessage = CustomNetwork.WrapMessage(
            CustomNetwork.MessageType.UpdateClientMissionData,
            json
        );

        if (!Hooks.Server.ServerBootstrapSystemPatch.PlayerInfoCache.TryGetValue(platformId, out var playerInfo)) {
            Log.Error($"Failed to get player info for platformId: {platformId}");
            return;
        }

        CustomNetwork.SendSystemMessageToClient(
            playerInfo.User,
            wrappedMessage
        );
    }

    public static void HandleUpdateMissionDataMessage(string message) {
        try {
            missionData = JsonSerializer.Deserialize<MissionData>(message);

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
            Log.Debug($"RaidStability {i}: {missionSettingBuffer[i].RaidStability}"); // TODO: remove
            Log.Debug($"SuccessRateBonus {i}: {missionSettingBuffer[i].SuccessRateBonus}"); // TODO: remove
            Log.Debug($"MissionLength {i}: {missionSettingBuffer[i].MissionLength}"); // TODO: remove
            Log.Debug($"InjuryChance {i}: {missionSettingBuffer[i].InjuryChance}"); // TODO: remove
            Log.Debug($"LootFactor {i}: {missionSettingBuffer[i].LootFactor}"); // TODO: remove

            var missionSetting = missionSettingBuffer[i];
            var key = getMissionSettingsKey(missionSetting, i, missionSettingBuffer.Length);
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
