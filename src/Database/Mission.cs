using System.Collections.Concurrent;

using Utils.Database;

namespace Database;

public static class Mission {
    private static string category = nameof(Mission);

    public static ConcurrentDictionary<string, long> Progress;

    internal static void save() {
        DB.save($"{category}{nameof(Progress)}", Progress, DB.Pretty_JSON_options);
    }

    internal static void load() {
        DB.load($"{category}{nameof(Progress)}", ref Progress);
    }
}
