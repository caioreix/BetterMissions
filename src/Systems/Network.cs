using System;
using System.Text;
using ProjectM;
using ProjectM.Network;
using Unity.Entities;
using Utils.Logger;
using Utils.VRising;

namespace BetterMissions.Systems;

public static class CustomNetwork {
    private const string _messagePrefix = $"[{MyPluginInfo.PLUGIN_NAME}]";
    private const string _commandPrefix = $"bm.";
    public const string _disableMessage = $"{_commandPrefix}ui.disable";
    public const string _enableCommand = $"{_commandPrefix}ui.enable";
    private const string _messageWarn = $"If you see this, you need to install {MyPluginInfo.PLUGIN_NAME} v{_pluginVersion} on client side or type '{_disableMessage}'!";
    private const string _pluginVersion = MyPluginInfo.PLUGIN_VERSION;
    private const string _separator = ":";

    private readonly static string[] _commandList = {
        _disableMessage,
        _enableCommand
    };

    private enum MessageParts {
        Prefix = 0,
        Warning = 1,
        Version = 2,
        MessageType = 3,
        Message = 4
    }

    public enum MessageType {
        InvalidMessage,
        UpdateClientMissionData,
    }

    public static void SendSystemMessageToClient(User user, string message) {
        Unity.Collections.FixedString512Bytes fixedMessage = new(message);

        Log.Trace($"Sending system message '{message}' to user({user.CharacterName}:{user.Index})");

        ServerChatUtils.SendSystemMessageToClient(
            Utils.VRising.Entities.EntityManager.Get(),
            user,
            ref fixedMessage
        );
    }

    public static void SendLocalMessage(string message, ServerChatMessageType messageType = ServerChatMessageType.Local) {
        Log.Trace($"Sending local message '{message}'");

        ClientSystemChatUtils.AddLocalMessage(
            Utils.VRising.Entities.EntityManager.Get(),
            message,
            messageType
        );
    }

    public static void ProcessMessage(MessageType messageType, string message) {
        switch (messageType) {
            case MessageType.UpdateClientMissionData:
                Mission.HandleUpdateMissionDataMessage(message);
                break;
            default:
                Log.Error($"Unknown message type: {messageType}");
                break;
        }
    }

    public static void ProcessCommand(string message, Entity userEntity) {
        User user = userEntity.Read<User>();
        Log.Trace($"Message from {user.CharacterName}:{user.Index} (Steam ID: {user.PlatformId}) content: {message}");

        switch (message) {
            case _disableMessage:
                Mission.DisableUserUISync(user);
                break;
            case _enableCommand:
                Mission.EnableUserUISync(user);
                break;
            default:
                string closestCommand = FindClosestCommand(message);
                string suggestion = string.IsNullOrEmpty(closestCommand) ? "" : $" Did you mean '{closestCommand}'?";
                SendSystemMessageToClient(user, $"Invalid command: '{message}'.{suggestion}");
                break;
        }
    }

    public static MessageType CheckMessage(string message, out string rawMessage) {
        return UnwrapMessage(message, out rawMessage);
    }

    public static string WrapMessage(MessageType messageType, string message) {
        string wrappedMessage = $"{_messagePrefix}{_separator}{_messageWarn}{_separator}{_pluginVersion}{_separator}{messageType}{_separator}{EncodeMessage(message)}";

        return wrappedMessage;
    }

    public static MessageType UnwrapMessage(string message, out string rawMessage) {
        rawMessage = message;
        try {
            if (IsPluginMessage(message)) {
                string[] messageParts = message.Split(_separator);
                if (messageParts.Length != 5) {
                    return MessageType.InvalidMessage;
                }

                if (messageParts[(int)MessageParts.Version] != _pluginVersion) {
                    return MessageType.InvalidMessage;
                }

                if (Enum.TryParse(messageParts[(int)MessageParts.MessageType], out MessageType messageType)) {
                    rawMessage = DecodeMessage(messageParts[(int)MessageParts.Message]);
                    return messageType;
                }
            }
        } catch (Exception e) {
            Log.Error(e);
        }

        return MessageType.InvalidMessage;
    }

    public static bool IsPluginMessage(string message) {
        if (!message.StartsWith(_messagePrefix)) {
            return false;
        }

        return true;
    }

    public static bool IsCommandMessage(string message) {
        if (!message.StartsWith(_commandPrefix)) {
            return false;
        }
        if (message == _commandPrefix) {
            return false;
        }

        return true;
    }

    private static string EncodeMessage(string message) {
        using var memoryStream = new System.IO.MemoryStream();
        using (var gzipStream = new System.IO.Compression.GZipStream(memoryStream, System.IO.Compression.CompressionLevel.Fastest, leaveOpen: true)) {
            byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
            gzipStream.Write(messageBytes, 0, messageBytes.Length);
        }
        return Convert.ToBase64String(memoryStream.ToArray());
    }

    private static string DecodeMessage(string encodedMessage) {
        byte[] compressedBytes = Convert.FromBase64String(encodedMessage);
        using var memoryStream = new System.IO.MemoryStream(compressedBytes);
        using var gzipStream = new System.IO.Compression.GZipStream(memoryStream, System.IO.Compression.CompressionMode.Decompress);
        using var resultStream = new System.IO.MemoryStream();
        gzipStream.CopyTo(resultStream);
        return Encoding.UTF8.GetString(resultStream.ToArray());
    }

    private static string FindClosestCommand(string input) {
        if (string.IsNullOrEmpty(input))
            return null;

        string closestMatch = null;
        int minDistance = int.MaxValue;

        foreach (var command in _commandList) {
            int distance = LevenshteinDistance(input.ToLower(), command.ToLower());
            if (distance < minDistance) {
                minDistance = distance;
                closestMatch = command;
            }
        }

        // Only suggest if the distance is reasonable
        return minDistance <= 5 ? closestMatch : null;
    }

    private static int LevenshteinDistance(string s, string t) {
        int n = s.Length;
        int m = t.Length;
        int[,] d = new int[n + 1, m + 1];

        if (n == 0) return m;
        if (m == 0) return n;

        for (int i = 0; i <= n; i++)
            d[i, 0] = i;

        for (int j = 0; j <= m; j++)
            d[0, j] = j;

        for (int j = 1; j <= m; j++) {
            for (int i = 1; i <= n; i++) {
                int cost = (s[i - 1] == t[j - 1]) ? 0 : 1;
                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost);
            }
        }

        return d[n, m];
    }
}
