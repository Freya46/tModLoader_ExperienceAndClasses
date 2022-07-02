using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ModLoader;
using ExperienceAndClasses.Helpers;
using Terraria.ID;

namespace ExperienceAndClasses.Items
{
    internal class ClassToken_Hybrid : ClassToken_Novice
    {
        public ClassToken_Hybrid()
        {
            name = "Hybrid";
            tier = 2;
            desc = "Basic hybrid class." +
                       "\n\nCan advance to any Tier III class or to the well-rounded Hybrid II class." +
                       "\n\nClass advancement is available at level " + Recipes.Helpers.TIER_LEVEL_REQUIREMENTS[tier + 1] + ".";
        }
        public override void AddRecipes()
        {
            int[,] vanillaIngredients = new int[,] { { ItemID.DirtBlock, 200 } };
            RecipeHelper.createCompleteRecipes(this, Mod, vanillaIngredients, null, "ClassToken_Novice");


            //Recipe recipe = CreateRecipe();
            //recipe.AddIngredient(Mod.Find<ModItem>("ClassToken_Novice"));
            //recipe.AddIngredient(ItemID.DirtBlock, 200);

            //RecipeHelper.addGenericStuff(recipe, tier, Mod);
            //recipe.Register();

        }
    }
}
