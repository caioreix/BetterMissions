using System;
using BetterMissions.Systems;
using HarmonyLib;
using ProjectM;
using ProjectM.Network;
using Stunlock.Core;
using Unity.Collections;
using Unity.Entities;
using Utils.Logger;
using Utils.VRising;
using Utils.VRising.Data;

namespace BetterMissions.Hooks.Server;

[HarmonyPatch]

public class BuffSystem_Spawn_ServerPatch {
    [HarmonyPatch(typeof(BuffSystem_Spawn_Server), nameof(BuffSystem_Spawn_Server.OnUpdate))]
    public class OnUpdate {
        public static void Prefix(BuffSystem_Spawn_Server __instance) {
            NativeArray<Entity> entities = __instance._Query.ToEntityArray(Allocator.Temp);
            try {
                foreach (Entity buffEntity in entities) {
                    try {
                        PrefabGUID guid = buffEntity.Read<PrefabGUID>();
                        Entity owner = buffEntity.Read<EntityOwner>().Owner;
                        if (!owner.Has<PlayerCharacter>()) continue;
                        PlayerCharacter playerCharacter = owner.Read<PlayerCharacter>();

                        if (guid != Prefabs.AB_Interact_Throne_Buff_Sit) {
                            continue;
                        }

                        User user = playerCharacter.UserEntity.Read<User>();

                        Log.Trace($"User: {user.CharacterName}:{user.Index} is sitting on the throne: {guid}");
                        Mission.UpdateUserDataUI(user);
                    } catch (Exception e) {
                        Log.Warning($"Failed to process buffEntity: {e}");
                    }
                }
            } finally {
                entities.Dispose();
            }
        }
    }
}
