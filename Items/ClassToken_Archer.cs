using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExperienceAndClasses.Helpers;

namespace ExperienceAndClasses.Items
{
    internal class ClassToken_Archer : ClassToken_Novice
    {
        public ClassToken_Archer()
        {
            name = "Archer";
            tier = 3;
            desc = "Archery class." +
                       "\n\nFocuses on archery weapons (bow/crossbow). Gun weapons do not" +
                         "\nrecieve any bonuses.";
        }
        public override void AddRecipes()
        {
            //Recipe recipe;

            //recipe = new Recipes.ClassRecipes(Mod, tier);
            //recipe.AddRecipeGroup("Wood", 500);
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hunter").Type, 1 } }, this, 1, recipe);

            //recipe = new Recipes.ClassRecipes(Mod, tier);
            //recipe.AddRecipeGroup("Wood", 500);
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 } }, this, 1, recipe);

            List<string> altTokens = new List<string>
            {
                "ClassToken_Hunter",
                "ClassToken_Hybrid"
            };
            RecipeHelper.createCompleteRecipes(this, Mod, null, null, altTokens);
        }
    }
}
