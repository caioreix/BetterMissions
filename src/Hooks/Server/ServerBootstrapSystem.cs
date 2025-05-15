using System;
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
            } catch (Exception e) {
                Log.Fatal(e);
            }
        }
    }
}
