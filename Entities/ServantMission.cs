using Unity.Entities;
using Unity.Collections;

namespace Entities;

public static class ActiveServantMission {
    // GetEntities of component type ActiveServantMission.
    public static NativeArray<Entity> GetEntities(EntityManager em) {
        var servantMissionsQuery = em.CreateEntityQuery(
                ComponentType.ReadWrite<ProjectM.ActiveServantMission>()
            );
        return servantMissionsQuery.ToEntityArray(Allocator.Temp);
    }
}