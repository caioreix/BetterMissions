using Unity.Entities;
using Unity.Collections;
using ProjectM;

namespace Entities;

public static class ServantCoffin {
    // GetEntities of component type ServantCoffinstation.
    public static NativeArray<Entity> GetEntities(EntityManager em) {
        var servantCoffinsQuery = em.CreateEntityQuery(
                    ComponentType.ReadWrite<ServantCoffinstation>()
                );
        return servantCoffinsQuery.ToEntityArray(Allocator.Temp);
    }
}