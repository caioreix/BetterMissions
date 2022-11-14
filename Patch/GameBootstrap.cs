using HarmonyLib;
using ProjectM;
using Database;

namespace Patch;

[HarmonyPatch]

public class GameBootstrapPatch {
    [HarmonyPatch(typeof(GameBootstrap), nameof(GameBootstrap.OnApplicationQuit))]
    public class OnApplicationQuit {
        public static void Prefix() {
            DB.Save();
        }
    }
}