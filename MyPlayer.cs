﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.Collections.Generic;

namespace ExperienceAndClasses
{
    public class MyPlayer : ModPlayer
    {
        public static double MAX_EXPERIENCE = Methods.Experience.GetExpReqForLevel(ExperienceAndClasses.MAX_LEVEL, true);

        public bool auth = false;

        public double experience = -1;
        public double experienceModifier = 1;

        public int explvlcap = -1;
        public int expdmgred = -1;

        public bool displayExp = false;
        public bool ignoreCaps = false;

        public float UILeft = 400f;
        public float UITop = 100f;
        public bool UIShow = true;
        public bool UITrans = false;

        public double bonusCritPct = 0;
        public double openerBonusPct = 0;
        public int openerTime_msec = 0;
        public DateTime timeLastAttack = DateTime.MinValue;

        public float percentMidas = 0;

        public bool hasLootedMonsterOrb = false;

        
        /// <summary>
        /// Returns experience total.
        /// </summary>
        /// <returns></returns>
        public double GetExp()
        {
            return experience;
        }

        /// <summary>
        /// Adds experience. For use in single-player or by server.
        /// </summary>
        /// <param name="xp"></param>
        public void AddExp(double xp)
        {
            /*~~~~~~~~~~~~~~~~~~~~~~Single Player and Server Only~~~~~~~~~~~~~~~~~~~~~~*/
            if (Main.netMode == 1) return;

            SetExp(experience + xp);
        }

        //take xp from player
        public void SubtractExp(double xp)
        {
            /*~~~~~~~~~~~~~~~~~~~~~~Single Player and Server Only~~~~~~~~~~~~~~~~~~~~~~*/
            if (Main.netMode == 1) return;

            SetExp(experience - xp);
        }

        //set xp of player
        public void SetExp(double xp)
        {
            /*~~~~~~~~~~~~~~~~~~~~~~Single Player and Server Only~~~~~~~~~~~~~~~~~~~~~~*/
            if (Main.netMode == 1) return;

            //in the rare case that the player is not synced with the server, don't do anything
            if (Main.netMode == 2 && experience == -1)
            {
                NetMessage.SendData(25, -1, -1, "Failed to change the experience value for player #"+player.whoAmI+":"+player.name +" (player not yet synced)", 255, 255, 0, 0, 0);
                return;
            }

            double priorExp = GetExp();
            int priorLevel = Methods.Experience.GetLevel(GetExp());
            experience = xp;
            LimitExp();
            LevelUp(priorLevel);

            //if server, tell client
            if (Main.netMode == 2)
            {
                Methods.PacketSender.ServerForceExperience(player);
            }
            else if (Main.netMode==0)
            {
                ExpMsg(experience - priorExp);
            }
        }

        /// <summary>
        /// Keep experience between zero and max.
        /// </summary>
        public void LimitExp()
        {
            /*~~~~~~~~~~~~~~~~~~~~~~Single Player and Server Only~~~~~~~~~~~~~~~~~~~~~~*/
            if (Main.netMode == 1) return;

            if (experience < 0) experience = 0;
            if (experience > MAX_EXPERIENCE) experience = MAX_EXPERIENCE;

            if (explvlcap!=-1)
            {
                double expCap = Methods.Experience.GetExpReqForLevel(explvlcap, true);
                if (experience > expCap) experience = expCap;
            }
        }

        /// <summary>
        /// Displays level-up/down messages. For use in single-player or by server.
        /// </summary>
        /// <param name="priorLevel"></param>
        public void LevelUp(int priorLevel)
        {
            /*~~~~~~~~~~~~~~~~~~~~~~Single Player and Server Only~~~~~~~~~~~~~~~~~~~~~~*/
            if (Main.netMode == 1) return;

            int level = Methods.Experience.GetLevel(GetExp());
            if (level>priorLevel)
            {
                if (Main.netMode == 0) Main.NewText("You have reached level " + level + "!");
                    else NetMessage.SendData(25, -1, -1, player.name+" has reached level "+level+"!", 255, 0, 255, 0, 0);
            }
            else if (level<priorLevel)
            {
                if (Main.netMode == 0) Main.NewText("You has fallen to level " + level + "!");
                    else NetMessage.SendData(25, -1, -1, player.name + " has dropped to level " + level + "!", 255, 255, 0, 0, 0);
            }
        }

