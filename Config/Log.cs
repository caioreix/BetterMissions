using BepInEx.Logging;
using System;
using System.IO;
using MissionControl;

namespace Config;

public class Log {
    public static ManualLogSource Logger;
    private static string TempLogFile;

    // Load the logs start configs.
    public static void Load(string worldType) {
        if (Env.LogOnTempFile.Value) {
            // TempLogFile = $"{System.IO.Path.GetTempPath()}{PluginInfo.PLUGIN_GUID}-{worldType}-{Guid.NewGuid().ToString()}.log";
            TempLogFile = $"{System.IO.Path.GetTempPath()}VRising-{PluginInfo.PLUGIN_GUID}-{worldType}.log";
            Env.LastLogTempFilePath.Value = TempLogFile;
            Env.Config.Save();

            Start($"Using \"{TempLogFile}\" to save logs.");
        }
    }

    // Info logs
    public static void Info(object data) {
        Logger.LogInfo(data);
        logOnFile(data, "Info   ");
    }

    // Error logs
    public static void Error(object data) {
        Logger.LogError(data);
        logOnFile(data, "Error  ");
    }

    // Debug logs
    public static void Debug(object data) {
        Logger.LogDebug(data);
        logOnFile(data, "Debug  ");
    }

    // Fatal logs
    public static void Fatal(object data) {
        Logger.LogFatal(data);
        logOnFile(data, "Fatal  ");
    }

    // Warning logs
    public static void Warning(object data) {
        Logger.LogWarning(data);
        logOnFile(data, "Warning");
    }

    // Message logs
    public static void Message(object data) {
        Logger.LogMessage(data);
        logOnFile(data, "Message");
    }

    // Start logs
    public static void Start(object data) {
        Logger.LogMessage(data);
        logOnFile(data, "Start  ", "\n");
    }

    // Trace logs
    public static void Trace(object data) {
        if (Env.EnableTraceLogs.Value) {
            Logger.LogDebug(data);
            logOnFile(data, "Trace  ");
        }
    }

    private static void logOnFile(object data, string level, string prefix = "") {
        if (Env.LogOnTempFile.Value) {
            using (StreamWriter w = File.AppendText(TempLogFile)) {
                var msg = $"{prefix}{DateTime.Now.ToString("hh:mm:ss")} [{PluginInfo.PLUGIN_GUID}: {level}]: {data}";
                w.WriteLine(msg);
            }
        }
    }
}