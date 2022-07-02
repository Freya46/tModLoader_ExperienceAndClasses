using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria.ID;
using ExperienceAndClasses.Helpers;
using Terraria.ModLoader;

namespace ExperienceAndClasses.Items
{
    internal class ClassToken_MinionMaster : ClassToken_Novice
    {
        public ClassToken_MinionMaster()
        {
            name = "Minion Master";
            tier = 3;
            desc = "Minion quantity class." +
                       "\n\nFocuses on quantity of minions rather than quality. Has" +
                         "\nslightly worse life and defense than the Soul Binder, but" +
                         "\nthis is offset by sheer numbers." +
                       "\n\nBe aware that many minions deal piecing damage and the game" +
                         "\nhas a limit on how often a single target can be hit by piecing" +
                         "\nattacks. It is possible to exceed this limit with these types" +
                         "\nof minions on a high level Minion Master, which reduces" +
                         "\neffective single target damage.";
        }
        public override void AddRecipes()
        {
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Summoner").Type, 1 }, { Mod.Find<ModItem>("Monster_Orb").Type, 10 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 }, { Mod.Find<ModItem>("Monster_Orb").Type, 10 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));
            Dictionary<string, int> modIng = new Dictionary<string, int>
            {
                {"Monster_Orb", 10 }
            };
            List<string> altTokens = new List<string>
            {
                "ClassToken_Summoner",
                "ClassToken_Hybrid"
            };
            RecipeHelper.createCompleteRecipes(this, Mod, null, null, altTokens, modIng);
        }
    }
}