        /// <summary>
        /// Displays the local "You have gained/lost x experience." message. Loss is always displayed.
        /// </summary>
        /// <param name="experienceChange"></param>
        public void ExpMsg(double experienceChange)
        {
            if (!Main.LocalPlayer.Equals(player)) return;

            if (experienceChange>0 && displayExp)
            {
                Main.NewText("You have earned " + (int)experienceChange + " experience.");
            }
            else if (experienceChange<0)
            {
                Main.NewText("You have lost " + (int)(experienceChange * -1) + " experience.");
            }
        }

        //save xp
        public override TagCompound Save()
        {
            UILeft = (mod as ExperienceAndClasses).myUI.getLeft();
            UITop = (mod as ExperienceAndClasses).myUI.getTop();

            return new TagCompound {
                { "experience", experience},
                {"experience_modifier", experienceModifier},
                {"display_exp", displayExp},
                {"ignore_caps", ignoreCaps},
                {"UI_left", UILeft},
                {"UI_top", UITop},
                {"UI_show", UIShow},
                {"has_looted_monster_orb", hasLootedMonsterOrb},
                {"UI_trans", UITrans},
                {"explvlcap", explvlcap},
                {"expdmgred", expdmgred}
            };
        }

        //load xp
        public override void Load(TagCompound tag)
        {
            //load exp
            experience = Commons.TryGet<double>(tag, "experience", 0);
            if (experience < 0) experience = 0;
            if (experience > MAX_EXPERIENCE) experience = MAX_EXPERIENCE;

            //load exp rate
            experienceModifier = Commons.TryGet<double>(tag, "experience_modifier", 1);

            //load exp message
            displayExp = Commons.TryGet<bool>(tag, "display_exp", false);

            //load ignore caps
            ignoreCaps = Commons.TryGet<bool>(tag, "ignore_caps", false);

            //UI
            UILeft = Commons.TryGet<float>(tag, "UI_left", 400f);
            UITop = Commons.TryGet<float>(tag, "UI_top", 100f);
            UIShow = Commons.TryGet<bool>(tag, "UI_show", true);
            UITrans = Commons.TryGet<bool>(tag, "UI_trans", false);

            //hasLootedMonsterOrb
            hasLootedMonsterOrb = Commons.TryGet<bool>(tag, "has_looted_monster_orb", false);

            //explvlcap
            explvlcap = Commons.TryGet<int>(tag, "explvlcap", -1);

            //expdmgred
            expdmgred = Commons.TryGet<int>(tag, "expdmgred", -1);
        }

        public override void SetupStartInventory(IList<Item> items)
        {
            Item item = new Item();
            item.SetDefaults(mod.ItemType("ClassToken_Novice"));
            item.stack = 1;
            items.Add(item);

            base.SetupStartInventory(items);
        }

        public override void OnEnterWorld(Player player)
        {
            if (explvlcap == 0) explvlcap = -1; //should fix an odd bug

            if (player.Equals(Main.LocalPlayer))
            {
                if (experience < 0) //occurs when a player who does not have the mod joins a server that uses the mod
                {
                    experience = 0;
                    player.PutItemInInventory(mod.ItemType("ClassToken_Novice"));
                }

                UI.MyUI.visible = true;
                (mod as ExperienceAndClasses).myUI.setTrans(UITrans);
                (mod as ExperienceAndClasses).myUI.setPosition(UILeft, UITop);
                (mod as ExperienceAndClasses).myUI.updateValue(GetExp());

                //settings
                if (Main.netMode == 0)
                {
                    Main.NewText("Require Auth: " + ExperienceAndClasses.requireAuth);
                    Main.NewText("Experience Rate: " + (experienceModifier*100)+"%");
                    Main.NewText("Ignore Class Caps: " + ignoreCaps);
                    if (explvlcap > 0) Main.NewText("Level Cap: " + explvlcap);
                        else Main.NewText("Level Cap: disabled");
                    if (expdmgred > 0) Main.NewText("Reduce Class Damamge: " + expdmgred + "%");
                        else Main.NewText("Reduce Class Damamge: disabled");
                }
            }

            base.OnEnterWorld(player);
        }

        public override void PreUpdate()
        {
            bonusCritPct = 0;
            openerBonusPct = 0;
            openerTime_msec = 0;
            percentMidas = 0;

            base.PreUpdate();
        }
        
