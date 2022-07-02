using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExperienceAndClasses.Helpers;
using ExperienceAndClasses.Recipes;
using Terraria.ID;

namespace ExperienceAndClasses.Items
{
    internal class ClassToken_Hunter : ClassToken_Novice
    {
        public ClassToken_Hunter()
        {
            name = "Hunter";
            tier = 2;
            desc = "Basic ranged class." +
                       "\n\nClass advancement is available at level " + Recipes.Helpers.TIER_LEVEL_REQUIREMENTS[tier + 1] + ".";
        }
        public override void AddRecipes()
        {
            //Recipe recipe = new Recipes.ClassRecipes(Mod, tier);
            //recipe.AddRecipeGroup("Wood", 100);
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Novice").Type, 1 } }, this, 1, recipe);
            int[,] groupIng = new int[,] { { RecipeGroupID.Wood, 100 } };
            RecipeHelper.createCompleteRecipes(this, Mod, null, groupIng, "ClassToken_Novice");
        }
    }
}
