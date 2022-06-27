﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExperienceAndClasses.Recipes
{
    /* Recipes that take experience */
    class ExpRecipe : Recipe
    {
        public double experienceNeeded = 0;

        public ExpRecipe(Mod mod, double experienceNeeded) : base(mod)
        {
            this.experienceNeeded = experienceNeeded;
        }

        public override bool RecipeAvailable()
        {
            if (Main.LocalPlayer.GetModPlayer<MyPlayer>(mod).GetExp() < experienceNeeded)
                return false;
            else
                return Commons.EnforceDuplicatesInRecipe(this);//base.RecipeAvailable();
        }

        public override void OnCraft(Item item)
        {
            if (Helpers.CraftWithExp(mod, experienceNeeded))
            {
                //success - do craft
                base.OnCraft(item);
            }
            else
            {
                //fail - remove the item if crafted
                base.OnCraft(item);
                Main.mouseItem.stack--;
            }
        }
    }

    /* Class Token recipe bases (take exp and standard item requirements, remove prefix, announce) */
    class ClassRecipes : ExpRecipe
    {
        public static int[] TIER_LEVEL_REQUIREMENTS = new int[] { 0, 0, 10, 25 };
        public ClassRecipes(Mod mod, int tier) : base(mod, 0)
        {
            //exp needed
            base.experienceNeeded = Methods.Experience.GetExpReqForLevel(TIER_LEVEL_REQUIREMENTS[tier], true);

            //items needed
            if (tier == 2)
            {
                AddIngredient(mod.ItemType<Items.Monster_Orb>(), 1);
            }
            else if (tier == 3)
            {
                AddIngredient(mod.ItemType<Items.Boss_Orb>(), 5);
                AddIngredient(mod.ItemType<Items.Monster_Orb>(), 50);
            }
        }
        public override void OnCraft(Item item)
        {
            //get exp prior to crafting
            MyPlayer myPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>(mod);
            double exp = myPlayer.GetExp();

            //do base (normally this can fail in ExpRecipe, but tokens don't stack so it shouldn't be possible here)
            base.OnCraft(item);

            //if there was enough exp to craft (this should always be the case because tokens cannot stack)
            if (exp >= experienceNeeded)
            {
                //remove prefix
                item.prefix = 0;
                item.rare = createItem.rare;

                //tell server to announce (no effect in single player)
                Methods.PacketSender.ClientTellAnnouncement(mod, Main.LocalPlayer.name + " has completed " + createItem.Name + "!", 255, 255, 0);
            }
        }
    }

    /* Recipes that are available at specified class tiers (token must be equipped, else tier 1) */
    class TierRecipe : Recipe
    {
        int tier;
        bool includeLowerTier;
        bool includeHigherTier;
        int levelMin;
        int levelMax;
        public TierRecipe(Mod mod, int tier, bool includeLowerTier, bool includeHigherTier, int levelMin, int levelMax) : base(mod)
        {
            this.tier = tier;
            this.includeLowerTier = includeLowerTier;
            this.includeHigherTier = includeHigherTier;
            this.levelMin = levelMin;
            this.levelMax = levelMax;
        }

        public override bool RecipeAvailable()
        {
            Player player = Main.LocalPlayer;
            MyPlayer myPlayer = player.GetModPlayer<MyPlayer>(mod);
            int tierCurrent = Methods.Experience.GetTier(player);
            int levelCurrent = Methods.Experience.GetLevel(myPlayer.GetExp());

            if (levelMin >= 0 && levelCurrent < levelMin) return false;
            if (levelMax >= 0 && levelCurrent > levelMax) return false;

            if (tierCurrent == tier || (tierCurrent > tier && includeHigherTier) || (tierCurrent < tier && includeLowerTier)) return Commons.EnforceDuplicatesInRecipe(this);
            else return false;
        }
    }
}
