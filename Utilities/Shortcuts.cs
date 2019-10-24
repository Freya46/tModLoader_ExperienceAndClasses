﻿using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace ExperienceAndClasses {
    class Shortcuts {

        //Mod Shortcut
        public static Mod MOD { get; private set; }

        //Hotkeys
        public static ModHotKey HOTKEY_UI { get; private set;}

        //Netmode
        public static bool IS_SERVER { get; private set; }
        public static bool IS_CLIENT { get; private set; }
        public static bool IS_SINGLEPLAYER { get; private set; }
        public static bool IS_PLAYER { get; private set; }
        public static int WHO_AM_I { get; private set; }

        //ModPlayer
        public static EACPlayer LOCAL_PLAYER { get; private set; }
        public static bool LOCAL_PLAYER_VALID { get; private set; }

        /*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ Methods ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*/

        public static void DoModLoad(Mod mod) {
            //mod
            MOD = mod;

            //hotkeys
            HOTKEY_UI = MOD.RegisterHotKey(Language.GetTextValue("Mods.ExperienceAndClasses.Common.Hotkey_UI"), "P");

            //netmode
            UpdateNetmode();

            //clear local player;
            LocalPlayerClear();

            //TODO: textures
            //TODO: sounds
        }

        public static void DoModUnload() {
            //mod
            MOD = null;

            //hotkeys
            HOTKEY_UI = null;

            //clear local player;
            LocalPlayerClear();

            //TODO: textures
            //TODO: sounds
        }

        public static void UpdateNetmode() {
            IS_SERVER = (Main.netMode == NetmodeID.Server);
            IS_CLIENT = (Main.netMode == NetmodeID.MultiplayerClient);
            IS_SINGLEPLAYER = (Main.netMode == NetmodeID.SinglePlayer);

            IS_PLAYER = IS_CLIENT || IS_SINGLEPLAYER;
        }

        public static void LocalPlayerClear() {
            LOCAL_PLAYER = null;
            LOCAL_PLAYER_VALID = false;
            WHO_AM_I = -1;
        }

        public static void LocalPlayerSet(EACPlayer eacplayer) {
            LOCAL_PLAYER = eacplayer;
            LOCAL_PLAYER_VALID = true;
            WHO_AM_I = eacplayer.player.whoAmI;
        }

    }
}
