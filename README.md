# BetterMissions

Coffin Sleep is a VRising mod that speeds up time while you sleep in your coffin.

## Instalation (Manual)

* Install [BepInEx](https://docs.bepinex.dev/master/articles/user_guide/installation/index.html)
* Extract [BetterMissions.dll](https://github.com/caioreix/BetterMissions/releases) into (VRising client folder)/BepInEx/plugins
* [ServerLaunchFix](https://v-rising.thunderstore.io/package/Mythic/ServerLaunchFix/) recommended for in-game hosted
  games
* (Optional) If not using ServerLaunchFix, extract BetterMissions.dll into (VRising server folder)/BepInEx/plugins

## How to use

* Just need to enter in your coffin and sleep.

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
