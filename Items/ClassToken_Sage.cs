using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria.ID;
using ExperienceAndClasses.Helpers;

namespace ExperienceAndClasses.Items
{
    internal class ClassToken_Sage : ClassToken_Novice
    {
        public ClassToken_Sage()
        {
            name = "Sage";
            tier = 3;
            desc = "Defensive magic class." +
                       "\n\nMagic damage and mana stats are second to the Mystic, but" +
                         "\nthe Sage has excellent life and defense. Occasionally" +
                         "\nrecovers a percentage of maximum mana. The Sage also produces" +
                         "\nan aura that boosts defense of nearby allies and further" +
                         "\nbolsters the Sage's defenses.";
        }
        public override void AddRecipes()
        {
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Mage").Type, 1 }, {ItemID.FallenStar, 10},
            //    {ItemID.StoneBlock, 500} }, this, 1, new Recipes.ClassRecipes(Mod, tier));
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 }, {ItemID.FallenStar, 10},
            //    {ItemID.StoneBlock, 500} }, this, 1, new Recipes.ClassRecipes(Mod, tier));

            int[,] ing = new int[,] { { ItemID.FallenStar, 10 }, { ItemID.StoneBlock, 500 } };
            List<string> altTokens = new List<string>
            {
                "ClassToken_Mage",
                "ClassToken_Hybrid"
            };

            RecipeHelper.createCompleteRecipes(this, Mod, ing, null, altTokens);
        }
    }
}
