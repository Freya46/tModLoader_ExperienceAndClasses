﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;

namespace ExperienceAndClasses.Systems {
    public abstract class PowerScaling {

        public enum IDs : byte {
            Melee,
            RangedThrowing,
            Magic,
            Minion,
            Light,
            Harmonic,
            Other,

            //insert here

            NUMBER_OF_IDs, //leave this last
        }

        public const byte ID_NUM_DEFAULT = (byte)IDs.Melee;

        public static readonly PowerScaling[] LOOKUP;
        public static readonly int LOOKUP_LENGTH;
        static PowerScaling() {
            LOOKUP = new PowerScaling[(byte)PowerScaling.IDs.NUMBER_OF_IDs];
            for (byte i = 0; i < LOOKUP.Length; i++) {
                LOOKUP[i] = Utilities.Commons.CreateObjectFromName<PowerScaling>(Enum.GetName(typeof(IDs), i));
            }
            LOOKUP_LENGTH = LOOKUP.Length;
        }

        public readonly IDs ID;
        public readonly byte ID_num;
        public readonly string[] Types;
        public float Scale { get; protected set; } = 1f;
        public bool Enabled { get; protected set; } = true;

        public string Name {
            get {
                string name = "";
                foreach (string type in Types) {
                    if (name.Length > 0)
                        name += "/";

                    name += Language.GetTextValue("Mods.ExperienceAndClasses.Common.Damage_Type_" + type);
                }

                return name;
            }
        }

        public PowerScaling(IDs id, string[] types) {
            ID = id;
            ID_num = (byte)id;
            Types = types;
        }

        public string ApplyPoints(EACPlayer eacplayer, float increase, float per_point, bool do_effects = true) {
            string str = "";
            if (Enabled) {
                increase *= Scale;
                per_point *= Scale;

                if (eacplayer.Fields.Is_Local) {
                    foreach (string type in Types) {
                        str += Attribute.BonusValueString(increase, "Stat_Damage_" + type, true, per_point);
                    }
                }

                if (do_effects) {
                    Apply(eacplayer, increase);
                }
            }
            return str;
        }

        protected abstract void Apply(EACPlayer eacplayer, float damage_increase);

        public PowerScaling GetNext() {
            int ind;

            ind = ID_num + 1;
            if (ind >= LOOKUP_LENGTH) {
                //loop around to start
                ind = 0;
            }

            if (LOOKUP[ind].Enabled)
                return LOOKUP[ind];
            else
                return LOOKUP[ind].GetNext();
        }

        public PowerScaling GetPrior() {
            int ind;


            ind = ID_num - 1;
            if (ind < 0) {
                //loop around to end
                ind = LOOKUP_LENGTH - 1; ;
            }

            if (LOOKUP[ind].Enabled)
                return LOOKUP[ind];
            else
                return LOOKUP[ind].GetPrior();
        }

        public class Melee : PowerScaling {
            public Melee() : base (IDs.Melee, new string[] { "Melee" }) {}
            protected override void Apply(EACPlayer eacplayer, float damage_increase) {
                eacplayer.player.meleeDamage += damage_increase;
            }
        }

        public class RangedThrowing : PowerScaling {
            public RangedThrowing() : base(IDs.RangedThrowing, new string[] { "Ranged" , "Throwing" }) { }
            protected override void Apply(EACPlayer eacplayer, float damage_increase) {
                eacplayer.player.rangedDamage += damage_increase;
                eacplayer.player.thrownDamage += damage_increase;
            }
        }

        public class Magic : PowerScaling {
            public Magic() : base(IDs.Magic, new string[] { "Magic" }) { }
            protected override void Apply(EACPlayer eacplayer, float damage_increase) {
                eacplayer.player.magicDamage += damage_increase;
            }
        }

        public class Minion : PowerScaling {
            public Minion() : base(IDs.Minion, new string[] { "Minion" }) { }
            protected override void Apply(EACPlayer eacplayer, float damage_increase) {
                eacplayer.player.minionDamage += damage_increase;
            }
        }

        public class Light : PowerScaling {
            public Light() : base(IDs.Light, new string[] { "Light" }) { }
            protected override void Apply(EACPlayer eacplayer, float damage_increase) {
                eacplayer.PSheet.Stats.Damage_Light += damage_increase;
            }
        }

        public class Harmonic : PowerScaling {
            public Harmonic() : base(IDs.Harmonic, new string[] { "Harmonic" }) { }
            protected override void Apply(EACPlayer eacplayer, float damage_increase) {
                eacplayer.PSheet.Stats.Damage_Harmonic += damage_increase;
            }
        }

        public class Other : PowerScaling {
            public Other() : base(IDs.Other, new string[] { "Other" }) { }
            protected override void Apply(EACPlayer eacplayer, float damage_increase) {
                eacplayer.PSheet.Stats.Damage_Other_Add += damage_increase;
            }
        }
    }
}