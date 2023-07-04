using System.Text;
using System;
using HarmonyLib;
using ProjectM.Shared.Systems;
using Utils.Logger;
using Utils.VRising.Entities;

namespace BetterMissions.Hooks;

[HarmonyPatch]

// ServantMissionUpdateSystem Called the mission dat update.
public class ServantMissionUpdateSystemPatch {
    [HarmonyPatch(typeof(ServantMissionUpdateSystem), nameof(ServantMissionUpdateSystem.OnCreate))]
    public static class OnCreate {
        public static void Prefix(ServantMissionUpdateSystem __instance) {
            try {
                if (Wetstone.API.VWorld.IsClient) {
                    World.Set(Wetstone.API.VWorld.Client);
                }
                if (Wetstone.API.VWorld.IsServer) {
                    World.Set(Wetstone.API.VWorld.Server);
                }
            } catch (Exception e) { Log.Fatal(e); }
        }
    }

    [HarmonyPatch(typeof(ServantMissionUpdateSystem), nameof(ServantMissionUpdateSystem.OnUpdate))]
    public static class OnUpdate {
        public static void Prefix(ServantMissionUpdateSystem __instance) {
            try {
                if (!Utils.Database.Cache.AlreadyCalled("ApplyMissionModifiers")) { // Run just on the first execution.
                    Systems.Mission.ApplyModifiers();
                }

                BetterMissions.Systems.Mission.ProgressPersistence();
            } catch (Exception e) { Log.Fatal(e); }
        }
    }

    [HarmonyPatch(typeof(ServantMissionUpdateSystem), nameof(ServantMissionUpdateSystem.OnDestroy))]
    public static class OnDestroy {
        public static void Prefix(ServantMissionUpdateSystem __instance) {
            try {
                Database.LocalDB.Save();
            } catch (Exception e) { Log.Fatal(e); }
        }
    }
}
