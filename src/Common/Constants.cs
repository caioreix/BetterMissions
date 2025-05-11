namespace BetterMissions.Common;

public static class Constants {

    public static class Modifier {
        public const float SuccessRateBonus = 1f;
        public const float MissionLength = 2f;
        public const float InjuryChance = 1f;
        public const float LootFactor = 1f;
    }

    public static class Reckless1 {
        public const string Name = "Reckless_1";
        public const int Index = 0;
        public const float SuccessRateBonus = 0f;
        public const int MissionLength = 7200;
        public const float InjuryChance = 0.25f;
        public const float LootFactor = 0.5f;
    }

    public static class Reckless2 {
        public const string Name = "Reckless_2";
        public const int Index = 1;
        public const float SuccessRateBonus = 0.05f;
        public const int MissionLength = 14400;
        public const float InjuryChance = 0.25f;
        public const float LootFactor = 0.75f;
    }

    public static class Normal1 {
        public const string Name = "Normal_1";
        public const int Index = 2;
        public const float SuccessRateBonus = 0.1f;
        public const int MissionLength = 23400;
        public const float InjuryChance = 0.2f;
        public const float LootFactor = 1f;
    }

    public static class Prepared1 {
        public const string Name = "Prepared_1";
        public const int Index = 3;
        public const float SuccessRateBonus = 0.15f;
        public const int MissionLength = 36000;
        public const float InjuryChance = 0.15f;
        public const float LootFactor = 1.25f;
    }

    public static class Prepared2 {
        public const string Name = "Prepared_2";
        public const int Index = 4;
        public const float SuccessRateBonus = 0.2f;
        public const int MissionLength = 57600;
        public const float InjuryChance = 0.15f;
        public const float LootFactor = 1.5f;
    }
}
