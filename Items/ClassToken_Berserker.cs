using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria.ID;
using ExperienceAndClasses.Helpers;

namespace ExperienceAndClasses.Items
{
    internal class ClassToken_Berserker : ClassToken_Novice
    {
        public ClassToken_Berserker()
        {
            name = "Berserker";
            tier = 3;
            desc = "Melee speed and agility class." +
                       "\n\nHas the highest melee speed as well as moderate life, agility," +
                         "\nand melee damage.";
        }
        public override void AddRecipes()
        {
            //Recipe recipe;

            //recipe = new Recipes.ClassRecipes(Mod, tier);
            //recipe.AddRecipeGroup("IronBar", 100);
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Squire").Type, 1 } }, this, 1, recipe);

            //recipe = new Recipes.ClassRecipes(Mod, tier);
            //recipe.AddRecipeGroup("IronBar", 100);
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 } }, this, 1, recipe);
            int[,] groupIng = new int[,] { { RecipeGroupID.IronBar, 100 } };
            List<string> altTokens = new List<string>
            {
                "ClassToken_Squire",
                "ClassToken_Hybrid"
            };
            RecipeHelper.createCompleteRecipes(this, Mod, null, groupIng, altTokens);
        }
    }
}
