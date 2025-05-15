using System;
using System.Reflection.Metadata;
using System.Text.Json;
using BetterMissions.Common;
// using BetterMissions.Models;
using Stunlock.Localization;
using TMPro;
using Utils.Database;
using Utils.Logger;
using Utils.VRising.Entities;

namespace BetterMissions.Systems;

public class Mission {
    // private static MissionData _missionData = null;
    private static readonly string[] missionNames = {
        Constants.Reckless1.Name,
        Constants.Reckless2.Name,
        Constants.Normal1.Name,
        Constants.Prepared1.Name,
        Constants.Prepared2.Name
    };

    public static void Setup() {
        DB.AddCleanActions();
    }

    public static void UnPatchClient() {
        // _missionData = null;
        _missionLengthParentObject = null;
        _missionHandle = null;
    }

    public static int GetMissionType() {
        return _missionHandle.position.x switch { // TODO: find a better way to get the mission type. Im not proud of this
            590 => Constants.Reckless1.Index,// Reckless1
            685 => Constants.Reckless2.Index,// Reckless2
            780 => Constants.Normal1.Index,// Normal1
            875 => Constants.Prepared1.Index,// Prepared1
            970 => Constants.Prepared2.Index,// Prepared2
            _ => -1,
        };
    }

    private static UnityEngine.Transform FindChildByPath(UnityEngine.Transform parent, params string[] path) {
        var current = parent;
        foreach (var name in path) {
            if (current == null) return null;
            current = current.Find(name);
        }
        return current;
    }

    private static UnityEngine.Transform _missionLengthParentObject = null;
    private static UnityEngine.Transform _missionHandle = null;

    public static void UpdateMissionUI(LocalizedText localizedText, LocalizedKeyValue keyValue) {
        // if (_missionData == null) {
        //     return;
        // }

        // if (_missionLengthParentObject == null) {
        //     _missionLengthParentObject = UnityEngine.GameObject.Find("SelectMissionLengthParent")?.transform;
        //     if (_missionLengthParentObject == null) {
        //         return;
        //     }
        // }

        // if (_missionHandle == null) {
        //     _missionHandle = FindChildByPath(_missionLengthParentObject, "SliderMissionLength", "Handle Slide Area", "Handle");
        //     if (_missionHandle == null) {
        //         return;
        //     }
        // }

        // try {
        //     switch (localizedText.name) {
        //         case "Text_InjuryChance":
        //             UpdateMissionInjuryChanceUI(keyValue);
        //             break;
        //         case "Text_Risk":
        //             UpdateMissionSuccessRateBonusChanceUI(keyValue);
        //             break;
        //         case "Value":
        //             string parentName = localizedText.transform.parent?.gameObject.name ?? "No parent";
        //             if (parentName == "Text_MissionLength") {
        //                 UpdateMissionLengthUI(keyValue);
        //                 break;
        //             }
        //             break;
        //         default:
        //             return;
        //     }
        // } catch (Exception e) { Log.Error(e); }
    }

    // private static void UpdateMissionLengthUI(LocalizedKeyValue keyValue) {
    //     switch (GetMissionType()) {
    //         case Constants.Reckless1.Index:
    //             keyValue.UpdateValue(SecondsToTimeString(_missionData.Reckless1.MissionLength));
    //             break;
    //         case Constants.Reckless2.Index:
    //             keyValue.UpdateValue(SecondsToTimeString(_missionData.Reckless2.MissionLength));
    //             break;
    //         case Constants.Normal1.Index:
    //             keyValue.UpdateValue(SecondsToTimeString(_missionData.Normal1.MissionLength));
    //             break;
    //         case Constants.Prepared1.Index:
    //             keyValue.UpdateValue(SecondsToTimeString(_missionData.Prepared1.MissionLength));
    //             break;
    //         case Constants.Prepared2.Index:
    //             keyValue.UpdateValue(SecondsToTimeString(_missionData.Prepared2.MissionLength));
    //             break;
    //     }
    // }

    // private static void UpdateMissionInjuryChanceUI(LocalizedKeyValue keyValue) {
    //     switch (GetMissionType()) {
    //         case Constants.Reckless1.Index:
    //             keyValue.UpdateValue((_missionData.Reckless1.InjuryChance * 100).ToString());
    //             break;
    //         case Constants.Reckless2.Index:
    //             keyValue.UpdateValue((_missionData.Reckless2.InjuryChance * 100).ToString());
    //             break;
    //         case Constants.Normal1.Index:
    //             keyValue.UpdateValue((_missionData.Normal1.InjuryChance * 100).ToString());
    //             break;
    //         case Constants.Prepared1.Index:
    //             keyValue.UpdateValue((_missionData.Prepared1.InjuryChance * 100).ToString());
    //             break;
    //         case Constants.Prepared2.Index:
    //             keyValue.UpdateValue((_missionData.Prepared2.InjuryChance * 100).ToString());
    //             break;
    //     }
    // }

