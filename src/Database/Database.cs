using Utils.Database;

namespace BetterMissions.Database;

public static class LocalDB {
    public static void Load() { DB.Load(); }
    public static void Save() { DB.Clean(); DB.Save(); }
    public static void Clean() { DB.Clean(); }
}
