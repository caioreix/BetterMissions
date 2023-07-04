using System.Collections.Generic;
using BepInEx.Configuration;

namespace BetterMissions.Settings;

public static class ENV {
    // Mission_General
    private static string missionGeneral = "🚩Mission";
    public static ConfigEntry<bool> OfflineMissionProgress;
    public static ConfigEntry<float> MissionLengthModifier;
    public static ConfigEntry<float> MissionSuccessRateBonusModifier;
    public static ConfigEntry<float> MissionInjuryChanceModifier;
    public static ConfigEntry<float> MissionLootFactorModifier;

    // Mission_Reckless1
    private static string missionReckless1 = "🚩Mission-Reckless1";
    private static ConfigEntry<bool> EnableReckless1Settings;
    private static ConfigEntry<float> Reckless1MissionLength;
    private static ConfigEntry<float> Reckless1MissionSuccessRateBonus;
    private static ConfigEntry<float> Reckless1MissionInjuryChance;
    private static ConfigEntry<float> Reckless1MissionLootFactor;

    // Mission_Reckless2
    private static string missionReckless2 = "🚩Mission-Reckless2";
    private static ConfigEntry<bool> EnableReckless2Settings;
    private static ConfigEntry<float> Reckless2MissionLength;
    private static ConfigEntry<float> Reckless2MissionSuccessRateBonus;
    private static ConfigEntry<float> Reckless2MissionInjuryChance;
    private static ConfigEntry<float> Reckless2MissionLootFactor;

    // Mission_Normal1
    private static string missionNormal1 = "🚩Mission-Normal1";
    private static ConfigEntry<bool> EnableNormal1Settings;
    private static ConfigEntry<float> Normal1MissionLength;
    private static ConfigEntry<float> Normal1MissionSuccessRateBonus;
    private static ConfigEntry<float> Normal1MissionInjuryChance;
    private static ConfigEntry<float> Normal1MissionLootFactor;

    // Mission_Prepared1
    private static string missionPrepared1 = "🚩Mission-Prepared1";
    private static ConfigEntry<bool> EnablePrepared1Settings;
    private static ConfigEntry<float> Prepared1MissionLength;
    private static ConfigEntry<float> Prepared1MissionSuccessRateBonus;
    private static ConfigEntry<float> Prepared1MissionInjuryChance;
    private static ConfigEntry<float> Prepared1MissionLootFactor;

    // Mission_Prepared2
    private static string missionPrepared2 = "🚩Mission-Prepared2";
    private static ConfigEntry<bool> EnablePrepared2Settings;
    private static ConfigEntry<float> Prepared2MissionLength;
    private static ConfigEntry<float> Prepared2MissionSuccessRateBonus;
    private static ConfigEntry<float> Prepared2MissionInjuryChance;
    private static ConfigEntry<float> Prepared2MissionLootFactor;


    public static class Mission {

        public static void Setup() {
            Utils.Settings.Config.AddConfigActions(load);
        }

