﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;
using static Terraria.ModLoader.ModContent;

namespace ExperienceAndClasses.Systems {
    class Password {

        public static string world_password = "";

        public static bool PlayerHasAccess(int player_index) {
            if ((world_password.Length > 0) && (player_index >= 0) && (player_index < Main.maxPlayers) && (Main.player[player_index].active)) {
                return (world_password == Main.player[player_index].GetModPlayer<EACPlayer>().FieldsSyncServer.password);
            }
            else
                return false;
        }

        public static void UpdateLocalPassword() {
            if (Shortcuts.IS_PLAYER) {
                string password_new = Shortcuts.GetConfigClient.Password;

                if (Shortcuts.IS_SINGLEPLAYER) {
                    if (password_new != world_password) {
                        world_password = password_new;
                        Main.NewText(Language.GetTextValue("Mods.ExperienceAndClasses.Common.Password_Display") + ":" + world_password);
                    }
                }
                else if (Shortcuts.IS_CLIENT && Shortcuts.LOCAL_PLAYER_VALID) {
                    if (password_new != Shortcuts.LOCAL_PLAYER.FieldsSyncServer.password) {
                        Shortcuts.LOCAL_PLAYER.FieldsSyncServer.password = password_new;
                        Utilities.PacketHandler.ClientPassword.Send(-1, Shortcuts.WHO_AM_I, Shortcuts.LOCAL_PLAYER.FieldsSyncServer.password);
                    }
                }
            }
        }

        internal class PasswordCommand : ModCommand {
            public override CommandType Type
                => CommandType.Console;

            public override string Command
                => "eac_password";

            public override string Description
                => Language.GetTextValue("Mods.ExperienceAndClasses.Common.Password_Command_Description");

            public override void Action(CommandCaller caller, string input, string[] args) {
                if (args.Length > 0) {
                    world_password = args[0];
                }
                System.Console.WriteLine(Language.GetTextValue("Mods.ExperienceAndClasses.Common.Password_Display") + ":" + world_password);
            }
        }
    }
}