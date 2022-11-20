using System;

using BepInEx.Configuration;
using BepInEx.Logging;
using BetterMissions;

namespace Settings;

public class Config {
    public static void Load(ConfigFile configFile, ManualLogSource logger, string worldType) {
        var configActions = new Action[]{
            () => ENV.load(),
    };

        Utils.Settings.Config.Load(PluginInfo.PLUGIN_GUID, configFile, configActions);
        Utils.Logger.Config.Load(logger, worldType);
    }
}