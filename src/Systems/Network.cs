


using System;
using ProjectM;
using ProjectM.Network;
using Utils.Logger;

namespace BetterMissions.Systems;

public static class CustomNetwork {
    private static string _messagePrefix = "[BetterMissions] ";

    public enum MessageType {
        None,
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

    public static MessageType CheckMessage(string message, out string rawMessage) {
        return UnwrapMessage(message, out rawMessage);
    }

    public static bool IsPluginMessage(string message) {
        if (message.StartsWith(_messagePrefix)) {
            return true;
        }

        return false;
    }

    public static string WrapMessage(MessageType messageType, string message) {
        string wrappedMessage = $"{_messagePrefix}{messageType}:{EncodeMessage(message)}";

        return wrappedMessage;
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
        return System.Text.Encoding.UTF8.GetString(resultStream.ToArray());
    }

    public static MessageType UnwrapMessage(string message, out string rawMessage) {
        try {
            if (IsPluginMessage(message)) {
                string unwrappedMessage = message[_messagePrefix.Length..];
                string[] messageParts = unwrappedMessage.Split(':');
                if (messageParts.Length > 1) {
                    if (Enum.TryParse(messageParts[0], out MessageType messageType)) {
                        rawMessage = DecodeMessage(messageParts[1]);
                        return messageType;
                    }
                }
            }
        } catch (Exception e) {
            Log.Error(e);
        }

        rawMessage = message;
        return MessageType.None;
    }
}
