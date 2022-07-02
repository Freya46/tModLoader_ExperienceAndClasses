using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria.ID;
using ExperienceAndClasses.Helpers;
using ExperienceAndClasses.Abilities;

namespace ExperienceAndClasses.Items
{
    internal class ClassToken_Saint: ClassToken_Novice
    {
        public ClassToken_Saint()
        {
            name = "Saint";
            tier = 3;
            desc = "Advanced support class." +
                       "\n\nSee website for ability descriptions (temporary).";

            // abilities
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Cleric_Active_Heal, 1));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Cleric_Passive_Cleanse, 1));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Cleric_Upgrade_Heal_Smite, 1));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Cleric_Active_Sanctuary, 1));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Cleric_Upgrade_Sanctuary_HolyLight, 1));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Cleric_Alternate_Heal_Barrier, 1));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Cleric_Upgrade_Sanctuary_Blessing, 1));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Saint_Active_DivineIntervention, 25));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Saint_Upgrade_Heal_Cure, 30));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Saint_Upgrade_Sanctuary_Link, 35));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Saint_Upgrade_DivineIntervention_Radius, 45));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Saint_Upgrade_Heal_Purify, 60));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Saint_Active_Paragon, 70));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Saint_Upgrade_DivineIntervention_Duration, 80));
            abilities.Add(new Tuple<AbilityMain.ID, byte>(AbilityMain.ID.Saint_Alternate_Paragon_Renew, 90));
        }
        public override void AddRecipes()
        {
            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Cleric").Type, 1 }, { ItemID.HeartLantern, 1},
            //    { ItemID.StarinaBottle, 1},{ ItemID.Campfire, 10} }, this, 1, new Recipes.ClassRecipes(Mod, tier));

            //Commons.QuckRecipe(Mod, new int[,] { { Mod.Find<ModItem>("ClassToken_Hybrid").Type, 1 }, { ItemID.HeartLantern, 1},
            //    { ItemID.StarinaBottle, 1},{ ItemID.Campfire, 10} }, this, 1, new Recipes.ClassRecipes(Mod, tier));

            int[,] ing = new int[,] { { ItemID.HeartLantern, 1 }, { ItemID.StarinaBottle, 1 }, { ItemID.Campfire, 10 } };
            List<string> altTokens = new List<string>
            {
                "ClassToken_Hybrid",
                "ClassToken_Cleric"
            };
            RecipeHelper.createCompleteRecipes(this, Mod, ing, null, altTokens);
        }
    }
}
