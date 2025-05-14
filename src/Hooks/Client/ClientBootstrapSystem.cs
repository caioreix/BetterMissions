using System;
using BetterMissions.Systems;
using HarmonyLib;
using ProjectM;
using Unity.Entities;
using Utils.Database;
using Utils.Logger;

namespace BetterMissions.Hooks.Client;

public class ClientBootstrapSystemPatch {
    [HarmonyPatch(typeof(ClientBootstrapSystem), nameof(ClientBootstrapSystem.OnDestroy))]
    public class OnDestroy {
        public static void Postfix() {
            try {
                // Unpatch the systems when the client is destroyed
                Mission.UnPatchClient();
                Utils.VRising.Entities.World.UnPatch();
                Cache.Clear();
            } catch (Exception e) { Log.Fatal(e); }
        }
    }
}
