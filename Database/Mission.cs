using System.Collections.Generic;
using System;

namespace Database;

public static class Mission {
    public static Dictionary<string, long> missionProgress;
    public static Dictionary<string, long> injuryProgress;

    internal static void save() {
        DB.save(nameof(missionProgress), missionProgress, DB.Pretty_JSON_options);
        DB.save(nameof(injuryProgress), injuryProgress, DB.Pretty_JSON_options);
    }

    internal static void load() {
        DB.load(nameof(missionProgress), ref missionProgress);
        DB.load(nameof(injuryProgress), ref injuryProgress);
    }
}
