using System;
using Unity.Entities;
using ProjectM;
using Settings;
using Entities;
using Logger;
using Unity.Collections;

namespace Systems;

public static class Injury {
    // ReduceAllNewInjuriesTimeProgress based on passed values
    public static void ReduceAllNewInjuriesTimeProgress(EntityManager em, float reduction) {
        var servantEntities = ServantCoffin.GetEntities(em);

        // Just garbage unused data if the entities are loaded
        if (servantEntities.Length > 0) {
            garbageCollector(em, servantEntities);
        }

        foreach (var servantEntity in servantEntities) {
            reduceNewInjuriesTimeProgress(em, servantEntity, reduction);
        }
    }

    private static void garbageCollector(EntityManager em, NativeArray<Entity> servantEntities) {
        foreach (var injuryProgress in Database.Mission.Progress) {
            var key = injuryProgress.Key;
            if (!existInjuryKey(em, servantEntities, key)) {
                Database.Mission.Progress.TryRemove(key, out _);
                Log.Trace($"Garbage injury remove: {key}");
            }
        }
    }

    private static bool existInjuryKey(EntityManager em, NativeArray<Entity> servantEntities, string key) {
        foreach (var servantEntity in servantEntities) {
            var servant = em.GetComponentData<ServantCoffinstation>(servantEntity);
            if (getServantKey(servant) == key) {
                return true;
            }
        }
        return false;
    }

    private static string getServantKey(ServantCoffinstation servant) {
        return servant.Injury.ToString();
    }

    private static void reduceNewInjuriesTimeProgress(EntityManager em, Entity injuryEntity, float reduction) {
        var servant = em.GetComponentData<ServantCoffinstation>(injuryEntity);
        var key = getServantKey(servant);

        if (injuryAlreadyFinished(ref servant, key)) {
            return;
        }

        reduceInjuryProgress(ref servant, key, reduction);
    }

    private static bool injuryAlreadyFinished(ref ServantCoffinstation servant, string key) {
        var currentTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();

        if (Database.Injury.Progress.TryGetValue(key, out long timestamp)) {
            if (Env.OfflineInjuryProgress.Value && currentTimestamp >= timestamp) {
                setInjuryLength(ref servant, 0);

                Database.Injury.Progress.TryRemove(key, out _);
                Log.Trace($"Finished injury remove: {key}");
            }
            return true;
        }
        return false;
    }

    private static double getInjuryLength(ServantCoffinstation servant) {
        return servant.RecuperateEndTime;
    }

    private static void setInjuryLength(ref ServantCoffinstation servant, double value) {
        servant.RecuperateEndTime = value;
    }

    private static void reduceInjuryProgress(ref ServantCoffinstation servant, string key, float reduction) {
        var newInjuryLength = getInjuryLength(servant) / reduction;
        setInjuryLength(ref servant, newInjuryLength);

        var newEndTimestamp = DateTimeOffset.Now.AddSeconds(getInjuryLength(servant)).ToUnixTimeSeconds();
        Database.Injury.Progress.TryAdd(
            key,
            newEndTimestamp
        );
        Log.Trace($"New injury added: {key}: {newEndTimestamp}");
    }
}