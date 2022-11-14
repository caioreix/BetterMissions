using System;
using Unity.Entities;
using ProjectM;
using Settings;
using Entities;

using Database; // FIXME

namespace Systems;

public static class Injury {

    // ReduceAllNewInjuriesTimeProgress based on passed values
    public static void ReduceAllNewInjuriesTimeProgress(EntityManager em, float reduction) {
        var servantEntities = ServantCoffin.GetEntities(em);
        foreach (var servantEntity in servantEntities) {
            reduceNewInjuriesTimeProgress(em, servantEntity, reduction);
        }
    }

    private static void reduceNewInjuriesTimeProgress(EntityManager em, Entity injuryEntity, float reduction) {
        var servant = em.GetComponentData<ServantCoffinstation>(injuryEntity);
        var currentTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();

        if (injuryAlreadyFinished(ref servant, currentTimestamp)) {
            return;
        }

        reduceInjuryProgress(ref servant, reduction);
    }

    private static bool injuryAlreadyFinished(ref ServantCoffinstation servant, long currentTimestamp) {
        if (Database.Injury.Progress.TryGetValue(servant.ServantName.ToString(), out long timestamp)) {
            if (Env.OfflineInjuryProgress.Value && currentTimestamp >= timestamp) {
                servant.RecuperateEndTime = 0;

                Database.Injury.Progress.TryRemove(servant.ServantName.ToString(), out _);
                DB.Save(); // FIXME
            }
            return true;
        }
        return false;
    }

    private static void reduceInjuryProgress(ref ServantCoffinstation servant, float reduction) {
        servant.RecuperateEndTime /= reduction;

        Database.Injury.Progress.TryAdd(
            servant.ServantName.ToString(),
            injuryEndTimestamp(servant.RecuperateEndTime)
        );
        DB.Save(); // FIXME
    }

    private static long injuryEndTimestamp(double recuperateEndTime) {
        var timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        var recuperateEndTimeLong = Convert.ToInt64(recuperateEndTime);

        return timestamp + recuperateEndTimeLong;
    }
}