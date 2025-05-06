#nullable enable

using System;
using System.Net.Mime;
using HarmonyLib;
using ProjectM.Shared.Systems;
using UnityEngine;
using Utils.Database;
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
                if (Application.productName == "VRising") {
                    World.Set(GetWorld("Client_0") ?? throw new Exception("There is no Client world (yet). Did you install a client mod on the server?"));
                }

                if (Application.productName == "VRisingServer") {
                    World.Set(GetWorld(nameof(Server)) ?? throw new Exception("There is no Server world (yet). Did you install a server mod on the client?"));
                }
            } catch (Exception e) {
                Log.Fatal(e);
            }
        }
    }

    [HarmonyPatch(typeof(ServantMissionUpdateSystem), nameof(ServantMissionUpdateSystem.OnUpdate))]
    public static class OnUpdate {
        public static void Prefix(ServantMissionUpdateSystem __instance) {
            try {
                if (!Cache.Exists("ApplyModifiers")) {
                    // Apply modifiers just once.
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

    [HarmonyPatch(typeof(ServantMissionUpdateSystem), nameof(ServantMissionUpdateSystem.OnDestroy))]
    public static class OnDestroy {
        public static void Prefix(ServantMissionUpdateSystem __instance) {
            try {
                // Database.LocalDB.Save(); // TODO Database
            } catch (Exception e) {
                Log.Fatal(e);
            }
        }
    }

    private static Unity.Entities.World? GetWorld(string name) {
        foreach (Unity.Entities.World sAllWorld in Unity.Entities.World.s_AllWorlds) {
            if (sAllWorld.Name == name) {
                return sAllWorld;
            }
        }
        return null;
    }
}
