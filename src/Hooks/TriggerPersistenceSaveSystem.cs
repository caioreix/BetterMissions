using System;
using HarmonyLib;
using ProjectM;
using Utils.Logger;

namespace BetterMissions.Hooks;

[HarmonyPatch]

public class TriggerPersistenceSaveSystemPatch {
    [HarmonyPatch(typeof(TriggerPersistenceSaveSystem), nameof(TriggerPersistenceSaveSystem.TriggerSave))]
    public class TriggerSave {
        public static void Prefix() {
            try {
                Database.LocalDB.Save();
            } catch (Exception e) { Log.Fatal(e); }
        }
    }
}