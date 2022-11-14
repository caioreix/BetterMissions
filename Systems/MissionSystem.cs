using Unity.Entities;
using Unity.Collections;
using ProjectM;

namespace Systems;

public static class Mission {
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
