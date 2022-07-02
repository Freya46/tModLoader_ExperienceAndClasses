using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria.ID;
using ExperienceAndClasses.Helpers;

namespace ExperienceAndClasses.Items
{
    internal class ClassToken_Ninja: ClassToken_Novice
    {
        public ClassToken_Ninja()
        {
            name = "Ninja";
            tier = 3;
            desc = "Throwing and agility class." +
                       "\n\nTo make throwing builds viable, Ninja has the highest" +
                         "\ndamage modifier of any class. Ninja also has excellent" +
                         "\nagility including the highest jump bonus.";
        }
        public override void AddRecipes()
        {
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Rogue").Type, 1 }, { ItemID.PlatinumCoin, 1 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 }, { ItemID.PlatinumCoin, 1 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));

            int[,] ing = new int[,] { { ItemID.PlatinumCoin, 1 } };
            List<string> altTokens = new List<string>
            {
                "ClassToken_Rogue",
                "ClassToken_Hybrid"
            };
            RecipeHelper.createCompleteRecipes(this, Mod, ing, null, altTokens);
        }
    }
}
