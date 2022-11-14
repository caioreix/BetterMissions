using BepInEx.Configuration;

namespace Settings;

public class Env {
    public static ConfigEntry<bool> LogOnTempFile;
    public static ConfigEntry<bool> EnableTraceLogs;

    // Load the plugin config variables.
    internal static void load() {
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
