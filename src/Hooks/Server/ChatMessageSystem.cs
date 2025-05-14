
using BetterMissions.Systems;
using HarmonyLib;
using ProjectM;
using ProjectM.Network;
using Unity.Collections;
using Unity.Entities;
using Utils.Logger;
using Utils.VRising;
using static BetterMissions.Systems.CustomNetwork;

namespace BetterMissions.Hooks.Server;

[HarmonyPatch]

public class ChatMessageSystemPatch {
    [HarmonyPatch(typeof(ChatMessageSystem), nameof(ChatMessageSystem.OnUpdate))]
    public class OnUpdate {
        public static void Prefix(ChatMessageSystem __instance) {
            NativeArray<Entity> entities = __instance.EntityQueries[0].ToEntityArray(Allocator.Temp);
            NativeArray<ChatMessageEvent> chatMessageEvents = __instance.EntityQueries[0].ToComponentDataArray<ChatMessageEvent>(Allocator.Temp);

            try {
                for (int i = 0; i < entities.Length; i++) {
                    Entity entity = entities[i];
                    ChatMessageEvent chatMessageEvent = chatMessageEvents[i];
                    Log.Trace($"ChatMessageEvent: {chatMessageEvent.MessageText.Value}");

                    MessageType messageType = CheckMessage(chatMessageEvent.MessageText.Value, out string unwrappedMessage);
                    if (messageType != MessageType.None) {
                        HandleClientMessage(messageType, unwrappedMessage);
                        entity.Destroy();
                    }
                }
            } finally {
                entities.Dispose();
                chatMessageEvents.Dispose();
            }
        }

        private static void HandleClientMessage(MessageType messageType, string message) {
            ProcessMessage(messageType, message);
        }
    }
}
