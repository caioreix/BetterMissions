using ProjectM;
using Entities;

namespace Hooks;

public static class PrefabCollectionSystemPatch {
    public static string GetPrefabName(PrefabGUID hashCode) {
        var s = World.Server.GetExistingSystem<PrefabCollectionSystem>();
        string name = "Nonexistent";
        if (hashCode.GuidHash == 0) {
            return name;
        }
        try {
            name = s.PrefabNameLookupMap[hashCode].ToString();
        } catch {
            name = "NoPrefabName";
        }
        return name;
    }
}