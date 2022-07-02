using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using System.Collections.Generic;


namespace ExperienceAndClasses
{
    internal class MyModSystem: ModSystem
    {

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (MouseTextIndex != -1)
            {
                layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
                    "Experience UI",
                    delegate
                    {
                        ExperienceAndClasses.myUserInterface.Update(Main._drawInterfaceGameTime);
                        ExperienceAndClasses.uiExp.Draw(Main.spriteBatch);
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }
    }
}
