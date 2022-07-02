using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ExperienceAndClasses.Helpers;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;

namespace ExperienceAndClasses.Items
{
    internal class ClassToken_Squire : ClassToken_Novice
    {
        public ClassToken_Squire()
        {
            name = "Squire";
            tier = 2;
            desc = "Basic melee damage and life class." +
                       "\n\nClass advancement is available at level " + Recipes.Helpers.TIER_LEVEL_REQUIREMENTS[tier + 1] + ".";
        }

        public override void AddRecipes()
        {
            //Recipe recipe = CreateRecipe()
            //// .AddIngredient(ItemID.IronBar, 10)
            //.AddRecipeGroup(RecipeGroupID.IronBar, 10)
            //.AddIngredient(Mod.Find<ModItem>("ClassToken_Novice"));

            //RecipeHelper.addGenericStuff(recipe, tier, Mod);
            //recipe.Register();

            int[,] vanillaRecipeGroupIngredients = new int[,] { { RecipeGroupID.IronBar, 10} };
            //int[,] vanillaIngredients = new int[,] { { ItemID.IronBar, 10 } };
            RecipeHelper.createCompleteRecipes(this, Mod, null, vanillaRecipeGroupIngredients, "ClassToken_Novice");
        }
    }
}
