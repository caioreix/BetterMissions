


using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using BetterMissions.Systems;
using HarmonyLib;
using ProjectM;
using ProjectM.Network;
using Stunlock.Network;
using Unity.Entities;
using Utils.Logger;
using Utils.VRising;

namespace BetterMissions.Hooks.Server;

[HarmonyPatch]

public class ServerBootstrapSystemPatch {
    public static ConcurrentDictionary<ulong, PlayerInfo> PlayerInfoCache = new();
    public struct PlayerInfo(Entity userEntity = default, Entity charEntity = default, User user = default) {
        public User User { get; set; } = user;
        public Entity UserEntity { get; set; } = userEntity;
        public Entity CharEntity { get; set; } = charEntity;
    }

    [HarmonyPatch(typeof(ServerBootstrapSystem), nameof(ServerBootstrapSystem.OnUserConnected))]
    public class OnUserConnected {
        public static void Postfix(ServerBootstrapSystem __instance, NetConnectionId netConnectionId) {
            try {
                if (!__instance._NetEndPointToApprovedUserIndex.TryGetValue(netConnectionId, out int userIndex)) return;
                ServerBootstrapSystem.ServerClient serverClient = __instance._ApprovedUsersLookup[userIndex];
                Entity userEntity = serverClient.UserEntity;

                if (userEntity == Entity.Null) return;

                User user = userEntity.Read<User>();

                Mission.UpdateUserDataUI(user);

                Entity playerCharacter = user.LocalCharacter.GetEntityOnServer();
                ulong platformId = user.PlatformId;

                PlayerInfo playerInfo = new(userEntity, playerCharacter, user);

                PlayerInfoCache.TryAdd(platformId, playerInfo);
            } catch (Exception e) {
                Log.Fatal(e);
            }
        }
    }

    [HarmonyPatch(typeof(ServerBootstrapSystem), nameof(ServerBootstrapSystem.OnUserDisconnected))]
    public class OnUserDisconnected {
        public static void Postfix(ServerBootstrapSystem __instance, NetConnectionId netConnectionId) {
            try {
                if (!__instance._NetEndPointToApprovedUserIndex.TryGetValue(netConnectionId, out int userIndex)) return;
                ServerBootstrapSystem.ServerClient serverClient = __instance._ApprovedUsersLookup[userIndex];
                Entity userEntity = serverClient.UserEntity;

                if (userEntity == Entity.Null) return;

                User user = userEntity.Read<User>();
                ulong platformId = user.PlatformId;

                PlayerInfoCache.TryRemove(platformId, out _);
            } catch (Exception e) {
                Log.Fatal(e);
            }
        }
    }
}
