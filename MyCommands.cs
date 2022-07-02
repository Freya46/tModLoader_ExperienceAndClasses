﻿using System;
using Terraria;
using Terraria.ModLoader;

namespace ExperienceAndClasses
{
    class Command_expstatusmsg : CommandTemplate
    {
        public Command_expstatusmsg()
        {
            name = "expstatusmsg";
            argstr = "";
            desc = "toggle showing status messages";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            ExperienceAndClasses.localMyPlayer.show_status_messages = !ExperienceAndClasses.localMyPlayer.show_status_messages;
            if (ExperienceAndClasses.localMyPlayer.show_status_messages)
                Main.NewText("Status messages enabled.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
            else
                Main.NewText("Status messages disabled.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
        }
    }

    class Command_expkillcount : CommandTemplate
    {
        public Command_expkillcount()
        {
            name = "expkillcount";
            argstr = "";
            desc = "toggle showing kill count of recently slain monsters";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            ExperienceAndClasses.localMyPlayer.show_kill_count = !ExperienceAndClasses.localMyPlayer.show_kill_count;
            if (ExperienceAndClasses.localMyPlayer.show_kill_count)
                Main.NewText("Kill count enabled.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
            else
                Main.NewText("Kill count disabled.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
        }
    }

    class Command_expabilityoverhead : CommandTemplate
    {
        public Command_expabilityoverhead()
        {
            name = "expabilityoverhead";
            argstr = "";
            desc = "toggle showing ability messages overhead";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            ExperienceAndClasses.localMyPlayer.ability_message_overhead = !ExperienceAndClasses.localMyPlayer.ability_message_overhead;
            if (ExperienceAndClasses.localMyPlayer.ability_message_overhead)
                Main.NewText("Overhead messages enabled.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
            else
                Main.NewText("Overhead messages disabled.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
        }
    }

    class Command_expuibarcd : CommandTemplate
    {
        public Command_expuibarcd()
        {
            name = "expuibarcd";
            argstr = "";
            desc = "toggle cooldown bars in the experience and cooldown ui";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            MyPlayer localMyPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            localMyPlayer.UICDBars = !localMyPlayer.UICDBars;
            if (localMyPlayer.UICDBars)
                Main.NewText("Cooldown bars enabled.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
            else
                Main.NewText("Cooldown bars disabled.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
        }
    }

    class Command_expcd : CommandTemplate
    {
        public Command_expcd()
        {
            name = "expcd";
            argstr = "seconds";
            desc = "toggle whether cooldown messages are displayed";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            MyPlayer localMyPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            float thresh = float.Parse(args[0]);
            if (thresh < 0f) thresh = -1f;
            localMyPlayer.thresholdCDMsg = thresh;
            if (localMyPlayer.thresholdCDMsg >= 0f)
                Main.NewText("Cooldown message threshold is " + localMyPlayer.thresholdCDMsg + " seconds.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
            else
                Main.NewText("Cooldown messages disabled.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
        }
    }

    class Command_expafk : CommandTemplate //allowAFK
    {
        public Command_expafk()
        {
            name = "expafk";
            argstr = "";
            desc = "toggle whether to enter AFK mode after " + ExperienceAndClasses.AFK_TIME_TICKS_SEC + " seconds";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            MyPlayer localMyPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            localMyPlayer.allowAFK = !localMyPlayer.allowAFK;
            if (localMyPlayer.allowAFK)
                Main.NewText("AFK mode enabled.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
            else
                Main.NewText("AFK mode disabled.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
        }
    }

    class Command_expui : CommandTemplate
    {
        public Command_expui()
        {
            name = "expui";
            argstr = "";
            desc = "toggle experience and cooldown ui visibility";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            MyPlayer localMyPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            localMyPlayer.UIShow = !localMyPlayer.UIShow;

            if (localMyPlayer.UIShow)
                Main.NewText("Experience and cooldown ui enabled.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
            else
                Main.NewText("Experience and cooldown ui disabled.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
        }
    }

    class Command_expuiinv : CommandTemplate
    {
        public Command_expuiinv()
        {
            name = "expuiinv";
            argstr = "";
            desc = "toggle always showing experience and cooldown ui in inventory screen";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            MyPlayer localMyPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            localMyPlayer.UIInventory = !localMyPlayer.UIInventory;

            if (localMyPlayer.UIInventory)
                Main.NewText("Experience and cooldown ui forced in inventory.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
            else
                Main.NewText("Experience and cooldown ui not forced in inventory.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
        }
    }

    class Command_expuibar : CommandTemplate
    {
        public Command_expuibar()
        {
            name = "expuibar";
            argstr = "";
            desc = "toggle experience bar in the experience and cooldown ui";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            MyPlayer localMyPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            localMyPlayer.UIExpBar = !localMyPlayer.UIExpBar;
            if (localMyPlayer.UIExpBar)
                Main.NewText("Experience bar enabled.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
            else
                Main.NewText("Experience bar disabled.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
        }
    }

    class Command_expuitrans : CommandTemplate
    {
        public Command_expuitrans()
        {
            name = "expuitrans";
            argstr = "";
            desc = "toggle experience and cooldown ui transparency";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            MyPlayer localMyPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            //UI.UIExp myUI = (mod as ExperienceAndClasses).uiExp;

            localMyPlayer.UITrans = !localMyPlayer.UITrans;
            UI.UIExp.SetTransparency(localMyPlayer.UITrans);
            if
                (localMyPlayer.UITrans) Main.NewText("Experience bar is now transparent.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
            else
                Main.NewText("Experience bar is now opaque.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
        }
    }

    class Command_expuireset : CommandTemplate
    {
        public Command_expuireset()
        {
            name = "expuireset";
            argstr = "";
            desc = "reset experience and cooldown ui";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            MyPlayer localMyPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            UI.UIExp myUI = ExperienceAndClasses.uiExp;

            UI.UIExp.SetPosition(400f, 100f);
            localMyPlayer.UIShow = true;
            localMyPlayer.UIExpBar = true;
            localMyPlayer.UITrans = false;
            UI.UIExp.SetTransparency(localMyPlayer.UITrans);
            //myUI.Update();
            Main.NewText("Experience bar reset.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
        }
    }

    class Command_explist : CommandTemplate
    {
        public Command_explist()
        {
            name = "explist";
            argstr = "";
            desc = "lists the level and class of all players";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Player player;
            double exp = 0, expHave, expNeed;
            int level;
            string job, message = "Current Players:";
            for (int playerIndex = 0; playerIndex < 255; playerIndex++)
            {
                player = Main.player[playerIndex];
                if (player.active)
                {
                    //temp
                    exp = Main.LocalPlayer.GetModPlayer<MyPlayer>().GetExp();
                    //Main.NewText(player.name + "=" + exp);

                    job = Methods.Experience.GetClass(player);
                    level = Methods.Experience.GetLevel(exp);

                    expHave = Methods.Experience.GetExpTowardsNextLevel(exp);
                    expNeed = Methods.Experience.GetExpReqForLevel(level + 1, false);

                    message += "\n" + player.name + ", Level " + level + "(" + Math.Round((double)expHave / (double)expNeed * 100, 2) + "%), " + job;
                }
            }
            Main.NewTextMultiline(message, false, ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
        }
    }

    class Command_expadd : CommandTemplate
    {
        public Command_expadd()
        {
            name = "expadd";
            argstr = "exp_amount";
            desc = "adds an amount of experience (requires expauth in multiplayer)";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            MyPlayer localMyPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();

            double exp = Double.Parse(args[0]);
            Methods.ChatCommands.CommandSetExp(Mod, localMyPlayer.GetExp() + exp, input);
        }
    }

    class Command_expsub : CommandTemplate
    {
        public Command_expsub()
        {
            name = "expsub";
            argstr = "exp_amount";
            desc = "subtracts an amount of experience (requires expauth in multiplayer)";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            MyPlayer localMyPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();

            double exp = Double.Parse(args[0]);
            Methods.ChatCommands.CommandSetExp(Mod, localMyPlayer.GetExp() - exp, input);
        }
    }

    class Command_expset : CommandTemplate
    {
        public Command_expset()
        {
            name = "expset";
            argstr = "exp_amount";
            desc = "sets amount of experience (requires expauth in multiplayer)";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            double exp = Double.Parse(args[0]);
            Methods.ChatCommands.CommandSetExp(Mod, exp, input);
        }
    }

    class Command_exprate : CommandTemplate
    {
        public Command_exprate()
        {
            name = "exprate";
            argstr = "[new_rate]";
            desc = "displays or sets experience rate (requires expauth in multiplayer)";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (args.Length == 0)
            {
                Main.NewText("The current exprate is " + (ExperienceAndClasses.worldExpModifier * 100) + "%.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
            }
            else
            {
                double rate = Double.Parse(args[0]);
                Methods.ChatCommands.CommandSetExpRate(Mod, rate / 100, input);
            }
        }
    }

    class Command_expdeath : CommandTemplate
    {
        public Command_expdeath()
        {
            name = "expdeath";
            argstr = "[new_rate]";
            desc = "displays or sets experience penalty on death (requires expauth in multiplayer)";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (args.Length == 0)
            {
                Main.NewText("The current death penalty is " + (ExperienceAndClasses.worldDeathPenalty * 100) + "%.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
            }
            else
            {
                double rate = Double.Parse(args[0]);
                Methods.ChatCommands.CommandSetDeathPenalty(Mod, rate / 100, input);
            }
        }
    }

    class Command_explvladd : CommandTemplate
    {
        public Command_explvladd()
        {
            name = "explvladd";
            argstr = "[level_amount]";
            desc = "add an amount of levels (requires expauth in multiplayer)";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            MyPlayer localMyPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();

            int amt;
            if (args.Length == 0) amt = 1;
            else amt = Int32.Parse(args[0]);

            double exp = localMyPlayer.GetExp();
            int level = Methods.Experience.GetLevel(exp) + amt;
            exp = Methods.Experience.GetExpReqForLevel(level, true);

            Methods.ChatCommands.CommandSetExp(Mod, exp, input);
        }
    }

    class Command_explvlsub : CommandTemplate
    {
        public Command_explvlsub()
        {
            name = "explvlsub";
            argstr = "[level_amount]";
            desc = "subtract an amount of levels (requires expauth in multiplayer)";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            MyPlayer localMyPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();

            int amt;
            if (args.Length == 0) amt = 1;
            else amt = Int32.Parse(args[0]);

            double exp = localMyPlayer.GetExp();
            int level = Methods.Experience.GetLevel(exp) - amt;
            exp = Methods.Experience.GetExpReqForLevel(level, true);
            if (exp < 0) exp = 0;

            Methods.ChatCommands.CommandSetExp(Mod, exp, input);
        }
    }

    class Command_explvlset : CommandTemplate
    {
        public Command_explvlset()
        {
            name = "explvlset";
            argstr = "level_amount";
            desc = "set amount of levels (requires expauth in multiplayer)";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (args.Length == 0) return;

            int level = Int32.Parse(args[0]);
            Methods.ChatCommands.CommandSetExp(Mod, Methods.Experience.GetExpReqForLevel(level, true), input);
        }
    }

    class Command_expmsg : CommandTemplate
    {
        public Command_expmsg()
        {
            name = "expmsg";
            argstr = "";
            desc = "toggle exp gain messages";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            MyPlayer localMyPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();

            localMyPlayer.displayExp = !localMyPlayer.displayExp;
            if (localMyPlayer.displayExp)
            {
                Main.NewText("Experience messages enabled.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
            }
            else
            {
                Main.NewText("Experience messages disabled.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
            }
        }
    }

    class Command_expclasscaps : CommandTemplate
    {
        public Command_expclasscaps()
        {
            name = "expclasscaps";
            argstr = "";
            desc = "toggle enforcing class bonus caps (map setting, requires expauth in multiplayer)";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Methods.ChatCommands.CommandToggleCaps(Mod, input);
        }
    }

    class Command_expauth : CommandTemplate
    {
        public Command_expauth()
        {
            name = "expauth";
            argstr = "[auth_code]";
            desc = "displays auth status or attempts authorization with the auth_code";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (Main.netMode == 0)
                Main.NewText("Auth is only for multiplayer use.");
            else
            {
                if (args.Length == 0)
                {
                    Methods.ChatCommands.CommandAuth(Mod, -1);
                }
                else
                {
                    double code = Double.Parse(args[0]);
                    Methods.ChatCommands.CommandAuth(Mod, code);
                }
            }
        }
    }

    class Command_explvlcap : CommandTemplate
    {
        public Command_explvlcap()
        {
            name = "explvlcap";
            argstr = "[level_cap]";
            desc = "display or set the level cap (map setting, requires expauth in multiplayer, 0 to disable)";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (args.Length == 0)
            {
                if (ExperienceAndClasses.worldLevelCap <= 0)
                {
                    Main.NewText("Level cap is disabled.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
                }
                else
                {
                    Main.NewText("Level cap is " + ExperienceAndClasses.worldLevelCap + ".", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
                }
            }
            else
            {
                int lvl = Int32.Parse(args[0]);
                Methods.ChatCommands.CommandLvlCap(Mod, lvl, input);
            }
        }
    }

    class Command_expdmgred : CommandTemplate
    {
        public Command_expdmgred()
        {
            name = "expdmgred";
            argstr = "[damage_reduction_percent]";
            desc = "display or set the class damage reduction (map setting, requires expauth in multiplayer, 0 to disable)";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (args.Length == 0)
            {
                if (ExperienceAndClasses.worldClassDamageReduction <= 0)
                {
                    Main.NewText("Damage reduction is disabled.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
                }
                else
                {
                    Main.NewText("Damage reduction is " + ExperienceAndClasses.worldClassDamageReduction + "%.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
                }
            }
            else
            {
                int dmgred = Int32.Parse(args[0]);
                if (dmgred <= 0) dmgred = -1;
                Methods.ChatCommands.CommandDmgRed(Mod, dmgred, input);
            }
        }
    }

    class Command_expnoauth : CommandTemplate
    {
        public Command_expnoauth()
        {
            name = "expnoauth";
            argstr = "";
            desc = "toggles auth requirement for current map (requires expauth in multiplayer)";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Methods.ChatCommands.CommandmapRequireAuth(Mod, input);
        }
    }

    class Command_exptrace : CommandTemplate
    {
        public Command_exptrace()
        {
            name = "exptrace";
            argstr = "";
            desc = "toggles trace for debuging this character";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            MyPlayer localMyPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>();
            localMyPlayer.traceChar = !localMyPlayer.traceChar;
            if (localMyPlayer.traceChar)
                Main.NewText("Character trace is enabled.", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
            else
                Main.NewText("Character trace is disabled", ExperienceAndClasses.MESSAGE_COLOUR_YELLOW);
        }
    }

    class Command_expmaptrace : CommandTemplate
    {
        public Command_expmaptrace()
        {
            name = "expmaptrace";
            argstr = "";
            desc = "toggles trace for debuging this map (requires expauth in multiplayer)";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Methods.ChatCommands.CommandMapTrace(Mod, input);
        }
    }

    class Command_expmapauthcode : CommandTemplate
    {
        public Command_expmapauthcode()
        {
            name = "expmapauthcode";
            argstr = "[new_auth_code]";
            desc = "display or change the map expauth code (code changes always requires expauth in multiplayer)";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (args.Length == 0)
            {
                Methods.ChatCommands.CommandShowmapAuthCode();
            }
            else
            {
                double newCode = Double.Parse(args[0]);
                if (newCode < 0) newCode = 0;
                newCode = Math.Round(newCode);
                Methods.ChatCommands.CommandSetmapAuthCode(Mod, newCode, input);
            }
        }
    }

    class Command_expsettings : CommandTemplate
    {
        public Command_expsettings()
        {
            name = "expsettings";
            argstr = "";
            desc = "displays the current settings";
        }

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Methods.ChatCommands.CommandDisplaySettings(Mod);
        }
    }

    public abstract class CommandTemplate : ModCommand
    {
        public string name, argstr, desc;

        public override CommandType Type
        {
            get { return CommandType.Chat; }
        }

        public override string Command
        {
            get { return name; }
        }

        public override string Usage
        {
            get { return "/"+name+" "+argstr; }
        }

        public override string Description
        {
            get { return "ExperienceAndClasses: "+desc; }
        }
    }
}
