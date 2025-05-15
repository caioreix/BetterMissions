using System;
using BetterMissions.Systems;
using HarmonyLib;
using ProjectM;
using ProjectM.Network;
using ProjectM.UI;
using Unity.Collections;
using Unity.Entities;
using Utils.Database;
using Utils.Logger;
using Utils.VRising;
using static BetterMissions.Systems.CustomNetwork;

namespace BetterMissions.Hooks.Client;

public class ClientChatSystemPatch {
    [HarmonyPatch(typeof(ClientChatSystem), nameof(ClientChatSystem.OnUpdate))]
    public class OnUpdate {
        public static void Prefix(ClientChatSystem __instance) {
            NativeArray<Entity> entities = __instance._ReceiveChatMessagesQuery.ToEntityArray(Allocator.Temp);
            try {
                foreach (Entity entity in entities) {
                    try {
                        if (entity.Has<ChatMessageServerEvent>()) {
                            ChatMessageServerEvent chatMessage = entity.Read<ChatMessageServerEvent>();

                            MessageType messageType = CheckMessage(chatMessage.MessageText.Value, out string unwrappedMessage);
                            if (messageType != MessageType.None) {
                                HandleServerMessage(messageType, unwrappedMessage);
                                entity.Destroy();
                            }
                        }
                    } catch (Exception) {
                        Log.Warning($"ClientChatSystemPatch.OnUpdate: failed to process entity: {entity}");
                    }
                }
            } finally {
                entities.Dispose();
            }
        }
    }

    private static void HandleServerMessage(MessageType messageType, string message) {
        ProcessMessage(messageType, message);
    }
}
