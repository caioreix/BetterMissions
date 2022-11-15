using BepInEx.Configuration;

namespace Settings;

public class Env {
    // SERVER
    public static ConfigEntry<float> MissionReduceRate;
    public static ConfigEntry<bool> OfflineMissionProgress;

    // DEBUG
    public static ConfigEntry<bool> LogOnTempFile;
    public static ConfigEntry<bool> EnableTraceLogs;

    // Load the plugin config variables.
    internal static void load() {
        MissionReduceRate = Config.cfg.Bind(
            "Server",
            "MissionReduceRate",
            2F,
            "Define the mission reduce divisor. Ex: if you set 2, 2 hours will be 1 hour (0 will be replaced by 1)"
        );

        OfflineMissionProgress = Config.cfg.Bind(
           "Server",
           "OfflineMissionProgress",
           true,
           "Enabled, mission progress will be loaded even with the server offline."
       );

        LogOnTempFile = Config.cfg.Bind(
            "Debug",
            "LogOnTempFile",
            false,
            "Enabled, will log every plugin log on a temp file"
        );

        EnableTraceLogs = Config.cfg.Bind(
            "Debug",
            "EnableTraceLogs",
            false,
            "Enabled, will print Trace logs (Debug output in BepInEx)"
        );

        validateValues();
    }

    private static void validateValues() {
        if (MissionReduceRate.Value == 0) MissionReduceRate.Value = 1;

        Config.cfg.Save();
    }
}
