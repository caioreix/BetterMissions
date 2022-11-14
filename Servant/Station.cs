using Unity.Entities;
using Unity.Collections;
using ProjectM;

namespace Servant;

public static class Station {
    // GetEntities of component type ServantCoffinstation.
    public static NativeArray<Entity> GetEntities(EntityManager em) {
        var servantCoffinsQuery = em.CreateEntityQuery(
                    ComponentType.ReadWrite<ServantCoffinstation>()
                );
        return servantCoffinsQuery.ToEntityArray(Allocator.Temp);
    }
}