using System;
using HarmonyLib;
using ProjectM.Shared.Systems;
using Utils.Database;
using Utils.Logger;

namespace BetterMissions.Hooks.Server;

[HarmonyPatch]

public class ServantMissionUpdateSystemPatch {
    [HarmonyPatch(typeof(ServantMissionUpdateSystem), nameof(ServantMissionUpdateSystem.OnUpdate))]
    public static class OnUpdate {
        public static void Prefix(ServantMissionUpdateSystem __instance) {
            try {
                // Apply modifiers just once.
                if (!Cache.Exists("ApplyModifiers")) {
                    Cache.Key(
                        "ApplyModifiers",
                        Systems.Mission.ApplyModifiers()
                    );
                }
            } catch (Exception e) {
                Log.Fatal(e);
            }
        }
    }
}
