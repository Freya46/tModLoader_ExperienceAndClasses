using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExperienceAndClasses.Helpers;
using Terraria.ID;

namespace ExperienceAndClasses.Items
{
    internal class ClassToken_Rogue: ClassToken_Novice
    {
        public ClassToken_Rogue()
        {
            name = "Rogue";
            tier = 2;
            desc = "Basic throwing, melee, and agility class." +
                       "\n\nClass advancement is available at level " + Recipes.Helpers.TIER_LEVEL_REQUIREMENTS[tier + 1] + ".";
        }
        public override void AddRecipes()
        {
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Novice").Type, 1 }, { ItemID.GoldCoin, 1 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));
            int[,] ing = new int[,] { { ItemID.GoldCoin, 1 } };
            RecipeHelper.createCompleteRecipes(this, Mod, ing, null, "ClassToken_Novice");
        }
    }
}
