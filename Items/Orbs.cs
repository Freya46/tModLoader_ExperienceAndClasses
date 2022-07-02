using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExperienceAndClasses.Items
{
    /* Template & Experience Orb x1 */
    //note that abstract ModItem cause issues
    public class Experience : ModItem
    {
        static int[] values = { 1, 100, 1000, 10000, 100000, 1000000 };

        public int orbValue = 1;

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Worth " + (ExperienceAndClasses.EXP_ITEM_VALUE * orbValue) + " experience.");
            if (orbValue > 1)
            {
                DisplayName.SetDefault("Experience Orb " + orbValue);
            }
            else
            {
                DisplayName.SetDefault("Experience Orb");
            }
        }

        public override void SetDefaults()
        {
            //name
            //item.name = "Experience Orb";
            //if (orbValue > 1) item.name += " " + orbValue;

            //info
            //item.toolTip = "Worth " + (ExperienceAndClasses.EXP_ITEM_VALUE * orbValue) + " experience.";
            Item.width = 29;
            Item.height = 30;
            Item.maxStack = 9999999;
            Item.value = 0;
            Item.rare = 7;
            Item.consumable = true;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.useStyle = 4;
        }

        public override void AddRecipes()
        {
            //orb-to-orb converstion
            if (orbValue == 1)
            {
                //convert down to 1's
                foreach (int i in values)
                {
                    if (i>1)
                    {
                        int expI = Mod.Find<ModItem>("Experience" + i).Type;
                        Recipe recipe = CreateRecipe()
                            .AddIngredient(expI);
                        recipe.Register();
                        // Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("Experience"+i).Type, 1 } }, this, i);
                    }
                }
            }
            else
            {
                //convert up from 1's
                int exp = ModContent.ItemType<Experience>();
                Recipe recipe = CreateRecipe()
                    .AddIngredient(exp, orbValue);
                recipe.Register();
                // Commons.QuckRecipe(Mod, new int[,] { {Mod.ItemType<Experience>(), orbValue } }, this, 1);
            }
            
            //exp-to-orb conversion
            // Commons.QuckRecipe(Mod, new int[,] { { } }, this, 1, new Recipes.ExpRecipe(Mod, ExperienceAndClasses.EXP_ITEM_VALUE * orbValue), TileID.Campfire);

            int exp2 = ModContent.ItemType<Experience>();
            int amountNeeded = (int)(ExperienceAndClasses.EXP_ITEM_VALUE * orbValue);
            Recipe recipe2 = CreateRecipe()
                .AddIngredient(exp2, amountNeeded)
                .AddTile(TileID.Campfire);
            recipe2.Register();
        }
        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (player.whoAmI == Main.LocalPlayer.whoAmI)
            {
                ExperienceAndClasses.localMyPlayer.AddExp(ExperienceAndClasses.EXP_ITEM_VALUE * orbValue, true);
            }
            return true;
        }

        /* Bypass MaxStacks */
        public override void OnCraft(Recipe recipe)
        {
            Item.maxStack = 9999999;
            base.OnCraft(recipe);
        }
        public override void UpdateInventory(Player player)
        {
            Item.maxStack = 9999999;
            base.UpdateInventory(player);
        }
    }

    /* Experience Orb x100 */
    public class Experience100 : Experience
    {
        public Experience100() : base()
        {
            orbValue = 100;
        }
        //public override void AddRecipes()
        //{
        //    //include basic recipes
        //    base.AddRecipes();

        //    /*~~~~~~~~~~~~~~~~~~~~~~~~Tier I Exchange Rates~~~~~~~~~~~~~~~~~~~~~~~~*/
        //    Commons.QuckRecipe(mod, new int[,] { { mod.ItemType("Boss_Orb"), 1 } }, this, 2, new Recipes.TierRecipe(mod, 1, true, false, -1, -1));
        //    Commons.QuckRecipe(mod, new int[,] { { mod.ItemType("Monster_Orb"), 1 } }, this, 1, new Recipes.TierRecipe(mod, 1, true, false, -1, -1));

        //    /*~~~~~~~~~~~~~~~~~~~~~~~~Tier 2 Exchange Rates~~~~~~~~~~~~~~~~~~~~~~~~*/
        //    Commons.QuckRecipe(mod, new int[,] { { mod.ItemType("Boss_Orb"), 1 } }, this, 6, new Recipes.TierRecipe(mod, 2, false, false, -1, -1));
        //    Commons.QuckRecipe(mod, new int[,] { { mod.ItemType("Monster_Orb"), 1 } }, this, 3, new Recipes.TierRecipe(mod, 2, false, false, -1, -1));

        //    /*~~~~~~~~~~~~~~~~~~~~~~~~Tier 3 Exchange Rates~~~~~~~~~~~~~~~~~~~~~~~~*/
        //    Commons.QuckRecipe(mod, new int[,] { { mod.ItemType("Monster_Orb"), 1 } }, this, 5, new Recipes.TierRecipe(mod, 3, false, false, -1, 49));
        //}
    }

    /* Experience Orb x1000 */
    public class Experience1000 : Experience
    {
        public Experience1000() : base()
        {
            orbValue = 1000;
        }
        //public override void AddRecipes()
        //{
        //    //include basic recipes
        //    base.AddRecipes();

        //    /*~~~~~~~~~~~~~~~~~~~~~~~~Tier 3 Exchange Rates~~~~~~~~~~~~~~~~~~~~~~~~*/
        //    Commons.QuckRecipe(mod, new int[,] { { mod.ItemType("Boss_Orb"), 1 } }, this, 1, new Recipes.TierRecipe(mod, 3, false, false, -1, 49));

        //    /*~~~~~~~~~~~~~~~~~~~~~~~~Tier 3 Level 50+ Exchange Rates~~~~~~~~~~~~~~~~~~~~~~~~*/
        //    Commons.QuckRecipe(mod, new int[,] { { mod.ItemType("Boss_Orb"), 1 } }, this, 2, new Recipes.TierRecipe(mod, 3, false, false, 50, -1));
        //    Commons.QuckRecipe(mod, new int[,] { { mod.ItemType("Monster_Orb"), 1 } }, this, 1, new Recipes.TierRecipe(mod, 3, false, false, 50, -1));
        //}
    }

    /* Experience Orb x10,000 */
    public class Experience10000 : Experience
    {
        public Experience10000() : base()
        {
            orbValue = 10000;
        }
    }

    /* Experience Orb x100,000 */
    public class Experience100000 : Experience
    {
        public Experience100000() : base()
        {
            orbValue = 100000;
        }
    }

    /* Experience Orb x1,000,000 */
    public class Experience1000000 : Experience
    {
        public Experience1000000() : base()
        {
            orbValue = 1000000;
        }
    }

    /* Boss Orb */
    public class Boss_Orb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boss Orb");
            Tooltip.SetDefault("Component for Tier III classes. Can also be crafted into Ascension Orbs, consumed for XP, or sold.");
        }

        public override void SetDefaults()
        {
            Item.width = 29;
            Item.height = 30;
            Item.maxStack = 9999999;
            Item.value = 50000;
            Item.rare = 10;
            Item.consumable = true;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.useStyle = 4;
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (player.whoAmI == Main.LocalPlayer.whoAmI)
            {
                ExperienceAndClasses.localMyPlayer.AddExp(ExperienceAndClasses.localMyPlayer.GetBossOrbXP(), true);
            }
            return true;
        }

        /* Bypass MaxStacks */
        public override void OnCraft(Recipe recipe)
        {
            Item.maxStack = 9999999;
            base.OnCraft(recipe);
        }
        public override void UpdateInventory(Player player)
        {
            Item.maxStack = 9999999;
            base.UpdateInventory(player);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(Mod, "desc", "Current XP Value: " + ExperienceAndClasses.localMyPlayer.GetBossOrbXP());
            line.OverrideColor = Color.LimeGreen;
            tooltips.Add(line);
        }
    }

    /* Ascension Orb */
    public class Monster_Orb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ascension Orb");
            Tooltip.SetDefault("Component for Tier II and III classes. Can also be consumed for XP, or sold.");
        }

        public override void SetDefaults()
        {
            Item.width = 29;
            Item.height = 30;
            Item.maxStack = 9999999;
            Item.value = 25000;
            Item.rare = 9;
            Item.consumable = true;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.useStyle = 4;
        }

        public override void AddRecipes()
        {
            //convert boss orb to ascension orb
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("Boss_Orb").Type, 1 } }, this, 3);

            int bossOrb = Mod.Find<ModItem>("Boss_Orb").Type;
            int monsterOrb = Mod.Find<ModItem>("Monster_Orb").Type;
            Recipe recipe1 = CreateRecipe()
                .AddIngredient(bossOrb);
            recipe1.ReplaceResult(this, 3);
            recipe1.Register();

            //alt recipe: gold
            //Commons.QuckRecipe(Mod, new int[,] { { ItemID.LifeCrystal, 1 }, { ItemID.ManaCrystal, 1 }, { ItemID.GoldBar, 20 } }, this, 1);

            Recipe recipe2 = CreateRecipe()
                .AddIngredient(ItemID.LifeCrystal)
                .AddIngredient(ItemID.ManaCrystal)
                .AddIngredient(ItemID.GoldBar, 20);
            recipe2.Register();

            //alt recipe: plat
           // Commons.QuckRecipe(Mod, new int[,] { { ItemID.LifeCrystal, 1 }, { ItemID.ManaCrystal, 1 }, { ItemID.PlatinumBar, 20 } }, this, 1);

            Recipe recipe3 = CreateRecipe()
                .AddIngredient(ItemID.LifeCrystal)
                .AddIngredient(ItemID.ManaCrystal)
                .AddIngredient(ItemID.PlatinumBar, 20);
            recipe3.Register();
        }

        public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
        {
            if (player.whoAmI == Main.LocalPlayer.whoAmI)
            {
                ExperienceAndClasses.localMyPlayer.AddExp(ExperienceAndClasses.localMyPlayer.GetMonsterOrbXP(), true);
            }
            return true;
        }

        /* Bypass MaxStacks */
        public override void OnCraft(Recipe recipe)
        {
            Item.maxStack = 9999999;
            base.OnCraft(recipe);
        }
        public override void UpdateInventory(Player player)
        {
            Item.maxStack = 9999999;
            base.UpdateInventory(player);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(Mod, "desc2", "Current XP Value: " + ExperienceAndClasses.localMyPlayer.GetMonsterOrbXP());
            line.OverrideColor = Color.LimeGreen;
            tooltips.Add(line);
        }
    }
}
