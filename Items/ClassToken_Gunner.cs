using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExperienceAndClasses.Helpers;
using Terraria.ID;

namespace ExperienceAndClasses.Items
{
    // Hunter - Gunner
    internal class ClassToken_Gunner : ClassToken_Novice
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
            //Recipe recipe;

            //recipe = new Recipes.ClassRecipes(Mod, tier);
            //recipe.AddRecipeGroup("IronBar", 100);
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hunter").Type, 1 } }, this, 1, recipe);

            //recipe = new Recipes.ClassRecipes(Mod, tier);
            //recipe.AddRecipeGroup("IronBar", 100);
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 } }, this, 1, recipe);
            int[,] groupIng = new int[,] { { RecipeGroupID.IronBar, 100 } };
            List<string> altTokens = new List<string>
            {
                "ClassToken_Hunter",
                "ClassToken_Hybrid"
            };
            RecipeHelper.createCompleteRecipes(this, Mod, null, groupIng, altTokens);
        }
    }
}