    // private static void UpdateMissionSuccessRateBonusChanceUI(LocalizedKeyValue keyValue) {
    //     switch (GetMissionType()) {
    //         case Constants.Reckless1.Index:
    //             keyValue.UpdateValue((_missionData.Reckless1.SuccessRateBonus * 100).ToString());
    //             break;
    //         case Constants.Reckless2.Index:
    //             keyValue.UpdateValue((_missionData.Reckless2.SuccessRateBonus * 100).ToString());
    //             break;
    //         case Constants.Normal1.Index:
    //             keyValue.UpdateValue((_missionData.Normal1.SuccessRateBonus * 100).ToString());
    //             break;
    //         case Constants.Prepared1.Index:
    //             keyValue.UpdateValue((_missionData.Prepared1.SuccessRateBonus * 100).ToString());
    //             break;
    //         case Constants.Prepared2.Index:
    //             keyValue.UpdateValue((_missionData.Prepared2.SuccessRateBonus * 100).ToString());
    //             break;
    //     }
    // }

    public static void UpdateMissionLengthUI(LocalizedText localizedText) {
        // if (_missionData == null) {
        //     return;
        // }
        // return;

        // localizedText.SetKeyValue(null);

        // LocalizedKeyValue localizedKeyValue = localizedText.GetComponent<LocalizedKeyValue>();
        // localizedKeyValue.GetValueString();


        // ItemAmount = Estimated Loot
        // Text_InjuryChance
        // Text_Risk = Injury Chance
        // Value = Mission Length


        // <color=yellow>Normal
        // +10% Success Chance

        // Text_Risk

        // Text_InjuryChance set position to (545 344 0)

        // Update the mission length UI
        // try {
        //     string parentName = localizedText.transform.parent?.gameObject.name ?? "No parent";
        //     if (parentName != "Text_MissionLength") {
        //         return;
        //     }
        //     switch (localizedText.GetComponent<TextMeshProUGUI>().text) {
        //         case Constants.Reckless1.DefaultMissionLengthString:
        //             localizedText.GetComponent<TextMeshProUGUI>().text = SecondsToTimeString(_missionData.Reckless1.MissionLength);
        //             break;
        //         case Constants.Reckless2.DefaultMissionLengthString:
        //             localizedText.GetComponent<TextMeshProUGUI>().text = SecondsToTimeString(_missionData.Reckless2.MissionLength);
        //             break;
        //         case Constants.Normal1.DefaultMissionLengthString:
        //             localizedText.GetComponent<TextMeshProUGUI>().text = SecondsToTimeString(_missionData.Normal1.MissionLength);
        //             break;
        //         case Constants.Prepared1.DefaultMissionLengthString:
        //             localizedText.GetComponent<TextMeshProUGUI>().text = SecondsToTimeString(_missionData.Prepared1.MissionLength);
        //             break;
        //         case Constants.Prepared2.DefaultMissionLengthString:
        //             localizedText.GetComponent<TextMeshProUGUI>().text = SecondsToTimeString(_missionData.Prepared2.MissionLength);
        //             break;
        //     }
        // } catch (Exception e) {
        //     Log.Error(e);
        // }

    }

    // private static string SecondsToTimeString(float seconds) {
    //     TimeSpan time = TimeSpan.FromSeconds(seconds);
    //     return $"{time.Hours:D2}:{time.Minutes:D2}:{time.Seconds:D2} "; // Final space just to avoid the text changing twice
    // }

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

    // public static void HandleClientLoadedMessage(string message) {
    //     if (!ulong.TryParse(message, out ulong platformId)) {
    //         Log.Error($"Failed to parse steamId from message: {message}");
    //         return;
    //     }

    //     var missionSettingBuffer = ServantMissionSetting.GetBuffer();
    //     if (!missionSettingBuffer.IsCreated || missionSettingBuffer.Length != 5) {
    //         Log.Error("ServantMissionSetting buffer is not created");
    //         return;
    //     }

    //     string json = JsonSerializer.Serialize(MissionSettingBufferToArray(missionSettingBuffer));
    //     string wrappedMessage = CustomNetwork.WrapMessage(
    //         CustomNetwork.MessageType.UpdateClientMissionData,
    //         json
    //     );

    //     if (!Hooks.Server.ServerBootstrapSystemPatch.PlayerInfoCache.TryGetValue(platformId, out var playerInfo)) {
    //         Log.Error($"Failed to get player info for platformId: {platformId}");
    //         return;
    //     }

    //     CustomNetwork.SendSystemMessageToClient(
    //         playerInfo.User,
    //         wrappedMessage
    //     );
    // }

