using System;
using Database;
using HarmonyLib;
using ProjectM;
using Utils.Logger;

namespace Hooks;

[HarmonyPatch]

public class TriggerPersistenceSaveSystemPatch {
    [HarmonyPatch(typeof(TriggerPersistenceSaveSystem), nameof(TriggerPersistenceSaveSystem.TriggerSave))]
    public class TriggerSave {
        public static void Prefix() {
            try {
                LocalDB.Save();
            } catch (Exception e) { Log.Fatal(e); }
        }
    }
}