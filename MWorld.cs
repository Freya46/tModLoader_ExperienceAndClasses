﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ExperienceAndClasses {
    class MWorld : ModWorld {
        public override void PostUpdate() {
            Systems.NPCRewards.ServerProcessXPBuffer();

            if (Utilities.Netmode.IS_SERVER) {
                //update time if server
                ExperienceAndClasses.UpdateTime();
            }
        }
    }
}
