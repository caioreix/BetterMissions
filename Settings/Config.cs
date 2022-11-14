using BepInEx.Configuration;

namespace Settings;

public class Config {
    internal static ConfigFile cfg;

    public static void Load(ConfigFile config) {
        Config.cfg = config;

        Env.load();
    }
}