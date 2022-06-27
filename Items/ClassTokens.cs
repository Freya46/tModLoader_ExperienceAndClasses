﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using ExperienceAndClasses.Abilities;

namespace ExperienceAndClasses.Items
{
    /* Template & Novice */
    //note that abstract ModItem cause issues
    public class ClassToken_Novice : ModItem
    {
        public static readonly string[] TIER_NAMES = new string[] { "?", "I", "II", "III" };
        public string name = "Novice";
        public int tier = 1;
        public string desc = "Starter class." +
                         "\n\nClass advancement is available at level " + Recipes.ClassRecipes.TIER_LEVEL_REQUIREMENTS[1 + 1] + ".";

        public List<Tuple<AbilityMain.ID, byte>> abilities = new List<Tuple<AbilityMain.ID, byte>>();

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            MyPlayer myLocalPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>(Mod);

            bool isEquipped = false;
            foreach (var i in myLocalPlayer.classTokensEquipped)
            {
                if (i.Item2.Equals(name)) isEquipped = true;
            }
            string desc2 = Helpers.ClassTokenEffects(Mod, Main.LocalPlayer, this, name, false, myLocalPlayer, isEquipped);

            if (desc2.Length > 0)
            {
                TooltipLine line = new TooltipLine(Mod, "desc2", desc2);
                line.OverrideColor = Color.LimeGreen;
                tooltips.Add(line);
            }
        }

        public override void UpdateInventory(Player player)
        {
            //update description in inventory (remove current bonuses)
            if (Main.LocalPlayer.Equals(player)) Item.RebuildTooltip();
            base.UpdateInventory(player);
        }

        public override void SetStaticDefaults()
        {
            //tier string
            string tierString = "?";
            if (tier > 0 && tier < TIER_NAMES.Length) tierString = TIER_NAMES[tier];

            //basic properties
            //item.name = "Class Token: " + name + " (Tier " + tierString + ")";
            DisplayName.SetDefault("Class Token: " + name + " (Tier " + tierString + ")");

            //add class description
            //item.toolTip = desc;
            Tooltip.SetDefault(desc);
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 36;
            Item.value = 0;
            Item.rare = 10;
            Item.accessory = true;

            //add class bonuses description
            Helpers.ClassTokenEffects(Mod, Main.LocalPlayer, this, name, false, new MyPlayer());
        }

