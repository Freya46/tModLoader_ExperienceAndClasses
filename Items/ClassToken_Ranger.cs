using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria.ID;
using ExperienceAndClasses.Helpers;

namespace ExperienceAndClasses.Items
{
    internal class ClassToken_Ranger: ClassToken_Novice
    {
        public ClassToken_Ranger()
        {
            name = "Ranger";
            tier = 3;
            desc = "Generic ranged class." +
                       "\n\nAn unspecialized ranged class. Equally well-suited to archery and" +
                         "\ngun weapons. Has slightly better survivability than Archer and" +
                         "\nGunner, but less damage.";
        }
        public override void AddRecipes()
        {
            //Recipe recipe;

            //recipe = new Recipes.ClassRecipes(Mod, tier);
            //recipe.AddRecipeGroup("IronBar", 50);
            //recipe.AddRecipeGroup("Wood", 250);
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hunter").Type, 1 } }, this, 1, recipe);

            //recipe = new Recipes.ClassRecipes(Mod, tier);
            //recipe.AddRecipeGroup("IronBar", 50);
            //recipe.AddRecipeGroup("Wood", 250);
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 } }, this, 1, recipe);

            int[,] group = new int[,] { { RecipeGroupID.IronBar, 50 }, { RecipeGroupID.Wood, 250 } };
            List<string> altTokens = new List<string>
            {
                "ClassToken_Hunter",
                "ClassToken_Hybrid"
            };
            RecipeHelper.createCompleteRecipes(this, Mod, null, group, altTokens);
        }
    }
}
