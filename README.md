# BetterMissions

Better Missions is a VRising mod that allows you to change servant missions times and any other things.

## Instalation (Manual)

- Install [BepInEx](https://thunderstore.io/c/v-rising/p/BepInEx/BepInExPack_V_Rising/)
- Extract [BetterMissions.dll](https://github.com/caioreix/BetterMissions/releases) into (VRising client folder)/VRising_Server/BepInEx/plugins
- To synchronize values, you must also install the mod on the client side into (VRising client folder)/BepInEx/plugins.
- [ServerLaunchFix](https://v-rising.thunderstore.io/package/Mythic/ServerLaunchFix/) recommended for in-game hosted
  games
- (Optional) If not using ServerLaunchFix, extract BetterMissions.dll into (VRising server folder)/BepInEx/plugins

## How to use

- When you start an mission in the throne it will automatically reduce the time based on defined configs.
- Starting from version 2.2.0, the mod validates client and server versions for compatibility and allows enabling or disabling UI synchronization via in-game commands.

Features:

- Control the mission duration.
- Offline mission duration progress.
- Work with other mods that speeds the time, like [CoffinSleep](https://github.com/caioreix/CoffinSleep).
- Client-server version validation to ensure compatibility.
- UI synchronization between client and server.
- Runtime control of UI synchronization by commands.

## Commands

The following commands can be used in the game chat:

- `.bm.ui.enable` - Enable UI synchronization with the server
- `.bm.ui.enable` - Disable UI synchronization with the server

## Configuration

Values can be configured at `(VRising client/server folder)/VRising/BepInEx/config/BetterMissions.cfg`

![difficult](./difficult.gif)

```

[0.🚩 Mission]
## Define the mission length modifier. (MissionLength / modifier)
# Setting type: Single
# Default value: 2
MissionLengthModifier = 2

## Define the mission success rate bonus modifier. (MissionSuccessRateBonus / modifier)
# Setting type: Single
# Default value: 1
MissionSuccessRateBonusModifier = 1

## Define the mission injury chance bonus modifier. (MissionInjuryChance / modifier)
# Setting type: Single
# Default value: 1
MissionInjuryChanceModifier = 1

## Define the mission loot factor bonus modifier. (MissionLootFactor / modifier)
# Setting type: Single
# Default value: 1
MissionLootFactorModifier = 1

[1.🚩 Mission-Reckless-1]

## Enabled, will use the specific mission level settings
# Setting type: Boolean
# Default value: false
EnableReckless1Settings = false

## Define the Reckless1 mission length in seconds.
# Setting type: Single
# Default value: 7200
Reckless1MissionLength = 7200

## Define the Reckless1 mission success rate bonus.
# Setting type: Single
# Default value: -0.2
Reckless1MissionSuccessRateBonus = -0.2

## Define the Reckless1 mission injury chance bonus.
# Setting type: Single
# Default value: 0.3
Reckless1MissionInjuryChance = 0.3

## Define the Reckless1 mission loot factor bonus.
# Setting type: Single
# Default value: 0.5
Reckless1MissionLootFactor = 0.5

[2.🚩 Mission-Reckless-2]

## Enabled, will use the specific mission level settings
# Setting type: Boolean
# Default value: false
EnableReckless2Settings = false

## Define the Reckless2 mission length in seconds.
# Setting type: Single
# Default value: 14400
Reckless2MissionLength = 14400

## Define the Reckless2 mission success rate bonus.
# Setting type: Single
# Default value: -0.1
Reckless2MissionSuccessRateBonus = -0.1

## Define the Reckless2 mission injury chance bonus.
# Setting type: Single
# Default value: 0.3
Reckless2MissionInjuryChance = 0.3

## Define the Reckless2 mission loot factor bonus.
# Setting type: Single
# Default value: 0.75
Reckless2MissionLootFactor = 0.75

[3.🚩 Mission-Normal]

## Enabled, will use the specific mission level settings
# Setting type: Boolean
# Default value: false
EnableNormal1Settings = false

## Define the Normal1 mission length in seconds.
# Setting type: Single
# Default value: 23400
Normal1MissionLength = 23400

## Define the Normal1 mission success rate bonus.
# Setting type: Single
# Default value: 0
Normal1MissionSuccessRateBonus = 0

## Define the Normal1 mission injury chance bonus.
# Setting type: Single
# Default value: 0.25
Normal1MissionInjuryChance = 0.25

## Define the Normal1 mission loot factor bonus.
# Setting type: Single
# Default value: 1
Normal1MissionLootFactor = 1

[4.🚩 Mission-Prepared-1]

## Enabled, will use the specific mission level settings
# Setting type: Boolean
# Default value: false
EnablePrepared1Settings = false

## Define the Prepared1 mission length in seconds.
# Setting type: Single
# Default value: 36000
Prepared1MissionLength = 36000

## Define the Prepared1 mission success rate bonus.
# Setting type: Single
# Default value: 0.1
Prepared1MissionSuccessRateBonus = 0.1

## Define the Prepared1 mission injury chance bonus.
# Setting type: Single
# Default value: 0.2
Prepared1MissionInjuryChance = 0.2

## Define the Prepared1 mission loot factor bonus.
# Setting type: Single
# Default value: 1.25
Prepared1MissionLootFactor = 1.25

[5.🚩 Mission-Prepared-2]

## Enabled, will use the specific mission level settings
# Setting type: Boolean
# Default value: false
EnablePrepared2Settings = false

## Define the Prepared2 mission length in seconds.
# Setting type: Single
# Default value: 57600
Prepared2MissionLength = 57600

## Define the Prepared2 mission success rate bonus.
# Setting type: Single
# Default value: 0.2
Prepared2MissionSuccessRateBonus = 0.2

## Define the Prepared2 mission injury chance bonus.
# Setting type: Single
# Default value: 0.2
Prepared2MissionInjuryChance = 0.2

## Define the Prepared2 mission loot factor bonus.
# Setting type: Single
# Default value: 1.5
Prepared2MissionLootFactor = 1.5
```
