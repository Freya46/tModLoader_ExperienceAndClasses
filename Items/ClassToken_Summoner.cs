using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria.ID;
using ExperienceAndClasses.Helpers;

namespace ExperienceAndClasses.Items
{
    internal class ClassToken_Summoner: ClassToken_Novice
    {
        public ClassToken_Summoner()
        {
            name = "Summoner";
            tier = 2;
            desc = "Basic minion class." +
                       "\n\nClass advancement is available at level " + Recipes.Helpers.TIER_LEVEL_REQUIREMENTS[tier + 1] + ".";
        }
        public override void AddRecipes()
        {
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Novice").Type, 1 }, { Mod.Find<ModItem>("Monster_Orb").Type, 1 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));
            Dictionary<string, int> modIng = new Dictionary<string, int>
            {
                {"Monster_Orb", 1 }
            };
            RecipeHelper.createCompleteRecipes(this, Mod, null, null, "ClassToken_Novice", modIng);
        }
    }
}
