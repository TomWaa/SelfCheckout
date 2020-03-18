using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Menus;
using xTile.Dimensions;

namespace SelfCheckout
{
    public class ModEntry : Mod
    {
        #region Fields

        private static readonly Texture2D PortraitRobin = Game1.content.Load<Texture2D>(@"Portraits\Robin");
        private static readonly Texture2D ProtraitMarnie = Game1.content.Load<Texture2D>(@"Portraits\Marnie");
        private ModConfig _config;

        #endregion
        #region Required Methods

        public override void Entry(IModHelper helper)
        {
            this._config = helper.ReadConfig<ModConfig>();
            Helper.Events.Input.ButtonPressed += new EventHandler<ButtonPressedEventArgs>(this.Input_ButtonPressed);
        }

        #endregion
        #region Methods

        private void AnimalShop()
        {
            Response[] responseArray1 = new Response[]
            {
                new Response("Supplies", Game1.content.LoadString(@"Strings\Locations:AnimalShop_Marnie_Supplies")),
                new Response("Purchase", Game1.content.LoadString(@"Strings\Locations:AnimalShop_Marnie_Animals")),
                new Response("Leave", Game1.content.LoadString(@"Strings\Locations:AnimalShop_Marnie_Leave"))
            };
            Game1.currentLocation.createQuestionDialogue("", responseArray1, "Marnie");
        }

        private void BlackSmith(NPC clint)
        {
            if (Game1.player.toolBeingUpgraded.Value != null)
            {
                if (Game1.player.daysLeftForToolUpgrade.Value > 0)
                {
                    Game1.drawDialogue(clint, Game1.content.LoadString(@"Data\ExtraDialogue:Clint_StillWorking", Game1.player.toolBeingUpgraded.Value.DisplayName));
                    base.Helper.Events.Display.MenuChanged += new EventHandler<MenuChangedEventArgs>(this.Display_MenuChanged);
                }
                else if (Game1.player.freeSpotsInInventory() <= 0)
                {   
                    Game1.drawDialogue(clint, Game1.content.LoadString(@"Data\ExtraDialogue:Clint_NoInventorySpace"));
                }
                else
                {
                    Game1.player.holdUpItemThenMessage(Game1.player.toolBeingUpgraded.Value, true);
                    Game1.player.addItemToInventoryBool(Game1.player.toolBeingUpgraded.Value, false);
                    Game1.player.toolBeingUpgraded.Value = null;
                }
            }
            else
            {
                // Check if player does not have any geodes in his inventory
                Response[] responseArray;
                if (!Game1.player.hasItemInInventory(0x217, 1, 0) && !Game1.player.hasItemInInventory(0x218, 1, 0) && !Game1.player.hasItemInInventory(0x219, 1, 0) && !Game1.player.hasItemInInventory(0x2ed, 1, 0))
                {
                    responseArray = new Response[]
                    {
                        new Response("Shop", Game1.content.LoadString(@"Strings\Locations:Blacksmith_Clint_Shop")),
                        new Response("Upgrade", Game1.content.LoadString(@"Strings\Locations:Blacksmith_Clint_Upgrade")),
                        new Response("Leave", Game1.content.LoadString(@"Strings\Locations:Blacksmith_Clint_Leave"))
                    };
                }
                else
                {
                    responseArray = new Response[]
                    {
                        new Response("Shop", Game1.content.LoadString(@"Strings\Locations:Blacksmith_Clint_Shop")),
                        new Response("Upgrade", Game1.content.LoadString(@"Strings\Locations:Blacksmith_Clint_Upgrade")),
                        new Response("Process", Game1.content.LoadString(@"Strings\Locations:Blacksmith_Clint_Geodes")),
                        new Response("Leave", Game1.content.LoadString(@"Strings\Locations:Blacksmith_Clint_Leave"))
                    };
                }
                Game1.currentLocation.createQuestionDialogue("", responseArray, "Blacksmith");
            }
        }

