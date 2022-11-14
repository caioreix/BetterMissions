using System;
using HarmonyLib;
using ProjectM.Shared.Systems;
using Logger;
using Settings;
using Database;

namespace Patch;

[HarmonyPatch]

// ServantMissionUpdateSystem Called the mission dat update.
public class ServantMissionUpdateSystemPatch {
    [HarmonyPatch(typeof(ServantMissionUpdateSystem), nameof(ServantMissionUpdateSystem.OnCreate))]
    public static class OnCreate {
        public static void Prefix(ServantMissionUpdateSystem __instance) {
            try {
                Log.Trace("ServantMissionUpdateSystem: OnCreate");
            } catch (Exception e) { Log.Fatal(e); }
        }
    }

    [HarmonyPatch(typeof(ServantMissionUpdateSystem), nameof(ServantMissionUpdateSystem.OnUpdate))]
    public static class OnUpdate {
        public static void Prefix(ServantMissionUpdateSystem __instance) {
            try {
                Systems.Mission.ReduceAllNewMissionsTimeProgress(__instance.EntityManager, Env.MissionReduceRate.Value);
                Systems.Injury.ReduceAllNewInjuriesTimeProgress(__instance.EntityManager, Env.InjuryReduceRate.Value);
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
