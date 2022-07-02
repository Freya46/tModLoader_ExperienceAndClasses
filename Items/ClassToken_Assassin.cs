using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExperienceAndClasses.Helpers;
using Terraria.ID;

namespace ExperienceAndClasses.Items
{
    internal class ClassToken_Assassin : ClassToken_Novice
    {
        public ClassToken_Assassin()
        {
            name = "Assassin";
            tier = 3;
            desc = "Melee critical and agility class." +
                       "\n\nHas the unique ability to Assassinate, which rewards a" +
                         "\n\"hit and run\" playstyle." +
                       "\n\nAssassinate: Occurs when making a melee attack against a target" +
                         "\nwhen you have not landed a hit recently. A buff and visual will" +
                         "\nindicate when it is ready. Yo-yos gain only half of the damage" +
                         "\nmultiplier. Does not trigger on projectile melee attacks such" +
                         "\nas boomerang, some flails and spears, or magic sword projectiles." +
                         "\nBonus critical damage is tripled on Assassinate.";
        }
        public override void AddRecipes()
        {
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Rogue").Type, 1 }, { ItemID.PlatinumCoin, 1 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 }, { ItemID.PlatinumCoin, 1 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));

            List<string> altTokens = new List<string>
            {
                "ClassToken_Rogue",
                "ClassToken_Hybrid"
            };
            int[,] ing = new int[,] { { ItemID.PlatinumCoin, 1 } };
            RecipeHelper.createCompleteRecipes(this, Mod, ing, null, altTokens);
        }
    }
}
