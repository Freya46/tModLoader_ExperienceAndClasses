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
    internal class ClassToken_HybridII : ClassToken_Novice
    {
        public ClassToken_HybridII()
        {
            name = "Hybrid II";
            tier = 3;
            desc = "Advanced hybrid class." +
                         "\nA jack-of-all-trades with numerous bonuses and decent" +
                         "\nsurvivability.";
        }
        public override void AddRecipes()
        {
            int[,] vanillaIngredients = new int[,] { { ItemID.DirtBlock, 999 } };
            RecipeHelper.createCompleteRecipes(this, Mod, vanillaIngredients, null, "ClassToken_Hybrid");

            //Recipe recipe = CreateRecipe();
            //recipe.AddIngredient(Mod.Find<ModItem>("ClassToken_Hybrid"));
            //recipe.AddIngredient(ItemID.DirtBlock, 999);

            //RecipeHelper.addGenericStuff(recipe, tier, Mod);

            //recipe.Register();
            // Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 }, { ItemID.DirtBlock, 999 } }, this, 1, new Recipes.ClassRecipes(Mod, tier));
        }
    }
}
