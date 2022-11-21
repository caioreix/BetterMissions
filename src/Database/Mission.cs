using System.Collections.Concurrent;
using System.Text.Json.Serialization;

using Utils.Database;

namespace Database;

public static class Mission {
    public struct ProgressStruct {
        public long EndTimestamp { get; set; }
        public float Modifier { get; set; }
        [JsonIgnore]
        public bool Synced;
    };

    private static string category = nameof(Mission);

    public static ConcurrentDictionary<string, ProgressStruct> Progress;

    internal static void save() {
        DB.save($"{category}{nameof(Progress)}", Progress, DB.Pretty_JSON_options);
    }

    internal static void load() {
        DB.load($"{category}{nameof(Progress)}", ref Progress);
    }
}
