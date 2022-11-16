using BepInEx;
using BepInEx.Logging;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using HarmonyLib;
using Wetstone.API;
using Database;
using Logger;

namespace BetterMissions;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
[BepInDependency("xyz.molenzwiebel.wetstone")]
public class Plugin : BasePlugin {
    public override void Load() {
        if (VWorld.IsServer) Server.Load(this.Config, this.Log);
        if (VWorld.IsClient) Client.Load(this.Config, this.Log);
    }

    public override bool Unload() {
        if (VWorld.IsServer) Server.Unload();
        if (VWorld.IsClient) Client.Unload();

        return false;
    }
}

public static class Server {
    public static Harmony harmony;
    internal static void Load(ConfigFile config, ManualLogSource logger) {
        Settings.Config.Load(config);
        Logger.Config.Load(logger, "Server", global::Settings.Env.LogOnTempFile.Value, global::Settings.Env.EnableTraceLogs.Value);

        DB.Config();
        DB.Load();

        harmony = new Harmony(PluginInfo.PLUGIN_GUID);

        Log.Trace("Patching harmony");
        harmony.PatchAll();

        Log.Info($"Plugin {PluginInfo.PLUGIN_GUID} v{PluginInfo.PLUGIN_VERSION} server side is loaded!");
    }

    internal static bool Unload() {
        harmony.UnpatchSelf();
        DB.Save();

        Log.Info($"Plugin {PluginInfo.PLUGIN_GUID} v{PluginInfo.PLUGIN_VERSION} server side is unloaded!");
        return true;
    }
}

internal static class Client {
    internal static void Load(ConfigFile config, ManualLogSource logger) {
        Settings.Config.Load(config);
        Logger.Config.Load(logger, "Client", global::Settings.Env.LogOnTempFile.Value, global::Settings.Env.EnableTraceLogs.Value);

        Log.Info($"Plugin {PluginInfo.PLUGIN_GUID} v{PluginInfo.PLUGIN_VERSION} client side is loaded!");
    }

    internal static void Unload() {
        Log.Info($"Plugin {PluginInfo.PLUGIN_GUID} v{PluginInfo.PLUGIN_VERSION} client side is unloaded!");
    }
}