        public override void AddRecipes()
        {
            if (tier==1) Commons.QuckRecipe(Mod, new int[,] { }, this, 1, new Recipes.ClassRecipes(Mod, tier));
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer myPlayer = player.GetModPlayer<MyPlayer>(Mod);

            myPlayer.classTokensEquipped.Add(new Tuple<ModItem, string>(this, name));

            //track (own) player's current abilities
            if (player.whoAmI == Main.LocalPlayer.whoAmI)
            {
                int level = myPlayer.GetLevel();
                foreach (Tuple<AbilityMain.ID, byte> ability in abilities)
                {
                    if (level >= ability.Item2)
                    {
                        myPlayer.unlocked_abilities_next[(int)ability.Item1] = true;
                    }
                }
            }
        }
    }

    /* Squire */
    public class ClassToken_Squire : ClassToken_Novice
    {
        public ClassToken_Squire()
        {
            name = "Squire";
            tier = 2;
            desc = "Basic melee damage and life class."+
                       "\n\nClass advancement is available at level " + Recipes.ClassRecipes.TIER_LEVEL_REQUIREMENTS[tier+1] + ".";
        }
        public override void AddRecipes()
        {
            Recipe recipe = new Recipes.ClassRecipes(Mod, tier);
            recipe.AddRecipeGroup("IronBar", 10);
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Novice").Type, 1 } }, this, 1, recipe);
        }
    }

    /* Squire - Tank */
    public class ClassToken_Tank : ClassToken_Novice
    {
        public ClassToken_Tank()
        {
            name = "Tank";
            tier = 3;
            desc = "Tank class."+
                       "\n\nHas the highest life, defense, and aggro. Occasionally recovers"+
                       "\na percentage of maximum life.";
        }
        public override void AddRecipes()
        {
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Squire").Type, 1 }, { ItemID.StoneBlock, 999 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 }, { ItemID.StoneBlock, 999 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));
        }
    }

    /* Squire - Warrior */
    public class ClassToken_Warrior : ClassToken_Novice
    {
        public ClassToken_Warrior()
        {
            name = "Warrior";
            tier = 3;
            desc = "Melee damage and life class."+
                       "\n\nHas the highest melee damage, and the second highest melee speed"+
                         "\nand life.";
        }
        public override void AddRecipes()
        {
            Recipe recipe;

            recipe = new Recipes.ClassRecipes(Mod, tier);
            recipe.AddRecipeGroup("IronBar", 100);
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Squire").Type, 1 } }, this, 1, recipe);

            recipe = new Recipes.ClassRecipes(Mod, tier);
            recipe.AddRecipeGroup("IronBar", 100);
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 } }, this, 1, recipe);
        }
    }

    /* Squire - Berserker */
    public class ClassToken_Berserker : ClassToken_Novice
    {
        public ClassToken_Berserker()
        {
            name = "Berserker";
            tier = 3;
            desc = "Melee speed and agility class."+
                       "\n\nHas the highest melee speed as well as moderate life, agility,"+
                         "\nand melee damage.";
        }
        public override void AddRecipes()
        {
            Recipe recipe;

            recipe = new Recipes.ClassRecipes(Mod, tier);
            recipe.AddRecipeGroup("IronBar", 100);
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Squire").Type, 1 } }, this, 1, recipe);

            recipe = new Recipes.ClassRecipes(Mod, tier);
            recipe.AddRecipeGroup("IronBar", 100);
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 } }, this, 1, recipe);
        }
    }

    /* Hunter */
    public class ClassToken_Hunter : ClassToken_Novice
    {
        public ClassToken_Hunter()
        {
            name = "Hunter";
            tier = 2;
            desc = "Basic ranged class."+
                       "\n\nClass advancement is available at level " + Recipes.ClassRecipes.TIER_LEVEL_REQUIREMENTS[tier+1] + ".";
        }
        public override void AddRecipes()
        {
            Recipe recipe = new Recipes.ClassRecipes(Mod, tier);
            recipe.AddRecipeGroup("Wood", 100);
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Novice").Type, 1 } }, this, 1, recipe);
        }
    }

    /* Hunter - Archer */
    public class ClassToken_Archer : ClassToken_Novice
    {
        public ClassToken_Archer()
        {
            name = "Archer";
            tier = 3;
            desc = "Archery class."+
                       "\n\nFocuses on archery weapons (bow/crossbow). Gun weapons do not"+
                         "\nrecieve any bonuses.";
        }
        public override void AddRecipes()
        {
            Recipe recipe;

            recipe = new Recipes.ClassRecipes(Mod, tier);
            recipe.AddRecipeGroup("Wood", 500);
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hunter").Type, 1 } }, this, 1, recipe);

            recipe = new Recipes.ClassRecipes(Mod, tier);
            recipe.AddRecipeGroup("Wood", 500);
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 } }, this, 1, recipe);
        }
    }

    /* Hunter - Ranger */
    public class ClassToken_Ranger : ClassToken_Novice
    {
        public ClassToken_Ranger()
        {
            name = "Ranger";
            tier = 3;
            desc = "Generic ranged class."+
                       "\n\nAn unspecialized ranged class. Equally well-suited to archery and"+
                         "\ngun weapons. Has slightly better survivability than Archer and"+
                         "\nGunner, but less damage.";
        }
        public override void AddRecipes()
        {
            Recipe recipe;

            recipe = new Recipes.ClassRecipes(Mod, tier);
            recipe.AddRecipeGroup("IronBar", 50);
            recipe.AddRecipeGroup("Wood", 250);
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hunter").Type, 1 } }, this, 1, recipe);

            recipe = new Recipes.ClassRecipes(Mod, tier);
            recipe.AddRecipeGroup("IronBar", 50);
            recipe.AddRecipeGroup("Wood", 250);
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 } }, this, 1, recipe);
        }
    }

    /* Hunter - Gunner */
    public class ClassToken_Gunner : ClassToken_Novice
    {
        public ClassToken_Gunner()
        {
            name = "Gunner";
            tier = 3;
            desc = "Gunnery class." +
                       "\n\nFocuses on gun weapons. Archery weapons (bow/crossbow) do not" +
                         "\nrecieve any bonuses.";
        }
        public override void AddRecipes()
        {
            Recipe recipe;

            recipe = new Recipes.ClassRecipes(Mod, tier);
            recipe.AddRecipeGroup("IronBar", 100);
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hunter").Type, 1 } }, this, 1, recipe);

            recipe = new Recipes.ClassRecipes(Mod, tier);
            recipe.AddRecipeGroup("IronBar", 100);
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 } }, this, 1, recipe);
        }
    }

    /* Mage */
    public class ClassToken_Mage : ClassToken_Novice
    {
        public ClassToken_Mage()
        {
            name = "Mage";
            tier = 2;
            desc = "Basic magic class."+
                       "\n\nClass advancement is available at level " + Recipes.ClassRecipes.TIER_LEVEL_REQUIREMENTS[tier+1] + ".";
        }
        public override void AddRecipes()
        {
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Novice").Type, 1 }, { ItemID.FallenStar, 3 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));
        }
    }

    /* Mage - Mystic */
    public class ClassToken_Mystic : ClassToken_Novice
    {
        public ClassToken_Mystic()
        {
            name = "Mystic";
            tier = 3;
            desc = "Magic damage class." +
                       "\n\nHas the highest magic damage, mana, mana regen, and mana cost" +
                         "\nreduction. This is the only class with magic crit. Occasionally" +
                         "\nrecovers a percentage of maximum mana.";
        }
        public override void AddRecipes()
        {
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Mage").Type, 1 }, {ItemID.FallenStar, 20} }, this, 1, new Recipes.ClassRecipes(Mod, tier));
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 }, {ItemID.FallenStar, 20} }, this, 1, new Recipes.ClassRecipes(Mod, tier));
        }
    }

    /* Mage - Sage */
    public class ClassToken_Sage : ClassToken_Novice
    {
        public ClassToken_Sage()
        {
            name = "Sage";
            tier = 3;
            desc = "Defensive magic class."+
                       "\n\nMagic damage and mana stats are second to the Mystic, but"+
                         "\nthe Sage has excellent life and defense. Occasionally" +
                         "\nrecovers a percentage of maximum mana. The Sage also produces"+
                         "\nan aura that boosts defense of nearby allies and further"+
                         "\nbolsters the Sage's defenses.";
        }
        public override void AddRecipes()
        {
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Mage").Type, 1 }, {ItemID.FallenStar, 10},
                {ItemID.StoneBlock, 500} }, this, 1, new Recipes.ClassRecipes(Mod, tier));
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 }, {ItemID.FallenStar, 10},
                {ItemID.StoneBlock, 500} }, this, 1, new Recipes.ClassRecipes(Mod, tier));
        }
    }

    /* Summoner */
    public class ClassToken_Summoner : ClassToken_Novice
    {
        public ClassToken_Summoner()
        {
            name = "Summoner";
            tier = 2;
            desc = "Basic minion class."+
                       "\n\nClass advancement is available at level " + Recipes.ClassRecipes.TIER_LEVEL_REQUIREMENTS[tier+1] + ".";
        }
        public override void AddRecipes()
        {
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Novice").Type, 1 }, { Mod.Find<ModItem>("Monster_Orb").Type, 1} }, this, 1, new Recipes.ClassRecipes(Mod, tier));
        }
    }

    /* Summoner - SoulBinder */
    public class ClassToken_SoulBinder : ClassToken_Novice
    {
        public ClassToken_SoulBinder()
        {
            name = "Soul Binder";
            tier = 3;
            desc = "Minion quality class."+
                       "\n\nFocuses on quality of minions rather than quantity. Has"+
                         "\nslightly better life and defense than the Minion Master.";
        }
        public override void AddRecipes()
        {
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Summoner").Type, 1 }, { Mod.Find<ModItem>("Monster_Orb").Type, 10 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 }, { Mod.Find<ModItem>("Monster_Orb").Type, 10 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));
        }
    }

    /* Summoner - MinionMaster */
    public class ClassToken_MinionMaster : ClassToken_Novice
    {
        public ClassToken_MinionMaster()
        {
            name = "Minion Master";
            tier = 3;
            desc = "Minion quantity class." +
                       "\n\nFocuses on quantity of minions rather than quality. Has" +
                         "\nslightly worse life and defense than the Soul Binder, but"+
                         "\nthis is offset by sheer numbers."+
                       "\n\nBe aware that many minions deal piecing damage and the game"+
                         "\nhas a limit on how often a single target can be hit by piecing"+
                         "\nattacks. It is possible to exceed this limit with these types"+
                         "\nof minions on a high level Minion Master, which reduces"+
                         "\neffective single target damage.";
        }
        public override void AddRecipes()
        {
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Summoner").Type, 1 }, {Mod.Find<ModItem>("Monster_Orb").Type, 10} }, this, 1, new Recipes.ClassRecipes(Mod, tier));
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 }, { Mod.Find<ModItem>("Monster_Orb").Type, 10 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));
        }
    }

    /* Rogue */
    public class ClassToken_Rogue : ClassToken_Novice
    {
        public ClassToken_Rogue()
        {
            name = "Rogue";
            tier = 2;
            desc = "Basic throwing, melee, and agility class."+
                       "\n\nClass advancement is available at level " + Recipes.ClassRecipes.TIER_LEVEL_REQUIREMENTS[tier+1] + ".";
        }
        public override void AddRecipes()
        {
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Novice").Type, 1 }, { ItemID.GoldCoin, 1 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));
        }
    }

    /* Rogue - Assassin */
    public class ClassToken_Assassin : ClassToken_Novice
    {
        public ClassToken_Assassin()
        {
            name = "Assassin";
            tier = 3;
            desc = "Melee critical and agility class."+
                       "\n\nHas the unique ability to Assassinate, which rewards a"+
                         "\n\"hit and run\" playstyle."+
                       "\n\nAssassinate: Occurs when making a melee attack against a target" +
                         "\nwhen you have not landed a hit recently. A buff and visual will"+
                         "\nindicate when it is ready. Yo-yos gain only half of the damage" +
                         "\nmultiplier. Does not trigger on projectile melee attacks such" +
                         "\nas boomerang, some flails and spears, or magic sword projectiles."+
                         "\nBonus critical damage is tripled on Assassinate.";
        }
        public override void AddRecipes()
        {
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Rogue").Type, 1 }, { ItemID.PlatinumCoin, 1 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 }, { ItemID.PlatinumCoin, 1 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));
        }
    }

    /* Rogue - Ninja */
    public class ClassToken_Ninja : ClassToken_Novice
    {
        public ClassToken_Ninja()
        {
            name = "Ninja";
            tier = 3;
            desc = "Throwing and agility class."+
                       "\n\nTo make throwing builds viable, Ninja has the highest"+
                         "\ndamage modifier of any class. Ninja also has excellent"+
                         "\nagility including the highest jump bonus.";
        }
        public override void AddRecipes()
        {
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Rogue").Type, 1}, { ItemID.PlatinumCoin, 1} }, this, 1, new Recipes.ClassRecipes(Mod, tier));
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 }, { ItemID.PlatinumCoin, 1 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));
        }
    }

    /* Cleric */
    public class ClassToken_Cleric : ClassToken_Novice
    {
        public ClassToken_Cleric()
        {
            name = "Cleric";
            tier = 2;
            desc = "Basic support class."+
                       "\n\nSee website for ability descriptions (temporary)."+
                       "\n\nClass advancement is available at level " + Recipes.ClassRecipes.TIER_LEVEL_REQUIREMENTS[tier+1] + ".";

            //abilities
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Cleric_Active_Heal, 10));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Cleric_Passive_Cleanse, 12));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Cleric_Upgrade_Heal_Smite, 14));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Cleric_Active_Sanctuary, 16));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Cleric_Upgrade_Sanctuary_HolyLight, 18));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Cleric_Alternate_Heal_Barrier, 20));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Cleric_Upgrade_Sanctuary_Blessing, 23));
        }
        public override void AddRecipes()
        {
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Novice").Type, 1 }, { ItemID.LesserHealingPotion, 3} }, this, 1, new Recipes.ClassRecipes(Mod, tier));
        }
    }

    /* Cleric - Saint */
    public class ClassToken_Saint : ClassToken_Novice
    {
        public ClassToken_Saint()
        {
            name = "Saint";
            tier = 3;
            desc = "Advanced support class." +
                       "\n\nSee website for ability descriptions (temporary).";

            //abilities
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Cleric_Active_Heal, 1));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Cleric_Passive_Cleanse, 1));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Cleric_Upgrade_Heal_Smite, 1));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Cleric_Active_Sanctuary, 1));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Cleric_Upgrade_Sanctuary_HolyLight, 1));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Cleric_Alternate_Heal_Barrier, 1));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Cleric_Upgrade_Sanctuary_Blessing, 1));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Saint_Active_DivineIntervention, 25));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Saint_Upgrade_Heal_Cure, 30));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Saint_Upgrade_Sanctuary_Link, 35));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Saint_Upgrade_DivineIntervention_Radius, 45));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Saint_Upgrade_Heal_Purify, 60));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Saint_Active_Paragon, 70));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Saint_Upgrade_DivineIntervention_Duration, 80));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Saint_Alternate_Paragon_Renew, 90));
        }
        public override void AddRecipes()
        {
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Cleric").Type, 1 }, { ItemID.HeartLantern, 1},
                { ItemID.StarinaBottle, 1},{ ItemID.Campfire, 10} }, this, 1, new Recipes.ClassRecipes(Mod, tier));

            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 }, { ItemID.HeartLantern, 1},
                { ItemID.StarinaBottle, 1},{ ItemID.Campfire, 10} }, this, 1, new Recipes.ClassRecipes(Mod, tier));
        }
    }

    /* Hybrid */
    public class ClassToken_Hybrid : ClassToken_Novice
    {
        public ClassToken_Hybrid()
        {
            name = "Hybrid";
            tier = 2;
            desc = "Basic hybrid class."+
                       "\n\nCan advance to any Tier III class or to the well-rounded Hybrid II class."+
                       "\n\nClass advancement is available at level " + Recipes.ClassRecipes.TIER_LEVEL_REQUIREMENTS[tier+1] + ".";
        }
        public override void AddRecipes()
        {
            Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Novice").Type, 1 }, { ItemID.DirtBlock, 200 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));
        }
    }

    /* Hybrid - Hybrid II */
    public class ClassToken_HybridII : ClassToken_Novice
    {
        public ClassToken_HybridII()
        {
            name = "Hybrid II";
            tier = 3;
            desc = "Advanced hybrid class."+
                         "\nA jack-of-all-trades with numerous bonuses and decent"+
                         "\nsurvivability.";
        }
        public override void AddRecipes()
        {
            Commons.QuckRecipe(Mod, new int[,] { {Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1}, { ItemID.DirtBlock, 999} }, this, 1, new Recipes.ClassRecipes(Mod, tier));
        }
    }
}