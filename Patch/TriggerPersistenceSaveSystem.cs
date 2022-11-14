using System;
using HarmonyLib;
using ProjectM;
using Logger;
using Database;

namespace Patch;

[HarmonyPatch]

public class TriggerPersistenceSaveSystemPatch {
    [HarmonyPatch(typeof(TriggerPersistenceSaveSystem), nameof(TriggerPersistenceSaveSystem.TriggerSave))]
    public class TriggerSave {
        public static void Prefix() {
            try {
                DB.Save();
            } catch (Exception e) { Log.Fatal(e); }
        }
    }
}