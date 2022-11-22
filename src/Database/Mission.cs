using System.Collections.Concurrent;
using System.Text.Json.Serialization;
using Utils.Database;

namespace BetterMissions.Database;

public static class Mission {
    public struct ProgressStruct {
        public long EndTimestamp { get; set; }
        public float Modifier { get; set; }
        [JsonIgnore]
        public bool Synced;
    };

    private static string category = nameof(Mission);
    public static ConcurrentDictionary<string, ProgressStruct> Progress;

    public static void Setup() {
        DB.AddLoadActions(
            () => DB.loadFile($"{category}{nameof(Progress)}", ref Progress)
        );
        DB.AddSaveActions(
            () => DB.saveFile($"{category}{nameof(Progress)}", Progress, true)
        );
    }
}
