using BetterMissions;
using System.Text.Json;
using System.IO;
using Logger;

namespace Database;

public static class DB {
    internal static string folderPath = $"{BepInEx.Paths.ConfigPath}\\{PluginInfo.PLUGIN_GUID}";
    internal static JsonSerializerOptions JSONOptions = new() {
        WriteIndented = false,
        IncludeFields = false
    };
    internal static JsonSerializerOptions Pretty_JSON_options = new() {
        WriteIndented = true,
        IncludeFields = true
    };

    public static void Config() {
        if (!Directory.Exists(folderPath)) {
            Directory.CreateDirectory(folderPath);
            Log.Info($"Database folder created on \"{folderPath}\".");
        } else {
            Log.Info($"Using \"{folderPath}\" as database storage.");
        }
    }

    public static void Load() {
        Mission.load();

        Log.Info("All database is now loaded.");
    }

    public static void Save() {
        Mission.save();

        Log.Info("All database saved to JSON files.");
    }

    internal static void save<T>(string fileName, T data, JsonSerializerOptions jsonOptions) {
        var filePath = $"{DB.folderPath}\\{fileName}.json";
        File.WriteAllText(filePath, JsonSerializer.Serialize(data, jsonOptions));
    }

    internal static void load<T>(string fileName, ref T data) where T : new() {
        var filePath = $"{DB.folderPath}\\{fileName}.json";
        if (!File.Exists(filePath)) {
            FileStream stream = File.Create(filePath);
            stream.Dispose();
        }
        string json = File.ReadAllText(filePath);
        try {
            data = JsonSerializer.Deserialize<T>(json);
            Log.Trace($"{fileName} DB Populated");
        } catch {
            data = new T();
            Log.Trace($"{fileName} DB Created");
        }
    }
};