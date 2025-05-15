using System;
using BetterMissions.Systems;
using HarmonyLib;
using ProjectM;
using Unity.Entities;
using Utils.Database;
using Utils.Logger;

namespace BetterMissions.Hooks.Client;

public class ClientBootstrapSystemPatch {
    [HarmonyPatch(typeof(ClientBootstrapSystem), nameof(ClientBootstrapSystem.OnCreate))]
    public class OnCreate {
        public static void Postfix() {
            try {
                BetterMissions.Client.harmony.CreateClassProcessor(typeof(LocalizedTextPatch.SetKeyValue)).Patch();
                Log.Info("CreateClassProcessorSetKeyValue Patched");
            } catch (Exception e) { Log.Warning($"CreateClassProcessor: {e}"); }
        }
    }

    [HarmonyPatch(typeof(ClientBootstrapSystem), nameof(ClientBootstrapSystem.OnDestroy))]
    public class OnDestroy {
        public static void Postfix() {
            try {
                BetterMissions.Client.harmony.Unpatch(
                    AccessTools.Method(typeof(LocalizedText), nameof(LocalizedText.SetKeyValue)),
                    HarmonyPatchType.All,
                    BetterMissions.Client.harmony.Id
                );

                // Unpatch the systems when the client is destroyed
                Mission.UnPatchClient();
                Utils.VRising.Entities.World.UnPatch();
                Cache.Clear();
            } catch (Exception e) { Log.Fatal(e); }
        }
    }
}