        private void Carpenter()
        {
            if ((Game1.player.daysUntilHouseUpgrade.Value >= 0) || (Game1.getFarm().isThereABuildingUnderConstruction() || (Game1.player.currentUpgrade != null)))
            {
                Game1.activeClickableMenu = new ShopMenu(Utility.getCarpenterStock(), 0, "Robin");
            }
            else
            {
                List<Response> list = new List<Response>(4);
                list.Add(new Response("Shop", Game1.content.LoadString(@"Strings\Locations:ScienceHouse_CarpenterMenu_Shop")));

                if (!Game1.IsMasterGame)
                {
                    if (Game1.player.houseUpgradeLevel < 3)
                    {
                        list.Add(new Response("Upgrade", Game1.content.LoadString(@"Strings\Locations:ScienceHouse_CarpenterMenu_UpgradeCabin")));
                    }
                }
                else if (Game1.player.HouseUpgradeLevel < 3)
                {
                    list.Add(new Response("Upgrade", Game1.content.LoadString(@"Strings\Locations:ScienceHouse_CarpenterMenu_UpgradeHouse")));
                }
                else if (((Game1.MasterPlayer.mailReceived.Contains("cclsComplete") || (Game1.MasterPlayer.mailReceived.Contains("JojaMemeber") || Game1.MasterPlayer.hasCompletedCommunityCenter())) &&
                    ((Game1.getLocationFromName("Town") as Town).daysUntilCommunityUpgrade.Value <= 0)) &&
                    !Game1.MasterPlayer.mailReceived.Contains("pamHouseUpgrade"))
                {
                    list.Add(new Response("CommunityUpgrade", Game1.content.LoadString(@"Strings\Locations:ScienceHouse_CarpenterMenu_CommunityUpgrade")));
                }

                list.Add(new Response("Construct", Game1.content.LoadString(@"Strings\Locations:ScienceHouse_CarpenterMenu_Construct")));
                list.Add(new Response("Leave", Game1.content.LoadString(@"Strings\Locations:ScienceHouse_CarpenterMenu_Leave")));
                Game1.currentLocation.createQuestionDialogue(Game1.content.LoadString(@"Strings\Locations:ScienceHouse_CarpenterMenu"), list.ToArray(), "Carpenter");
            }
        }

        private void Display_MenuChanged(object sender, MenuChangedEventArgs e)
        {
            if (e.NewMenu == null)
            {
                if (!Game1.player.hasItemInInventory(0x217, 1, 0) && !Game1.player.hasItemInInventory(0x218, 1, 0) && !Game1.player.hasItemInInventory(0x219, 1, 0) && !Game1.player.hasItemInInventory(0x2ed, 1, 0))
                {
                    Game1.activeClickableMenu = new ShopMenu(Utility.getBlacksmithStock(), 0, "Clint");
                }
                else
                {
                    Response[] responseArray = new Response[]
                    {
                        new Response("Shop", Game1.content.LoadString(@"Strings\Locations:Blacksmith_Clint_Shop")),
                        new Response("Process", Game1.content.LoadString(@"Strings\Locations:Blacksmith_Clint_Geodes")),
                        new Response("Leave", Game1.content.LoadString(@"Strings\Locations:Blacksmith_Clint_Leave"))
                    };
                    Game1.currentLocation.createQuestionDialogue("", responseArray, "Blacksmith");
                }

                base.Helper.Events.Display.MenuChanged -= new EventHandler<MenuChangedEventArgs>(this.Display_MenuChanged);
            }
        }

        private void Input_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            if (!Context.IsWorldReady || !Context.IsPlayerFree || Game1.activeClickableMenu != null || !SButtonExtensions.IsActionButton(e.Button))
                return;

