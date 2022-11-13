using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using Wetstone.API;

namespace MissionControl;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
[BepInDependency("xyz.molenzwiebel.wetstone")]
public class Plugin : BasePlugin
{
    public static Harmony harmony;

    public override void Load()
    {
        global::Config.Log.Logger = this.Log;
        global::Config.Env.Config = this.Config;

        var wType = getWorldType();

        global::Config.Env.Load();
        global::Config.Log.Load(wType);

        if (VWorld.IsServer)
        {
            harmony = new Harmony(PluginInfo.PLUGIN_GUID);

            harmony.PatchAll();

            global::Config.Log.Info($"Plugin {PluginInfo.PLUGIN_GUID} v{PluginInfo.PLUGIN_VERSION} server site is loaded!");
        }

        if (VWorld.IsClient)
        {
            global::Config.Log.Info($"Plugin {PluginInfo.PLUGIN_GUID} v{PluginInfo.PLUGIN_VERSION} client side is loaded!");
        }
    }

    public override bool Unload()
    {
        harmony.UnpatchSelf();

        global::Config.Log.Info($"Plugin {PluginInfo.PLUGIN_GUID} v{PluginInfo.PLUGIN_VERSION} is unloaded!");
        return true;
    }

    private static string getWorldType()
    {
        if (VWorld.IsClient)
        {
            return "Client";
        }
        if (VWorld.IsServer)
        {
            return "Server";
        }

        return "Untyped";
    }
}

