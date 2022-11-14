using Unity.Entities;
using Unity.Collections;
using ProjectM;

namespace Servant;

public static class Mission {
    // GetEntities of component type ActiveServantMission.
    public static NativeArray<Entity> GetEntities(EntityManager em) {
        var servantMissionsQuery = em.CreateEntityQuery(
                ComponentType.ReadWrite<ActiveServantMission>()
            );
        return servantMissionsQuery.ToEntityArray(Allocator.Temp);
    }

    // DivideProgressBy of servant missions.
    public static void DivideProgressBy(EntityManager em, Entity missionEntity, int reduction) {
        var missionBuffer = em.GetBuffer<ActiveServantMission>(missionEntity);

        for (int i = 0; i < missionBuffer.Length; i++) {
            var mission = missionBuffer[i];
            mission.MissionLength /= reduction;
            if (mission.MissionLength <= 0) {
                mission.MissionLength = 0;
            }
            missionBuffer[i] = mission;
        }
    }
}
