using System.Collections.Generic;

namespace Database;

public static class Mission {
    public static Dictionary<string, float> missionProgress;
    public static List<string> missionUpdated;

    internal static void Save() {
        DB.save(nameof(missionProgress), missionProgress, DB.Pretty_JSON_options);
        DB.save(nameof(missionUpdated), missionUpdated, DB.Pretty_JSON_options);
    }

    internal static void Load() {
        DB.load(nameof(missionProgress), ref missionProgress);
        DB.load(nameof(missionUpdated), ref missionUpdated);
    }
}
