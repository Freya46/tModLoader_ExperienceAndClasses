using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using ExperienceAndClasses.Helpers;
using Terraria.Localization;

namespace ExperienceAndClasses.Items
{
    internal class ClassToken_Tank: ClassToken_Novice
    {
        public ClassToken_Tank()
        {
            name = "Tank";
            tier = 3;
            desc = "Tank class." +
                       "\n\nHas the highest life, defense, and aggro. Occasionally recovers" +
                       "\na percentage of maximum life.";
        }

        public override void AddRecipes()
        {
            //Recipe recipe1 = CreateRecipe()
            //.AddIngredient(Mod.Find<ModItem>("ClassToken_Squire"));

            //RecipeHelper.addGenericStuff(recipe1, tier, Mod);
            //recipe1.Register();


            //Recipe recipe2 = CreateRecipe()
            //.AddIngredient(Mod.Find<ModItem>("ClassToken_Hybrid"));

            //RecipeHelper.addGenericStuff(recipe2, tier, Mod);
            //recipe2.Register();

            List<string> alternativeClassTokenStrings = new List<string>();
            alternativeClassTokenStrings.Add("ClassToken_Squire");
            alternativeClassTokenStrings.Add("ClassToken_Hybrid");

            RecipeHelper.createCompleteRecipes(this, Mod, null, null, alternativeClassTokenStrings);
        }
    }
}
