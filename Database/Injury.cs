using System.Collections.Generic;

namespace Database;

public static class Injury {
    private static string category = nameof(Injury);

    public static Dictionary<string, string> Progress;

    internal static void save() {
        DB.save($"{category}{nameof(Progress)}", Progress, DB.Pretty_JSON_options);
    }

    internal static void load() {
        DB.load($"{category}{nameof(Progress)}", ref Progress);
    }
}
