using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using BepInEx.Logging;
using BetterMissions.Database;
using HarmonyLib;
using Utils.Logger;
using Bloodstone.API;

namespace BetterMissions;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("gg.deca.Bloodstone")]
// [Bloodstone.API.Reloadable]
public class Plugin : BasePlugin
{
    public override void Load()
    {
        if (VWorld.IsServer) Server.Load(this.Config, this.Log);
        if (VWorld.IsClient) Client.Load(this.Config, this.Log);
    }

    public override bool Unload()
    {
        if (VWorld.IsServer) Server.Unload();
        if (VWorld.IsClient) Client.Unload();

        return false;
    }
}

public static class Server
{
    public static Harmony harmony;
    internal static void Load(ConfigFile config, ManualLogSource logger)
    {
        Settings.Config.Load(config, logger, "Server");

        // LocalDB.Load(); // TODO

        harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);

        Log.Trace("Patching harmony");
        harmony.PatchAll();

        Log.Info($"Plugin {MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} server side is loaded!");
    }

    internal static bool Unload()
    {
        harmony.UnpatchSelf();
        // LocalDB.Save();

        Log.Info($"Plugin {MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} server side is unloaded!");
        return true;
    }
}

internal static class Client
{
    internal static void Load(ConfigFile config, ManualLogSource logger)
    {
        Settings.Config.Load(config, logger, "Client");

        Log.Info($"Plugin {MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} client side is loaded!");
    }

    internal static void Unload()
    {
        Log.Info($"Plugin {MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} client side is unloaded!");
    }
}