            string str = Game1.currentLocation.doesTileHaveProperty((int)e.Cursor.GrabTile.X, (int)e.Cursor.GrabTile.Y, "Action", "Buildings");
            if (str == "Buy General")
            {
                if (this._config.Pierre && (this._config.ShopsAlwaysOpen || this.IsNpcInLocation("Pierre", "")))
                {
                    base.Helper.Input.Suppress(e.Button);
                    Game1.activeClickableMenu = new ShopMenu(((SeedShop)Game1.currentLocation).shopStock(), 0, "Pierre");
                    return;
                }
                return;
            }
            else
            {
                if (str == "Carpenter")
                {
                    if (this._config.ShopsAlwaysOpen)
                    {
                        NPC npc = Game1.currentLocation.characters.Find("Robin");
                        if (npc != null)
                        {
                            Vector2 vector = npc.getTileLocation();
                            object[] objArray1 = new object[] { new Location((int)vector.X, (int)vector.Y) };
                            base.Helper.Reflection.GetMethod(Game1.currentLocation, "carpenters", true).Invoke(objArray1);
                            goto TR_0016;
                        }
                    }
                    else
                    {
                        this.Carpenter();
                        goto TR_0016;
                    }
                }
                else if (str == "AnimalShop")
                {
                    if (this._config.Ranch)
                    {
                        if (!this._config.ShopsAlwaysOpen)
                        {
                            NPC npc = Game1.currentLocation.characters.Find("Marnie");
                            if (npc != null)
                            {
                                Vector2 vector = npc.getTileLocation();
                                object[] objArray = new object[] { new Location((int)vector.X, ((int)vector.Y) + 1) };
                                base.Helper.Reflection.GetMethod(Game1.currentLocation, "animalShop", true).Invoke(objArray);
                                goto TR_000F;
                            }
                        }
                        else
                        {
                            this.AnimalShop();
                            goto TR_000F;
                        }
                    }
                }
                else if (str == "Buy Fish")
                {
                    if (this._config.FishShop && this._config.ShopsAlwaysOpen || this.IsNpcInLocation("Willy", "") || this.IsNpcInLocation("Willy", "Beach"))
                    {
                        base.Helper.Input.Suppress(e.Button);
                        Game1.activeClickableMenu = new ShopMenu(Utility.getFishShopStock(Game1.player), 0, "Willy");
                        return;
                    }
                }
                else if (str == "Blacksmith")
                {
                    if (this._config.Blacksmith)
                    {
                        if (!this._config.ShopsAlwaysOpen)
                        {
                            NPC npc = Game1.currentLocation.characters.Find("Clint");
                            if (npc != null)
                            {
                                this.BlackSmith(npc);
                                goto TR_0005;
                            }
                        }
                        else
                        {
                            this.BlackSmith(Game1.getCharacterFromName("Clint", false));
                            goto TR_0005;
                        }
                    }
                }
                else
                {
                    if (str != "IceCreamStand")
                        return;

                    if (this._config.IceCreamStand && this._config.ShopsAlwaysOpen || this._config.IceCreamInAllSeasons || SDate.Now().Season == "summer")
                    {
                        Dictionary<StardewValley.ISalable, int[]> dictionary = new Dictionary<StardewValley.ISalable, int[]>();
                        int[] numArray = new int[] { 250, 0x7fff_ffff };
                        dictionary.Add(new StardewValley.Object(0xe9, 1, false, -1, 0), numArray);
                        Game1.activeClickableMenu = new ShopMenu(dictionary, 0, null);
                        base.Helper.Input.Suppress(e.Button);
                    }
                }
                return;
            }
            TR_0005:
            base.Helper.Input.Suppress(e.Button);
            return;
            TR_000F:
            base.Helper.Input.Suppress(e.Button);
            return;
            TR_0016:
            base.Helper.Input.Suppress(e.Button);
        }

        private bool IsNpcInLocation(string name, string locationName = "") =>
            (((locationName.Length == 0) ? Game1.currentLocation : Game1.locations.Find(locationName)).characters.Find(name) != null);

        #endregion
    }
}
