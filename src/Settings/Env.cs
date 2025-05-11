using BetterMissions.Common;
using Utils.Settings;

namespace BetterMissions.Settings;

public static class ENV {
    // Mission_General
    private static string missionGeneral = "0.🚩 Mission";
    public static ConfigElement<bool> EnableMissionRaidStability;
    public static ConfigElement<ProjectM.RaidStability> MissionRaidStability;
    public static ConfigElement<float> MissionLengthModifier;
    public static ConfigElement<float> MissionSuccessRateBonusModifier;
    public static ConfigElement<float> MissionInjuryChanceModifier;
    public static ConfigElement<float> MissionLootFactorModifier;

    // Mission_Reckless1
    private static string missionReckless1 = "1.🚩 Mission-Reckless-1";
    private static ConfigElement<bool> EnableReckless1Settings;
    private static ConfigElement<int> Reckless1MissionLength;
    private static ConfigElement<ProjectM.RaidStability> Reckless1RaidStability;
    private static ConfigElement<float> Reckless1MissionSuccessRateBonus;
    private static ConfigElement<float> Reckless1MissionInjuryChance;
    private static ConfigElement<float> Reckless1MissionLootFactor;

    // Mission_Reckless2
    private static string missionReckless2 = "2.🚩 Mission-Reckless-2";
    private static ConfigElement<bool> EnableReckless2Settings;
    private static ConfigElement<int> Reckless2MissionLength;
    private static ConfigElement<ProjectM.RaidStability> Reckless2RaidStability;
    private static ConfigElement<float> Reckless2MissionSuccessRateBonus;
    private static ConfigElement<float> Reckless2MissionInjuryChance;
    private static ConfigElement<float> Reckless2MissionLootFactor;

    // Mission_Normal1
    private static string missionNormal1 = "3.🚩 Mission-Normal";
    private static ConfigElement<bool> EnableNormal1Settings;
    private static ConfigElement<int> Normal1MissionLength;
    private static ConfigElement<ProjectM.RaidStability> Normal1RaidStability;
    private static ConfigElement<float> Normal1MissionSuccessRateBonus;
    private static ConfigElement<float> Normal1MissionInjuryChance;
    private static ConfigElement<float> Normal1MissionLootFactor;

    // Mission_Prepared1
    private static string missionPrepared1 = "4.🚩 Mission-Prepared-1";
    private static ConfigElement<bool> EnablePrepared1Settings;
    private static ConfigElement<int> Prepared1MissionLength;
    private static ConfigElement<ProjectM.RaidStability> Prepared1RaidStability;
    private static ConfigElement<float> Prepared1MissionSuccessRateBonus;
    private static ConfigElement<float> Prepared1MissionInjuryChance;
    private static ConfigElement<float> Prepared1MissionLootFactor;

    // Mission_Prepared2
    private static string missionPrepared2 = "5.🚩 Mission-Prepared-2";
    private static ConfigElement<bool> EnablePrepared2Settings;
    private static ConfigElement<int> Prepared2MissionLength;
    private static ConfigElement<ProjectM.RaidStability> Prepared2RaidStability;
    private static ConfigElement<float> Prepared2MissionSuccessRateBonus;
    private static ConfigElement<float> Prepared2MissionInjuryChance;
    private static ConfigElement<float> Prepared2MissionLootFactor;


    public static class Mission {

        public static void Setup() {
            Utils.Settings.Config.AddConfigActions(load);
        }

