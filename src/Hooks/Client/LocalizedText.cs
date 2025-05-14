using System;
using BetterMissions.Common;
using BetterMissions.Systems;
using HarmonyLib;
using TMPro;
using Utils.Logger;

namespace BetterMissions.Hooks.Client;


public class LocalizedTextPatch {
    [HarmonyPatch(typeof(LocalizedText), nameof(LocalizedText.UpdateText))]
    public class UpdateText {
        public static void Postfix(LocalizedText __instance) {
            try {
                Mission.UpdateMissionLengthUI(__instance);
            } catch (Exception e) {
                Log.Fatal(e);
            }
        }
    }
}
