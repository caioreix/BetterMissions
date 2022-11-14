using BepInEx.Configuration;

namespace Settings;

public class Env {
    public static ConfigEntry<float> MissionReduceRate;
    public static ConfigEntry<bool> OfflineMissionProgress;
    public static ConfigEntry<float> InjuryReduceRate;
    public static ConfigEntry<bool> OfflineInjuryProgress;

    public static ConfigEntry<bool> LogOnTempFile;
    public static ConfigEntry<bool> EnableTraceLogs;

    // Load the plugin config variables.
    internal static void load() {
        MissionReduceRate = Config.cfg.Bind(
            "Server",
            "MissionReduceRate",
            2F,
            "Define the mission reduce divisor. Ex: if you set 2, 2 hours will be 1 hour"
        );

        OfflineMissionProgress = Config.cfg.Bind(
           "Server",
           "OfflineMissionProgress",
           true,
           "Enabled, mission progress will be loaded even with the server offline."
       );

        InjuryReduceRate = Config.cfg.Bind(
            "Server",
            "InjuryReduceRate",
            2F,
            "Define the injury reduce divisor. Ex: if you set 2, 2 hours will be 1 hour"
        );

        OfflineInjuryProgress = Config.cfg.Bind(
           "Server",
           "OfflineInjuryProgress",
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
    }
}