        // Load the plugin config variables.
        private static void load() {
            // Mission_General
            MissionLengthModifier = Utils.Settings.Config.Bind(
                missionGeneral,
                nameof(MissionLengthModifier),
                Constants.Modifier.MissionLength,
                "Define the mission length modifier. (MissionLength / modifier)"
            );

            EnableMissionRaidStability = Utils.Settings.Config.Bind(
                missionGeneral,
                nameof(EnableMissionRaidStability),
                false,
                "Disabled, will use the default mission raid stability settings, if enabled, will use the MissionRaidStability type to all missions type."
            );

            MissionRaidStability = Utils.Settings.Config.Bind(
                missionGeneral,
                nameof(MissionRaidStability),
                Constants.Modifier.RaidStability,
                "Define the mission raid stability type. Only used if EnableMissionRaidStability is enabled."
            );

            MissionSuccessRateBonusModifier = Utils.Settings.Config.Bind(
                missionGeneral,
                nameof(MissionSuccessRateBonusModifier),
                Constants.Modifier.SuccessRateBonus,
                "Define the mission success rate bonus modifier. (MissionSuccessRateBonus / modifier)"
            );

            MissionInjuryChanceModifier = Utils.Settings.Config.Bind(
                missionGeneral,
                nameof(MissionInjuryChanceModifier),
                Constants.Modifier.InjuryChance,
                "Define the mission injury chance bonus modifier. (MissionInjuryChance / modifier)"
            );

            MissionLootFactorModifier = Utils.Settings.Config.Bind(
                missionGeneral,
                nameof(MissionLootFactorModifier),
                Constants.Modifier.LootFactor,
                "Define the mission loot factor bonus modifier. (MissionLootFactor / modifier)"
            );

            // Mission_Reckless1
            EnableReckless1Settings = Utils.Settings.Config.Bind(
                missionReckless1,
                nameof(EnableReckless1Settings),
                false,
                "Enabled, will use the specific mission level settings"
            );

            Reckless1MissionLength = Utils.Settings.Config.Bind(
                missionReckless1,
                nameof(Reckless1MissionLength),
                Constants.Reckless1.MissionLength,
                "Define the Reckless1 mission length in seconds."
            );
            Reckless1RaidStability = Utils.Settings.Config.Bind(
                missionReckless1,
                nameof(Reckless1RaidStability),
                Constants.Reckless1.RaidStability,
                "Define the Reckless1 raid stability."
            );
            Reckless1MissionSuccessRateBonus = Utils.Settings.Config.Bind(
                missionReckless1,
                nameof(Reckless1MissionSuccessRateBonus),
                Constants.Reckless1.SuccessRateBonus,
                "Define the Reckless1 mission success rate bonus."
            );
            Reckless1MissionInjuryChance = Utils.Settings.Config.Bind(
                missionReckless1,
                nameof(Reckless1MissionInjuryChance),
                Constants.Reckless1.InjuryChance,
                "Define the Reckless1 mission injury chance bonus."
            );
            Reckless1MissionLootFactor = Utils.Settings.Config.Bind(
                missionReckless1,
                nameof(Reckless1MissionLootFactor),
                Constants.Reckless1.LootFactor,
                "Define the Reckless1 mission loot factor bonus."
            );

            // Mission_Reckless2
            EnableReckless2Settings = Utils.Settings.Config.Bind(
                missionReckless2,
                nameof(EnableReckless2Settings),
                false,
                "Enabled, will use the specific mission level settings"
            );

            Reckless2MissionLength = Utils.Settings.Config.Bind(
                missionReckless2,
                nameof(Reckless2MissionLength),
                Constants.Reckless2.MissionLength,
                "Define the Reckless2 mission length in seconds."
            );
            Reckless2RaidStability = Utils.Settings.Config.Bind(
                missionReckless2,
                nameof(Reckless2RaidStability),
                Constants.Reckless2.RaidStability,
                "Define the Reckless2 raid stability."
            );
            Reckless2MissionSuccessRateBonus = Utils.Settings.Config.Bind(
                missionReckless2,
                nameof(Reckless2MissionSuccessRateBonus),
                Constants.Reckless2.SuccessRateBonus,
                "Define the Reckless2 mission success rate bonus."
            );
            Reckless2MissionInjuryChance = Utils.Settings.Config.Bind(
                missionReckless2,
                nameof(Reckless2MissionInjuryChance),
                Constants.Reckless2.InjuryChance,
                "Define the Reckless2 mission injury chance bonus."
            );
            Reckless2MissionLootFactor = Utils.Settings.Config.Bind(
                missionReckless2,
                nameof(Reckless2MissionLootFactor),
                Constants.Reckless2.LootFactor,
                "Define the Reckless2 mission loot factor bonus."
            );

            // Mission_Normal1
            EnableNormal1Settings = Utils.Settings.Config.Bind(
                missionNormal1,
                nameof(EnableNormal1Settings),
                false,
                "Enabled, will use the specific mission level settings"
            );

            Normal1MissionLength = Utils.Settings.Config.Bind(
                missionNormal1,
                nameof(Normal1MissionLength),
                Constants.Normal1.MissionLength,
                "Define the Normal1 mission length in seconds."
            );
            Normal1RaidStability = Utils.Settings.Config.Bind(
                missionNormal1,
                nameof(Normal1RaidStability),
                Constants.Normal1.RaidStability,
                "Define the Normal1 raid stability."
            );
            Normal1MissionSuccessRateBonus = Utils.Settings.Config.Bind(
                missionNormal1,
                nameof(Normal1MissionSuccessRateBonus),
                Constants.Normal1.SuccessRateBonus,
                "Define the Normal1 mission success rate bonus."
            );
            Normal1MissionInjuryChance = Utils.Settings.Config.Bind(
                missionNormal1,
                nameof(Normal1MissionInjuryChance),
                Constants.Normal1.InjuryChance,
                "Define the Normal1 mission injury chance bonus."
            );
            Normal1MissionLootFactor = Utils.Settings.Config.Bind(
                missionNormal1,
                nameof(Normal1MissionLootFactor),
                Constants.Normal1.LootFactor,
                "Define the Normal1 mission loot factor bonus."
            );

            // Mission_Prepared1
            EnablePrepared1Settings = Utils.Settings.Config.Bind(
                missionPrepared1,
                nameof(EnablePrepared1Settings),
                false,
                "Enabled, will use the specific mission level settings"
            );

            Prepared1MissionLength = Utils.Settings.Config.Bind(
                missionPrepared1,
                nameof(Prepared1MissionLength),
                Constants.Prepared1.MissionLength,
                "Define the Prepared1 mission length in seconds."
            );
            Prepared1RaidStability = Utils.Settings.Config.Bind(
                missionPrepared1,
                nameof(Prepared1RaidStability),
                Constants.Prepared1.RaidStability,
                "Define the Prepared1 raid stability."
            );
            Prepared1MissionSuccessRateBonus = Utils.Settings.Config.Bind(
                missionPrepared1,
                nameof(Prepared1MissionSuccessRateBonus),
                Constants.Prepared1.SuccessRateBonus,
                "Define the Prepared1 mission success rate bonus."
            );
            Prepared1MissionInjuryChance = Utils.Settings.Config.Bind(
                missionPrepared1,
                nameof(Prepared1MissionInjuryChance),
                Constants.Prepared1.InjuryChance,
                "Define the Prepared1 mission injury chance bonus."
            );
            Prepared1MissionLootFactor = Utils.Settings.Config.Bind(
                missionPrepared1,
                nameof(Prepared1MissionLootFactor),
                Constants.Prepared1.LootFactor,
                "Define the Prepared1 mission loot factor bonus."
            );

            // Mission_Prepared2
            EnablePrepared2Settings = Utils.Settings.Config.Bind(
                missionPrepared2,
                nameof(EnablePrepared2Settings),
                false,
                "Enabled, will use the specific mission level settings"
            );

            Prepared2MissionLength = Utils.Settings.Config.Bind(
                missionPrepared2,
                nameof(Prepared2MissionLength),
                Constants.Prepared2.MissionLength,
                "Define the Prepared2 mission length in seconds."
            );
            Prepared2RaidStability = Utils.Settings.Config.Bind(
                missionPrepared2,
                nameof(Prepared2RaidStability),
                Constants.Prepared2.RaidStability,
                "Define the Prepared2 raid stability."
            );
            Prepared2MissionSuccessRateBonus = Utils.Settings.Config.Bind(
                missionPrepared2,
                nameof(Prepared2MissionSuccessRateBonus),
                Constants.Prepared2.SuccessRateBonus,
                "Define the Prepared2 mission success rate bonus."
            );
            Prepared2MissionInjuryChance = Utils.Settings.Config.Bind(
                missionPrepared2,
                nameof(Prepared2MissionInjuryChance),
                Constants.Prepared2.InjuryChance,
                "Define the Prepared2 mission injury chance bonus."
            );
            Prepared2MissionLootFactor = Utils.Settings.Config.Bind(
                missionPrepared2,
                nameof(Prepared2MissionLootFactor),
                Constants.Prepared2.LootFactor,
                "Define the Prepared2 mission loot factor bonus."
            );

            validateValues();
        }

