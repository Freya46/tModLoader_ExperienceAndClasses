using ExperienceAndClasses.Helpers;
using Terraria.ModLoader;
using Terraria;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

using ExperienceAndClasses.Abilities;

namespace ExperienceAndClasses.Items
{
    internal class ClassToken_Novice : ModItem
    {
        public static readonly string[] TIER_NAMES = new string[] { "?", "I", "II", "III" };
        public string name = "Novice";
        public int tier = 1;
        public string desc = "Starter class." +
                         "\n\nClass advancement is available at level " + Recipes.Helpers.TIER_LEVEL_REQUIREMENTS[1 + 1] + ".";

        public List<Tuple<AbilityMain.ID, byte>> abilities = new List<Tuple<AbilityMain.ID, byte>>();


        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            MyPlayer myLocalPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();

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
            if (tier == 1)
            {
                Recipe recipe = CreateRecipe();
                recipe.Register();
            };
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            MyPlayer myPlayer = player.GetModPlayer<MyPlayer>();

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
}
