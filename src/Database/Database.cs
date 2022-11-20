using System;

using Utils.Database;

namespace Database;

public static class LocalDB {
    public static void Config() {
        var loadActions = new Action[]{
            () => Mission.load(),
        };

        var saveActions = new Action[]{
            () => Mission.save(),
        };

        var cleanActions = new Action[] { };

        DB.Config(loadActions, saveActions, cleanActions);
    }

    public static void Load() { DB.Load(); }
    public static void Save() { DB.Clean(); DB.Save(); }
    public static void Clean() { DB.Clean(); }
}