        public override void PostUpdateEquips()
        {
            //UI
            if (player.Equals(Main.LocalPlayer))
            {
                if (UIShow)
                {
                    UI.MyUI.visible = true;
                }
                else
                {
                    UI.MyUI.visible = false;
                }
            }

            base.PostUpdateEquips();
        }

        public override void PostUpdate()
        {
            //things to do if this is you
            if (player.Equals(Main.LocalPlayer))
            {
                //update UI if local single-player
                if(Main.netMode==0) (mod as ExperienceAndClasses).myUI.updateValue(GetExp());
            }

            //
            base.PostUpdate();
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            /*~~~~~~~~~~~~~~~~~~~~~~Single Player and Server Only~~~~~~~~~~~~~~~~~~~~~~*/
            if (!pvp && (Main.netMode==0 || Main.netMode==2))
            {
                int level = Methods.Experience.GetLevel(GetExp());

                double maxLoss = Methods.Experience.GetExpReqForLevel(level + 1, false) * 0.1;
                double expSoFar = Methods.Experience.GetExpTowardsNextLevel(GetExp());

                double expLoss = maxLoss;
                if (expSoFar < maxLoss)
                {
                    expLoss = expSoFar;
                }
                expLoss = Math.Floor(expLoss);

                SubtractExp(expLoss); //notifies client if server

                if (Main.netMode==0)
                {
                    (mod as ExperienceAndClasses).myUI.updateValue(GetExp());
                }
            }

            base.Kill(damage, hitDirection, pvp, damageSource);
        }

        public override void ModifyHitNPC(Item item, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            //on-hit midas
            if (Main.rand.Next(100) < (percentMidas * 100)) target.AddBuff(Terraria.ID.BuffID.Midas, 300);

            //Assassin special attack
            DateTime now = DateTime.Now;
            if (openerBonusPct>0 && item.melee && (timeLastAttack.AddMilliseconds(openerTime_msec).CompareTo(now)<=0 || target.life==target.lifeMax))
            {
                //bonus opener damage
                damage = (int)Math.Round((double)damage * (1 + openerBonusPct), 0);

                //crit opener?
                if (bonusCritPct > 0) damage = (int)Math.Round((double)damage * (1 + (bonusCritPct*3)), 0);
            }
            else
            {
                //bonus crit damage (Assassin)
                if (item.melee && bonusCritPct > 0) damage = (int)Math.Round((double)damage * (1 + bonusCritPct), 0);
            }

            //record time
            timeLastAttack = now;

            //remove buff
            int buffInd = player.FindBuffIndex(mod.BuffType("Buff_OpenerAttack"));
            if (buffInd != -1) player.DelBuff(buffInd);

            //base
            base.ModifyHitNPC(item, target, ref damage, ref knockback, ref crit);
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            //on-hit midas
            if (Main.rand.Next(100) < (percentMidas * 100)) target.AddBuff(Terraria.ID.BuffID.Midas, 300);

            //Assassin special attack for yoyo
            DateTime now = DateTime.Now;
            Item item = Main.player[proj.owner].HeldItem;
            if (openerBonusPct > 0 && proj.melee && item.channel && (timeLastAttack.AddMilliseconds(openerTime_msec).CompareTo(now) <= 0 || target.life == target.lifeMax))
            {
                //bonus opener damage (50% YOYO PENALTY)
                damage = (int)Math.Round((double)damage * (1 + (openerBonusPct/2)), 0);

                //crit opener?
                if (bonusCritPct > 0) damage = (int)Math.Round((double)damage * (1 + (bonusCritPct * 3)), 0);
            }

            //record time
            timeLastAttack = now;

            //remove buff
            int buffInd = player.FindBuffIndex(mod.BuffType("Buff_OpenerAttack"));
            if (buffInd != -1) player.DelBuff(buffInd);

            //base
            base.ModifyHitNPCWithProj(proj, target, ref damage, ref knockback, ref crit, ref hitDirection);
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            int buffInd = player.FindBuffIndex(mod.BuffType("Buff_OpenerAttack"));
            if (buffInd != -1)
            {
                if (Main.rand.Next(10) == 0 && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, mod.DustType("Dust_OpenerAttack"), player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 3f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    Main.playerDrawDust.Add(dust);
                }
            }
            base.DrawEffects(drawInfo, ref r, ref g, ref b, ref a, ref fullBright);
        }

    }
}