    // private static MissionData BuildMissionData(Unity.Entities.DynamicBuffer<ProjectM.ServantMissionSetting> missionSettingBuffer) {
    //     return new MissionData {
    //         Reckless1 = new Reckless1 {
    //             MissionLength = missionSettingBuffer[0].MissionLength,
    //             InjuryChance = missionSettingBuffer[0].InjuryChance,
    //             SuccessRateBonus = missionSettingBuffer[0].SuccessRateBonus
    //         },
    //         Reckless2 = new Reckless2 {
    //             MissionLength = missionSettingBuffer[1].MissionLength,
    //             InjuryChance = missionSettingBuffer[1].InjuryChance,
    //             SuccessRateBonus = missionSettingBuffer[1].SuccessRateBonus
    //         },
    //         Normal1 = new Normal1 {
    //             MissionLength = missionSettingBuffer[2].MissionLength,
    //             InjuryChance = missionSettingBuffer[2].InjuryChance,
    //             SuccessRateBonus = missionSettingBuffer[2].SuccessRateBonus
    //         },
    //         Prepared1 = new Prepared1 {
    //             MissionLength = missionSettingBuffer[3].MissionLength,
    //             InjuryChance = missionSettingBuffer[3].InjuryChance,
    //             SuccessRateBonus = missionSettingBuffer[3].SuccessRateBonus
    //         },
    //         Prepared2 = new Prepared2 {
    //             MissionLength = missionSettingBuffer[4].MissionLength,
    //             InjuryChance = missionSettingBuffer[4].InjuryChance,
    //             SuccessRateBonus = missionSettingBuffer[4].SuccessRateBonus
    //         }
    //     };
    // }


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
            // _missionData = JsonSerializer.Deserialize<MissionData>(message);



            var missionSettingBuffer = ServantMissionSetting.GetBuffer();
            if (missionSettingBuffer.IsCreated) {
                MissionSettingArrayToBuffer(
                    JsonSerializer.Deserialize<Models.ServantMissionSetting[]>(message),
                    ref missionSettingBuffer
                );


                // for (int i = 0; i < missionSettingBuffer.Length; i++) {
                // var missionSetting = missionSettingBuffer[i];

                // switch (i) {
                //     case Constants.Reckless1.Index:
                //         missionSetting.MissionLength = _missionData.Reckless1.MissionLength;
                //         missionSetting.InjuryChance = _missionData.Reckless1.InjuryChance;
                //         missionSetting.SuccessRateBonus = _missionData.Reckless1.SuccessRateBonus;
                //         break;
                //     case Constants.Reckless2.Index:
                //         missionSetting.MissionLength = _missionData.Reckless2.MissionLength;
                //         missionSetting.InjuryChance = _missionData.Reckless2.InjuryChance;
                //         missionSetting.SuccessRateBonus = _missionData.Reckless2.SuccessRateBonus;
                //         break;
                //     case Constants.Normal1.Index:
                //         missionSetting.MissionLength = _missionData.Normal1.MissionLength;
                //         missionSetting.InjuryChance = _missionData.Normal1.InjuryChance;
                //         missionSetting.SuccessRateBonus = _missionData.Normal1.SuccessRateBonus;
                //         break;
                //     case Constants.Prepared1.Index:
                //         missionSetting.MissionLength = _missionData.Prepared1.MissionLength;
                //         missionSetting.InjuryChance = _missionData.Prepared1.InjuryChance;
                //         missionSetting.SuccessRateBonus = _missionData.Prepared1.SuccessRateBonus;
                //         break;
                //     case Constants.Prepared2.Index:
                //         missionSetting.MissionLength = _missionData.Prepared2.MissionLength;
                //         missionSetting.InjuryChance = _missionData.Prepared2.InjuryChance;
                //         missionSetting.SuccessRateBonus = _missionData.Prepared2.SuccessRateBonus;
                //         break;
                // }

                // missionSettingBuffer[i] = missionSetting;

                // Log.Debug($"RaidStability {i}: {missionSettingBuffer[i].RaidStability}"); // TODO: remove
                // Log.Debug($"SuccessRateBonus {i}: {missionSettingBuffer[i].SuccessRateBonus}"); // TODO: remove
                // Log.Debug($"MissionLength {i}: {missionSettingBuffer[i].MissionLength}"); // TODO: remove
                // Log.Debug($"InjuryChance {i}: {missionSettingBuffer[i].InjuryChance}"); // TODO: remove
                // Log.Debug($"LootFactor {i}: {missionSettingBuffer[i].LootFactor}"); // TODO: remove
                // }

                string json = JsonSerializer.Serialize(MissionSettingBufferToArray(missionSettingBuffer));
                Log.Info($"MissionSettingBuffer: {json}");
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
