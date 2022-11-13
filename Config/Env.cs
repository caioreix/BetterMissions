using BepInEx.Configuration;

namespace Config;

public class Env
{
    public static ConfigFile Config;
    public static ConfigEntry<bool> LogOnTempFile;
    public static ConfigEntry<string> LastLogTempFilePath;

    // Load the plugin config variables.
    public static void Load()
    {
        LogOnTempFile = Config.Bind(
            "Debug",
            "LogOnTempFile",
            false,
            "Enabled, will log every plugin log on a temp file"
        );

        LastLogTempFilePath = Config.Bind(
            "Debug",
            "LastLogTempFilePath",
            "",
            "Just to get the file path more easily"
        );
    }
}
