using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria.ID;
using ExperienceAndClasses.Helpers;

namespace ExperienceAndClasses.Items
{
    internal class ClassToken_SoulBinder: ClassToken_Novice
    {
        public ClassToken_SoulBinder()
        {
            name = "Soul Binder";
            tier = 3;
            desc = "Minion quality class." +
                       "\n\nFocuses on quality of minions rather than quantity. Has" +
                         "\nslightly better life and defense than the Minion Master.";
        }
        public override void AddRecipes()
        {
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Summoner").Type, 1 }, { Mod.Find<ModItem>("Monster_Orb").Type, 10 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 }, { Mod.Find<ModItem>("Monster_Orb").Type, 10 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));

            List<string> altTokens = new List<string>
            {
                "ClassToken_Summoner",
                "ClassToken_Hybrid"
            };
            Dictionary<string, int> modIng = new Dictionary<string, int>
            {
                {"Monster_Orb", 10 }
            };

            RecipeHelper.createCompleteRecipes(this, Mod, null, null, altTokens, modIng);
        }
    }
}
