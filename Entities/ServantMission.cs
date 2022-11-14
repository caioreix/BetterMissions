using Unity.Entities;
using Unity.Collections;
using ProjectM;

namespace Entities;

public static class ServantMission {
    // GetEntities of component type ActiveServantMission.
    public static NativeArray<Entity> GetEntities(EntityManager em) {
        var servantMissionsQuery = em.CreateEntityQuery(
                ComponentType.ReadWrite<ActiveServantMission>()
            );
        return servantMissionsQuery.ToEntityArray(Allocator.Temp);
    }
}