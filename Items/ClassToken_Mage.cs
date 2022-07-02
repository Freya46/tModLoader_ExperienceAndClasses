using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExperienceAndClasses.Helpers;
using Terraria.ID;

namespace ExperienceAndClasses.Items
{
    internal class ClassToken_Mage: ClassToken_Novice
    {
        public ClassToken_Mage()
        {
            name = "Mage";
            tier = 2;
            desc = "Basic magic class." +
                       "\n\nClass advancement is available at level " + Recipes.Helpers.TIER_LEVEL_REQUIREMENTS[tier + 1] + ".";
        }
        public override void AddRecipes()
        {
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Novice").Type, 1 }, { ItemID.FallenStar, 3 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));
            int[,] ing = new int[,] { { ItemID.FallenStar, 3 } };
            RecipeHelper.createCompleteRecipes(this, Mod, ing, null, "ClassToken_Novice");
        }
    }
}
