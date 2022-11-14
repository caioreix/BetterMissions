using HarmonyLib;
using ProjectM;
using Database;

namespace Patch;

[HarmonyPatch]

public class TriggerPersistenceSaveSystemPatch {
    [HarmonyPatch(typeof(TriggerPersistenceSaveSystem), nameof(TriggerPersistenceSaveSystem.TriggerSave))]
    public class TriggerSave {
        public static void Prefix() {
            DB.Save();
        }
    }
}