using Terraria;
using Terraria.ModLoader;
using Terraria.ID;


namespace ExperienceAndClasses.Recipes
{
    public static class Helpers
    {
        public static int[] TIER_LEVEL_REQUIREMENTS = new int[] { 0, 0, 10, 25 };
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

        public static double getExperienceNeeded(int tier)
        {
            return Methods.Experience.GetExpReqForLevel(TIER_LEVEL_REQUIREMENTS[tier], true);
        }

        public static bool isTokenCraftable(int tier)
        {
            double experienceNeeded = getExperienceNeeded(tier);
            MyPlayer myPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            double exp = myPlayer.GetExp();

            return exp >= experienceNeeded;
        }

        public static void onCraft(Mod mod, int tier, Recipe recipe, Item item)
        {
            bool success = Recipes.Helpers.CraftWithExp(mod, Recipes.Helpers.getExperienceNeeded(tier));

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
    }
}
