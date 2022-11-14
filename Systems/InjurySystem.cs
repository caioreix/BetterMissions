using Unity.Entities;
using ProjectM;
using Entities;

namespace Systems;

public static class Injury {
    // DivideProgressBy of servant injury.
    public static void DivideProgressBy(EntityManager em, int reduction) {
        var coffinEntities = ServantCoffin.GetEntities(em);

        foreach (var coffinEntity in coffinEntities) {
            var coffinStation = em.GetComponentData<ServantCoffinstation>(coffinEntity);

            if (coffinStation.State == ServantCoffinState.Converting) {
                coffinStation.RecuperateEndTime /= reduction;
                if (coffinStation.RecuperateEndTime <= 0) {
                    coffinStation.RecuperateEndTime = 0;
                }
                em.SetComponentData<ServantCoffinstation>(coffinEntity, coffinStation);
            }
        }
    }
}