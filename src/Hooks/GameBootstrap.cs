using System;
using HarmonyLib;
using ProjectM;
using Utils.Logger;

namespace BetterMissions.Hooks;

[HarmonyPatch]

public class GameBootstrapPatch {
    [HarmonyPatch(typeof(GameBootstrap), nameof(GameBootstrap.OnApplicationQuit))]
    public class OnApplicationQuit {
        public static void Prefix() {
            try {
                // Database.LocalDB.Save();  // TODO Database
            } catch (Exception e) { Log.Fatal(e); }
        }
    }
}