        private static void validateValues() {
            // Mission_Reckless1
            if (EnableReckless1Settings.Value) {
                Database.Mission.Settings.TryAdd(
                    Constants.Reckless1.Name,
                    new Database.Mission.Setting() {
                        RaidStability = Reckless1RaidStability.Value,
                        InjuryChance = Reckless1MissionInjuryChance.Value,
                        LootFactor = Reckless1MissionLootFactor.Value,
                        MissionLength = Reckless1MissionLength.Value,
                        SuccessRateBonus = Reckless1MissionSuccessRateBonus.Value,
                    }
                );
            }

            // Mission_Reckless2
            if (EnableReckless2Settings.Value) {
                Database.Mission.Settings.TryAdd(
                   Constants.Reckless2.Name,
                    new Database.Mission.Setting() {
                        RaidStability = Reckless2RaidStability.Value,
                        InjuryChance = Reckless2MissionInjuryChance.Value,
                        LootFactor = Reckless2MissionLootFactor.Value,
                        MissionLength = Reckless2MissionLength.Value,
                        SuccessRateBonus = Reckless2MissionSuccessRateBonus.Value,
                    }
                );
            }

            // Mission_Normal1
            if (EnableNormal1Settings.Value) {
                Database.Mission.Settings.TryAdd(
                    Constants.Normal1.Name,
                    new Database.Mission.Setting() {
                        RaidStability = Normal1RaidStability.Value,
                        InjuryChance = Normal1MissionInjuryChance.Value,
                        LootFactor = Normal1MissionLootFactor.Value,
                        MissionLength = Normal1MissionLength.Value,
                        SuccessRateBonus = Normal1MissionSuccessRateBonus.Value,
                    }
                );
            }

            // Mission_Prepared1
            if (EnablePrepared1Settings.Value) {
                Database.Mission.Settings.TryAdd(
                    Constants.Prepared1.Name,
                    new Database.Mission.Setting() {
                        RaidStability = Prepared1RaidStability.Value,
                        InjuryChance = Prepared1MissionInjuryChance.Value,
                        LootFactor = Prepared1MissionLootFactor.Value,
                        MissionLength = Prepared1MissionLength.Value,
                        SuccessRateBonus = Prepared1MissionSuccessRateBonus.Value,
                    }
                );
            }

            // Mission_Prepared2
            if (EnablePrepared2Settings.Value) {
                Database.Mission.Settings.TryAdd(
                    Constants.Prepared2.Name,
                    new Database.Mission.Setting() {
                        RaidStability = Prepared2RaidStability.Value,
                        InjuryChance = Prepared2MissionInjuryChance.Value,
                        LootFactor = Prepared2MissionLootFactor.Value,
                        MissionLength = Prepared2MissionLength.Value,
                        SuccessRateBonus = Prepared2MissionSuccessRateBonus.Value,
                    }
                );
            }

            Utils.Settings.Config.Save();
        }
    }
}