        // Load the plugin config variables.
        private static void load() {
            // Mission_General
            OfflineMissionProgress = Utils.Settings.Config.cfg.Bind(
                missionGeneral,
                nameof(OfflineMissionProgress),
                true,
                "Enabled, mission progress will be loaded even with the server offline."
            );

            MissionLengthModifier = Utils.Settings.Config.cfg.Bind(
                missionGeneral,
                nameof(MissionLengthModifier),
                2F,
                "Define the mission length modifier. (MissionLength / modifier)"
            );

            MissionSuccessRateBonusModifier = Utils.Settings.Config.cfg.Bind(
                missionGeneral,
                nameof(MissionSuccessRateBonusModifier),
                1F,
                "Define the mission success rate bonus modifier. (MissionSuccessRateBonus / modifier)"
            );

            MissionInjuryChanceModifier = Utils.Settings.Config.cfg.Bind(
                missionGeneral,
                nameof(MissionInjuryChanceModifier),
                1F,
                "Define the mission injury chance bonus modifier. (MissionInjuryChance / modifier)"
            );

            MissionLootFactorModifier = Utils.Settings.Config.cfg.Bind(
                missionGeneral,
                nameof(MissionLootFactorModifier),
                1F,
                "Define the mission loot factor bonus modifier. (MissionLootFactor / modifier)"
            );

            // Mission_Reckless1
            EnableReckless1Settings = Utils.Settings.Config.cfg.Bind(
                missionReckless1,
                nameof(EnableReckless1Settings),
                false,
                "Enabled, will use the specific mission level settings"
            );

            Reckless1MissionLength = Utils.Settings.Config.cfg.Bind(
                missionReckless1,
                nameof(Reckless1MissionLength),
                7200F,
                "Define the Reckless1 mission length in seconds."
            );
            Reckless1MissionSuccessRateBonus = Utils.Settings.Config.cfg.Bind(
                missionReckless1,
                nameof(Reckless1MissionSuccessRateBonus),
                -0.2F,
                "Define the Reckless1 mission success rate bonus."
            );
            Reckless1MissionInjuryChance = Utils.Settings.Config.cfg.Bind(
                missionReckless1,
                nameof(Reckless1MissionInjuryChance),
                0.3F,
                "Define the Reckless1 mission injury chance bonus."
            );
            Reckless1MissionLootFactor = Utils.Settings.Config.cfg.Bind(
                missionReckless1,
                nameof(Reckless1MissionLootFactor),
                0.5F,
                "Define the Reckless1 mission loot factor bonus."
            );

            // Mission_Reckless2
            EnableReckless2Settings = Utils.Settings.Config.cfg.Bind(
                missionReckless2,
                nameof(EnableReckless2Settings),
                false,
                "Enabled, will use the specific mission level settings"
            );

            Reckless2MissionLength = Utils.Settings.Config.cfg.Bind(
                missionReckless2,
                nameof(Reckless2MissionLength),
                1440F,
                "Define the Reckless2 mission length in seconds."
            );
            Reckless2MissionSuccessRateBonus = Utils.Settings.Config.cfg.Bind(
                missionReckless2,
                nameof(Reckless2MissionSuccessRateBonus),
                -0.1F,
                "Define the Reckless2 mission success rate bonus."
            );
            Reckless2MissionInjuryChance = Utils.Settings.Config.cfg.Bind(
                missionReckless2,
                nameof(Reckless2MissionInjuryChance),
                0.3F,
                "Define the Reckless2 mission injury chance bonus."
            );
            Reckless2MissionLootFactor = Utils.Settings.Config.cfg.Bind(
                missionReckless2,
                nameof(Reckless2MissionLootFactor),
                0.75F,
                "Define the Reckless2 mission loot factor bonus."
            );

            // Mission_Normal1
            EnableNormal1Settings = Utils.Settings.Config.cfg.Bind(
                missionNormal1,
                nameof(EnableNormal1Settings),
                false,
                "Enabled, will use the specific mission level settings"
            );

            Normal1MissionLength = Utils.Settings.Config.cfg.Bind(
                missionNormal1,
                nameof(Normal1MissionLength),
                28800F,
                "Define the Normal1 mission length in seconds."
            );
            Normal1MissionSuccessRateBonus = Utils.Settings.Config.cfg.Bind(
                missionNormal1,
                nameof(Normal1MissionSuccessRateBonus),
                0F,
                "Define the Normal1 mission success rate bonus."
            );
            Normal1MissionInjuryChance = Utils.Settings.Config.cfg.Bind(
                missionNormal1,
                nameof(Normal1MissionInjuryChance),
                0.25F,
                "Define the Normal1 mission injury chance bonus."
            );
            Normal1MissionLootFactor = Utils.Settings.Config.cfg.Bind(
                missionNormal1,
                nameof(Normal1MissionLootFactor),
                1F,
                "Define the Normal1 mission loot factor bonus."
            );

            // Mission_Prepared1
            EnablePrepared1Settings = Utils.Settings.Config.cfg.Bind(
                missionPrepared1,
                nameof(EnablePrepared1Settings),
                false,
                "Enabled, will use the specific mission level settings"
            );

            Prepared1MissionLength = Utils.Settings.Config.cfg.Bind(
                missionPrepared1,
                nameof(Prepared1MissionLength),
                57600F,
                "Define the Prepared1 mission length in seconds."
            );
            Prepared1MissionSuccessRateBonus = Utils.Settings.Config.cfg.Bind(
                missionPrepared1,
                nameof(Prepared1MissionSuccessRateBonus),
                0.1F,
                "Define the Prepared1 mission success rate bonus."
            );
            Prepared1MissionInjuryChance = Utils.Settings.Config.cfg.Bind(
                missionPrepared1,
                nameof(Prepared1MissionInjuryChance),
                0.2F,
                "Define the Prepared1 mission injury chance bonus."
            );
            Prepared1MissionLootFactor = Utils.Settings.Config.cfg.Bind(
                missionPrepared1,
                nameof(Prepared1MissionLootFactor),
                1.25F,
                "Define the Prepared1 mission loot factor bonus."
            );

            // Mission_Prepared2
            EnablePrepared2Settings = Utils.Settings.Config.cfg.Bind(
                missionPrepared2,
                nameof(EnablePrepared2Settings),
                false,
                "Enabled, will use the specific mission level settings"
            );

            Prepared2MissionLength = Utils.Settings.Config.cfg.Bind(
                missionPrepared2,
                nameof(Prepared2MissionLength),
                82800F,
                "Define the Prepared2 mission length in seconds."
            );
            Prepared2MissionSuccessRateBonus = Utils.Settings.Config.cfg.Bind(
                missionPrepared2,
                nameof(Prepared2MissionSuccessRateBonus),
                0.2F,
                "Define the Prepared2 mission success rate bonus."
            );
            Prepared2MissionInjuryChance = Utils.Settings.Config.cfg.Bind(
                missionPrepared2,
                nameof(Prepared2MissionInjuryChance),
                0.2F,
                "Define the Prepared2 mission injury chance bonus."
            );
            Prepared2MissionLootFactor = Utils.Settings.Config.cfg.Bind(
                missionPrepared2,
                nameof(Prepared2MissionLootFactor),
                1.5F,
                "Define the Prepared2 mission loot factor bonus."
            );

            validateValues();
        }

        private static void validateValues() {
            // Mission_Reckless1
            if (EnableReckless1Settings.Value) {
                Database.Mission.Settings.TryAdd(
                    "Reckless_1",
                    new Database.Mission.Setting() {
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
                    "Reckless_2",
                    new Database.Mission.Setting() {
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
                    "Normal_1",
                    new Database.Mission.Setting() {
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
                    "Prepared_1",
                    new Database.Mission.Setting() {
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
                    "Prepared_2",
                    new Database.Mission.Setting() {
                        InjuryChance = Prepared2MissionInjuryChance.Value,
                        LootFactor = Prepared2MissionLootFactor.Value,
                        MissionLength = Prepared2MissionLength.Value,
                        SuccessRateBonus = Prepared2MissionSuccessRateBonus.Value,
                    }
                );
            }

            Utils.Settings.Config.cfg.Save();
        }
    }
}
