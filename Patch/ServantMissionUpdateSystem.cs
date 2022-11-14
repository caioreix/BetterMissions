using HarmonyLib;
using ProjectM.Shared.Systems;
using Logger;
using Database;

namespace Patch;

[HarmonyPatch]

// ServantMissionUpdateSystem Called the mission dat update.
public class ServantMissionUpdateSystemPatch {
    [HarmonyPatch(typeof(ServantMissionUpdateSystem), nameof(ServantMissionUpdateSystem.OnCreate))]
    public static class OnCreate {
        public static void Prefix(ServantMissionUpdateSystem __instance) {
            Log.Trace("ServantMissionUpdateSystem: OnCreate");
        }
    }

    [HarmonyPatch(typeof(ServantMissionUpdateSystem), nameof(ServantMissionUpdateSystem.OnUpdate))]
    public static class OnUpdate {
        public static void Prefix(ServantMissionUpdateSystem __instance) {
            Log.Trace("ServantMissionUpdateSystem: OnUpdate");
        }
    }

    [HarmonyPatch(typeof(ServantMissionUpdateSystem), nameof(ServantMissionUpdateSystem.OnDestroy))]
    public static class OnDestroy {
        public static void Prefix(ServantMissionUpdateSystem __instance) {
            DB.Save();
        }
    }
}
