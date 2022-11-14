using HarmonyLib;
using Logger;
using ProjectM.Shared.Systems;

namespace Patch;

[HarmonyPatch]

// ServantMissionUpdateSystem Called the mission dat update.
public class ServantMissionUpdateSystemPatch {
    [HarmonyPatch(typeof(ServantMissionUpdateSystem), "OnCreate")]
    public static class OnCreate {
        public static void Prefix(ServantMissionUpdateSystem __instance) {
            Log.Trace("ServantMissionUpdateSystem: OnCreate");
        }
    }

    [HarmonyPatch(typeof(ServantMissionUpdateSystem), "OnUpdate")]
    public static class OnUpdate {
        public static void Prefix(ServantMissionUpdateSystem __instance) {
            Log.Trace("ServantMissionUpdateSystem: OnUpdate");
        }
    }

    [HarmonyPatch(typeof(ServantMissionUpdateSystem), "OnDestroy")]
    public static class OnDestroy {
        public static void Prefix(ServantMissionUpdateSystem __instance) {
            Log.Trace("ServantMissionUpdateSystem: OnDestroy");
        }
    }
}
