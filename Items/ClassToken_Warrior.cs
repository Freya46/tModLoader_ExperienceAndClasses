using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ExperienceAndClasses.Helpers;

namespace ExperienceAndClasses.Items
{
    internal class ClassToken_Warrior : ClassToken_Novice
    {
        public ClassToken_Warrior()
        {
            name = "Warrior";
            tier = 3;
            desc = "Melee damage and life class." +
                       "\n\nHas the highest melee damage, and the second highest melee speed" +
                         "\nand life.";
        }

        public override void AddRecipes()
        {
            //Recipe recipe1 = CreateRecipe();
            //recipe1.AddIngredient(Mod.Find<ModItem>("ClassToken_Squire"));

            //RecipeHelper.addGenericStuff(recipe1, tier, Mod);
            //recipe1.Register();



            //Recipe recipe2 = CreateRecipe();
            //recipe2.AddIngredient(Mod.Find<ModItem>("ClassToken_Hybrid"));

            //RecipeHelper.addGenericStuff(recipe2, tier, Mod);
            //recipe2.Register();

            List<string> altTokens = new List<string>
            {
                "ClassToken_Squire",
                "ClassToken_Hybrid"
            };

            RecipeHelper.createCompleteRecipes(this, Mod, null, null, altTokens);
        }
    }
}
