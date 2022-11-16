
namespace Entities;

public static class World {
    private static Unity.Entities.World _serverWorld;
    public static Unity.Entities.World Server {
        get {
            if (_serverWorld != null) return _serverWorld;

            _serverWorld = getWorld("Server")
                ?? throw new System.Exception("There is no Server world (yet). Did you install a server mod on the client?");
            return _serverWorld;
        }
    }

    private static Unity.Entities.World getWorld(string name) {
        foreach (var world in Unity.Entities.World.s_AllWorlds) {
            if (world.Name == name) {
                return world;
            }
        }

        return null;
    }
}