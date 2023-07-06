using System;
using HarmonyLib;
using ProjectM.Shared.Systems;
using Utils.Logger;
using Utils.Database;
using Utils.VRising.Entities;

namespace BetterMissions.Hooks;

[HarmonyPatch]

// ServantMissionUpdateSystem Called the mission dat update.
public class ServantMissionUpdateSystemPatch {
    [HarmonyPatch(typeof(ServantMissionUpdateSystem), nameof(ServantMissionUpdateSystem.OnCreate))]
    public static class OnCreate {
        public static void Prefix(ServantMissionUpdateSystem __instance) {
            try {
                if (Bloodstone.API.VWorld.IsClient) {
                    World.Set(Bloodstone.API.VWorld.Client);
                }
                if (Bloodstone.API.VWorld.IsServer) {
                    World.Set(Bloodstone.API.VWorld.Server);
                }
            } catch (Exception e) { Log.Fatal(e); }
        }
    }

    [HarmonyPatch(typeof(ServantMissionUpdateSystem), nameof(ServantMissionUpdateSystem.OnUpdate))]
    public static class OnUpdate {
        public static void Prefix(ServantMissionUpdateSystem __instance) {
            try {
                if (!Cache.Exists("ApplyModifiers")) { // Apply modifiers just once.
                    Cache.Key(
                        "ApplyModifiers",
                        Systems.Mission.ApplyModifiers()
                    );
                }
            } catch (Exception e) { Log.Fatal(e); }
        }
    }

    [HarmonyPatch(typeof(ServantMissionUpdateSystem), nameof(ServantMissionUpdateSystem.OnDestroy))]
    public static class OnDestroy {
        public static void Prefix(ServantMissionUpdateSystem __instance) {
            try {
                // Database.LocalDB.Save(); // TODO Database
            } catch (Exception e) { Log.Fatal(e); }
        }
    }
}
