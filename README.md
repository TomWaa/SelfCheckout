# Self Checkout
Stardew Valley mod that enables you to buy in the shop if no one is at the counter but the owner is at the location

Originally made by [GuiNoya/SelfServiceShop](https://github.com/GuiNoya/SVMods/tree/master/SelfServiceShop)
I rewrote it so it works the current Stardew Valley version.

# Installation
1. [Install latest version of SMAPI](https://smapi.io/) and follow this [guide](https://stardewvalleywiki.com/Modding:Player_Guide/Getting_Started#Getting_started)
2. Unzip the mod folder into **Stardew Valley/Mods**
3. Run the game

# Configuration
As default all the configurations are set to true. The `config.json` file will generate after running the mod for the first time.

The different fields are:
Key | Value | Description
--- | --- | ---
Pierre | `True`/`False` | Lets you buy from Pierre's shop while he is in the shop (even if he is arranging the shelves)
Ranch | `True`/`False` | Lets you buy from the Ranch when Marnie is on the ranch
Carpenter | `True`/`False` | Lets you buy from Robin when she is the house
FishShop | `True`/`False` | Lets you buy from Willy when he is at the beach
Blacksmith | `True`/`False` | Lets you access the Blacksmith menu if Clint is upgrading a tool
IceCreamStand | `True`/`False` | Lets you buy ice cream from the stand even if there is no one there
IceCreamInAllSeasons | `True`/`False` | Enables you to buy ice cream in all seasons
ShopsAlwaysOpen | `True`/`False` | Ignore the owner location (or season for the ice cream stand), so if you can reach the counter, you can buy from the shop.
