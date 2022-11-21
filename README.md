# BetterMissions

Better Missions is a VRising mod that allows you to change servant missions times and any other things.

## Instalation (Manual)

* Install [BepInEx](https://docs.bepinex.dev/master/articles/user_guide/installation/index.html)
* Extract [BetterMissions.dll](https://github.com/caioreix/BetterMissions/releases) into (VRising client folder)/BepInEx/plugins
* [ServerLaunchFix](https://v-rising.thunderstore.io/package/Mythic/ServerLaunchFix/) recommended for in-game hosted
  games
* (Optional) If not using ServerLaunchFix, extract BetterMissions.dll into (VRising server folder)/BepInEx/plugins

## How to use

* When you start an mission in the throne it will automatically reduce the time based on defined configs.

Features:
  - Control the mission duration.
  - Offline mission duration progress.

Future features:
  - Config reduction for each mission on map.
  - Update database before saving, to work with other mods like (CoffinSleep).
  - Encrypt json files.
  - Update mission time if the MissionReduceRate was changed.
  - Add chat command to reload server configs.
  - Level system to send servants to the mission.

## Configuration

Values can be configured at `(VRising client/server folder)/VRising/BepInEx/config/BetterMissions.cfg`

```
[Server]
## Define the mission reduce divisor. Ex: if you set 2, 2 hours will be 1 hour (0 will be replaced by 1)
# Setting type: Single
# Default value: 2
MissionReduceRate = 2

## Enabled, mission progress will be loaded even with the server offline.
# Setting type: Boolean
# Default value: true
OfflineMissionProgress = true
```
