using System;
using HarmonyLib;
using ProjectM;
using Utils.Database;
using Utils.Logger;

namespace BetterMissions.Hooks.Client;

public class ClientBootstrapSystemPatch {
    [HarmonyPatch(typeof(ClientBootstrapSystem), nameof(ClientBootstrapSystem.OnDestroy))]
    public class OnDestroy {
        public static void Postfix() {
            try {
                Utils.VRising.Entities.World.UnPatch();
                Cache.Clear();
            } catch (Exception e) { Log.Fatal(e); }
        }
    }
}
