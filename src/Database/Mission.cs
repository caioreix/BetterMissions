using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Utils.Database;

namespace BetterMissions.Database;

public static class Mission {
    public struct Progress {
        public long EndTimestamp { get; set; }
        [JsonIgnore]
        public bool Synced;
    };


    public struct Setting {
        public ProjectM.RaidStability RaidStability { get; set; }
        public float SuccessRateBonus { get; set; }
        public float MissionLength { get; set; }
        public float InjuryChance { get; set; }
        public float LootFactor { get; set; }
    }

    private static string category = nameof(Mission);
    public static ConcurrentDictionary<string, Progress> Progresses;
    public static Dictionary<string, Setting> Settings = new Dictionary<string, Setting>();

    public static void Setup() {
        DB.AddLoadActions(
            () => DB.loadFile($"{category}{nameof(Progresses)}", ref Progresses)
        );
        DB.AddSaveActions(
            () => DB.saveFile($"{category}{nameof(Progresses)}", Progresses, true)
        );
    }
}
