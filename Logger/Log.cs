namespace Logger;

public class Log {
    // Info logs
    public static void Info(object data) {
        Config.logger.LogInfo(data);
        Config.logFile(data, "Info   ");
    }

    // Error logs
    public static void Error(object data) {
        Config.logger.LogError(data);
        Config.logFile(data, "Error  ");
    }

    // Debug logs
    public static void Debug(object data) {
        Config.logger.LogDebug(data);
        Config.logFile(data, "Debug  ");
    }

    // Fatal logs
    public static void Fatal(object data) {
        Config.logger.LogFatal(data);
        Config.logFile(data, "Fatal  ");
    }

    // Warning logs
    public static void Warning(object data) {
        Config.logger.LogWarning(data);
        Config.logFile(data, "Warning");
    }

    // Message logs
    public static void Message(object data) {
        Config.logger.LogMessage(data);
        Config.logFile(data, "Message");
    }

    // Start logs
    public static void Start(object data) {
        Config.logger.LogMessage(data);
        Config.logFile(data, "Start  ", "\n");
    }

    // Trace logs
    public static void Trace(object data) {
        if (Config.traceLevel) {
            Config.logger.LogDebug(data);
            Config.logFile(data, "Trace  ");
        }
    }
}