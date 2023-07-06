using BepInEx.Configuration;
using BepInEx.Logging;

namespace BetterMissions.Settings;

public class Config {
    public static void Load(ConfigFile configFile, ManualLogSource logger, string worldType) {
        // Settings setup
        Settings.ENV.Mission.Setup();
        Utils.Settings.Config.Setup(MyPluginInfo.PLUGIN_GUID, configFile);
        Utils.Settings.Config.Load(); // just load this after setup all actions.

        // Logger setup
        Utils.Logger.Config.Setup(logger, worldType);

        // Databases setup
        // Database.Mission.Setup(); // Load/Save // TODO Database
        // Systems.Mission.Setup();  // Clean // TODO Database
    }
}
