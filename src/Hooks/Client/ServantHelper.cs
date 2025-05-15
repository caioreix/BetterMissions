

using HarmonyLib;
using ProjectM;
using Utils.Logger;

namespace BetterMissions.Hooks.Client;


public class ServantHelperPatch {
    [HarmonyPatch(typeof(ServantHelper), nameof(ServantHelper.GetMissionSuccessChanceForServant_Client))]
    public class GetMissionSuccessChanceForServant_Client {
        public static void Prefix() {
            // Unity.Entities.EntityManager entityManager,
            // Stunlock.Core.PrefabLookupMap prefabLookupMap,
            // ref ServantMissionSettingsSingleton servantMissionSettings,
            // Stunlock.Core.PrefabGUID selectedMission,
            // ProjectM.Network.ServantInfoEvent.Response.Entry currentServant,
            // Unity.Collections.FixedList4096Bytes<ProjectM.Network.ServantInfoEvent.Response.Entry> allServants,
            // Unity.Collections.NativeList<ProjectM.Network.NetworkId> assignedServants,
            // float missionStability,
            // out int partyPower,
            // out float perkLootBonus

            // ServantHelper.TryGetMissionSetting()

            Log.Info($"ServantHelperPatch: GetMissionSuccessChanceForServant_Client called");
        }
    }

    [HarmonyPatch(typeof(ServantHelper), nameof(ServantHelper.TryGetMissionSetting))]
    public class TryGetMissionSetting {
        public static void Prefix(Unity.Entities.DynamicBuffer<ServantMissionSetting> missionList, int missionDataID, ServantMissionSetting mission) {

            for (int i = 0; i < missionList.Length; i++) {
                Log.Info($"ServantHelperPatch: TryGetMissionSetting found mission {missionList[i].InjuryChance}");
                Log.Info($"ServantHelperPatch: TryGetMissionSetting found mission {missionList[i].LootFactor}");
                Log.Info($"ServantHelperPatch: TryGetMissionSetting found mission {missionList[i].MissionLength}");
                Log.Info($"ServantHelperPatch: TryGetMissionSetting found mission {missionList[i].RaidStability}");
                Log.Info($"ServantHelperPatch: TryGetMissionSetting found mission {missionList[i].SuccessRateBonus}");
            }
            // Log.Info()

            // Log.Info($"ServantHelperPatch: TryGetMissionSetting called");
        }

        public static void Postfix(bool __result, Unity.Entities.DynamicBuffer<ServantMissionSetting> missionList, int missionDataID, ref ServantMissionSetting mission) {
            if (__result) {
                for (int i = 0; i < missionList.Length; i++) {
                    Log.Info($"ServantHelperPatch: TryGetMissionSetting found mission[{i}] {missionList[i].InjuryChance}");
                    Log.Info($"ServantHelperPatch: TryGetMissionSetting found mission[{i}] {missionList[i].LootFactor}");
                    Log.Info($"ServantHelperPatch: TryGetMissionSetting found mission[{i}] {missionList[i].MissionLength}");
                    Log.Info($"ServantHelperPatch: TryGetMissionSetting found mission[{i}] {missionList[i].RaidStability}");
                    Log.Info($"ServantHelperPatch: TryGetMissionSetting found mission[{i}] {missionList[i].SuccessRateBonus}");
                }

                Log.Info($"ServantHelperPatch: TryGetMissionSetting found mission {mission.InjuryChance}");
                Log.Info($"ServantHelperPatch: TryGetMissionSetting found mission {mission.LootFactor}");
                Log.Info($"ServantHelperPatch: TryGetMissionSetting found mission {mission.MissionLength}");
                Log.Info($"ServantHelperPatch: TryGetMissionSetting found mission {mission.RaidStability}");
                Log.Info($"ServantHelperPatch: TryGetMissionSetting found mission {mission.SuccessRateBonus}");
            }
        }
    }
}
