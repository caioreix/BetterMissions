using System;
using HarmonyLib;
using ProjectM;
using Logger;
using Database;

namespace Patch;

[HarmonyPatch]

public class GameBootstrapPatch {
    [HarmonyPatch(typeof(GameBootstrap), nameof(GameBootstrap.OnApplicationQuit))]
    public class OnApplicationQuit {
        public static void Prefix() {
            try {
                DB.Save();
            } catch (Exception e) { Log.Fatal(e); }
        }
    }
}