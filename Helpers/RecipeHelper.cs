using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using ExperienceAndClasses.Items;
using ExperienceAndClasses.Recipes;

namespace ExperienceAndClasses.Helpers
{
    internal class RecipeHelper
    {
        public static bool isTokenCraftable(int tier)
        {
            double experienceNeeded = Recipes.Helpers.getExperienceNeeded(tier);
            MyPlayer myPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            double exp = myPlayer.GetExp();

            return exp >= experienceNeeded;
        }

        public static void onCraft(Mod mod, int tier, Recipe recipe, Item item)
        {
            bool success = CraftWithExp(mod, Recipes.Helpers.getExperienceNeeded(tier));

            if (success)
            {
                item.prefix = 0;
                item.rare = ItemRarityID.White;

                //tell server to announce (no effect in single player)
                Methods.PacketSender.ClientTellAnnouncement(mod, Main.LocalPlayer.name + " has completed " + item.Name + "!", 255, 255, 0);
            }
            else
            {
                Main.mouseItem.stack--;
            }
        }

        /// <summary>
        /// Returns true if the player had enough experience, else false.
        /// If this returns false in a OnCraft, you must "Main.mouseItem.stack--;" to prevent exploit.
        /// </summary>
        /// <param name="mod"></param>
        /// <param name="experienceNeeded"></param>
        /// <returns></returns>
        public static bool CraftWithExp(Mod mod, double experienceNeeded)
        {
            MyPlayer myPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();

            if (myPlayer.GetExp() >= experienceNeeded) //have enough exp
            {
                //take exp
                if (experienceNeeded > 0)
                {
                    if (Main.netMode == 0)
                        myPlayer.SubtractExp(experienceNeeded);
                    else
                    {
                        //tell server to reduce experience
                        Methods.PacketSender.ClientTellAddExp(mod, -experienceNeeded);
                    }
                }

                //success
                return true;
            }
            else
            {
                //fail
                return false;
            }
        }

        private static Recipe addTierIngredients(Recipe recipe, int tier, Mod mod)
        {
            if (tier == 2)
            {
                recipe.AddIngredient(mod.Find<ModItem>("Monster_Orb"), 1);
            }
            else if (tier == 3)
            {
                recipe.AddIngredient(mod.Find<ModItem>("Boss_Orb"), 5);
                recipe.AddIngredient(mod.Find<ModItem>("Monster_Orb"), 50);
            }

            return recipe;
        }

        private static Recipe addOnCraftCallback(Recipe recipe, int tier, Mod mod)
        {
            recipe.AddOnCraftCallback(delegate (Recipe recipe, Item item)
             {
                 onCraft(mod, tier, recipe, item);
             });

            return recipe;
        }

        private static Recipe addCondition(Recipe recipe, int tier)
        {
            recipe.AddCondition(NetworkText.FromLiteral("Experience"), recipe =>
             {
                 return isTokenCraftable(tier);
             });

            return recipe;
        }

        private static Recipe addGenericStuff(Recipe recipe, int tier, Mod mod)
        {
            addCondition(recipe, tier);
            addOnCraftCallback(recipe, tier, mod);
            addTierIngredients(recipe, tier, mod);

            return recipe;
        }

        public static List<Recipe> createCompleteRecipes(ClassToken_Novice classToken, Mod mod, int[,] vanillaIngredients, int[,] vanillaRecipeGroupIngredients, List<string> alternativeClassTokenNames, Dictionary<string, int> modIngredients = null)
        {
            List<Recipe> recipes = new List<Recipe>();

            alternativeClassTokenNames.ForEach(delegate (string modItemName)
            {
                Recipe recipe = classToken.CreateRecipe();
                ModItem modItem = mod.Find<ModItem>(modItemName);

                recipe.AddIngredient(modItem);

                if (vanillaIngredients != null)
                {
                    for (int i = 0; i < vanillaIngredients.GetLength(0); i++)
                    {
                        recipe.AddIngredient(vanillaIngredients[i, 0], vanillaIngredients[i, 1]);
                    }
                }

                if (vanillaRecipeGroupIngredients != null)
                {
                    for (int i = 0; i < vanillaRecipeGroupIngredients.GetLength(0); i++)
                    {
                        recipe.AddRecipeGroup(vanillaRecipeGroupIngredients[i, 0], vanillaRecipeGroupIngredients[i, 1]);
                    }
                }

                if(modIngredients != null)
                {
                    foreach(KeyValuePair<string, int> entry in modIngredients)
                    {
                        ModItem modItemIngredient = mod.Find<ModItem>(entry.Key);
                        recipe.AddIngredient(modItemIngredient, entry.Value);
                    }
                }


                addGenericStuff(recipe, classToken.tier, mod);
                recipe.Register();
            });

            return recipes;
        }

        public static List<Recipe> createCompleteRecipes(ClassToken_Novice classToken, Mod mod, int[,] vanillaIngredients, int[,] vanillaRecipeGroupIngredients, string classTokenName, Dictionary<string, int> modIngredients = null)
        {
            List<string> alternativeModIngredients = new List<string>
            {
                classTokenName
            };

            return createCompleteRecipes(classToken, mod, vanillaIngredients, vanillaRecipeGroupIngredients, alternativeModIngredients, modIngredients);
        }
    }
}
