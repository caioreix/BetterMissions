using System;
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

            try {
                for (int i = 0; i < entities.Length; i++) {
                    try {
                        Entity entity = entities[i];
                        ChatMessageEvent chatMessageEvent = entity.Read<ChatMessageEvent>();
                        FromCharacter fromCharacter = entity.Read<FromCharacter>();
                        Entity userEntity = fromCharacter.User;

                        string message = chatMessageEvent.MessageText.Value;

                        if (IsCommandMessage(message)) {
                            HandleClientMessage(message, userEntity);
                            entity.Destroy();
                        }
                    } catch (Exception e) { Log.Error($"Error processing message: {e}"); }
                }
            } finally {
                entities.Dispose();
            }
        }
    }

    private static void HandleClientMessage(string message, Entity userEntity) {
        ProcessCommand(message, userEntity);
    }
}
