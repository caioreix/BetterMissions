using BepInEx.Configuration;

namespace BetterMissions.Settings;

public class ENV {
    // Server
    public static ConfigEntry<float> MissionReduceRate;
    public static ConfigEntry<bool> OfflineMissionProgress;

    public class Server {
        private static string serverSection = "🔧Server";

        public static void Setup() {
            Utils.Settings.Config.AddConfigActions(load);
        }

        // Load the plugin config variables.
        private static void load() {
            MissionReduceRate = Utils.Settings.Config.cfg.Bind(
                serverSection,
                nameof(MissionReduceRate),
                2F,
                "Define the mission reduce divisor. Ex: if you set 2, 2 hours will be 1 hour (0 will be replaced by 1)"
            );

            OfflineMissionProgress = Utils.Settings.Config.cfg.Bind(
               serverSection,
               nameof(OfflineMissionProgress),
               true,
               "Enabled, mission progress will be loaded even with the server offline."
           );

            validateValues();
        }

        private static void validateValues() {
            if (MissionReduceRate.Value == 0) MissionReduceRate.Value = 1;

            Utils.Settings.Config.cfg.Save();
        }
    }
}
