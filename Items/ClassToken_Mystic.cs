using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria.ID;
using ExperienceAndClasses.Helpers;

namespace ExperienceAndClasses.Items
{
    internal class ClassToken_Mystic : ClassToken_Novice
    {
        public ClassToken_Mystic()
        {
            name = "Mystic";
            tier = 3;
            desc = "Magic damage class." +
                       "\n\nHas the highest magic damage, mana, mana regen, and mana cost" +
                         "\nreduction. This is the only class with magic crit. Occasionally" +
                         "\nrecovers a percentage of maximum mana.";
        }
        public override void AddRecipes()
        {
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Mage").Type, 1 }, { ItemID.FallenStar, 20 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 }, { ItemID.FallenStar, 20 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));
            int[,] ing = new int[,] { { ItemID.FallenStar, 20 } };
            List<string> altTokens = new List<string>
            {
                "ClassToken_Mage",
                "ClassToken_Hybrid"
            };
            RecipeHelper.createCompleteRecipes(this, Mod, ing, null, altTokens);
        }
    }
}
