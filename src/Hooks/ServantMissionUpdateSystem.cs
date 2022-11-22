using System;
using HarmonyLib;
using ProjectM.Shared.Systems;
using Utils.Database;
using Utils.Logger;

namespace BetterMissions.Hooks;

[HarmonyPatch]

// ServantMissionUpdateSystem Called the mission dat update.
public class ServantMissionUpdateSystemPatch {

    [HarmonyPatch(typeof(ServantMissionUpdateSystem), nameof(ServantMissionUpdateSystem.OnUpdate))]
    public static class OnUpdate {
        public static void Prefix(ServantMissionUpdateSystem __instance) {
            try {
                BetterMissions.Systems.Mission.ReduceAllNewMissionsTimeProgress(__instance.EntityManager, Settings.ENV.MissionReduceRate.Value);
            } catch (Exception e) { Log.Fatal(e); }
        }
    }

    [HarmonyPatch(typeof(ServantMissionUpdateSystem), nameof(ServantMissionUpdateSystem.OnDestroy))]
    public static class OnDestroy {
        public static void Prefix(ServantMissionUpdateSystem __instance) {
            try {
                DB.Save();
            } catch (Exception e) { Log.Fatal(e); }
        }
    }
}
