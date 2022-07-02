using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExperienceAndClasses.Helpers;
using Terraria.ID;
using ExperienceAndClasses.Abilities;

namespace ExperienceAndClasses.Items
{
    internal class ClassToken_Cleric : ClassToken_Novice
    {
        public ClassToken_Cleric()
        {
            name = "Cleric";
            tier = 2;
            desc = "Basic support class." +
                       "\n\nSee website for ability descriptions (temporary)." +
                       "\n\nClass advancement is available at level " + Recipes.Helpers.TIER_LEVEL_REQUIREMENTS[tier + 1] + ".";

            // abilities
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
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Novice").Type, 1 }, { ItemID.LesserHealingPotion, 3 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));
            int[,] ing = new int[,] { { ItemID.LesserHealingPotion, 3 } };
            RecipeHelper.createCompleteRecipes(this, Mod, ing, null, "ClassToken_Novice");
        }
    }
}
