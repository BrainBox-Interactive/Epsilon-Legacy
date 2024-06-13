using Cosmos.System;
using Epsilon.Applications.System;
using Epsilon.System.Critical.Processing;
using System;
using PrismAPI.Graphics;
using System.Linq;
using Console = System.Console;
using System.Drawing;
using Color = PrismAPI.Graphics.Color;

namespace Epsilon.Interface.System.Shell.Screen
{
    public class ControlBar : Process
    {
        public override void Run()
        {
            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            GUI.canv.DrawFilledRectangle(
                x,
                y - 1,
                (ushort)w,
                1,
                0,
                GUI.colors.boColor
            );
            GUI.canv.DrawFilledRectangle(
                x,
                y,
                (ushort)w,
                (ushort)h,
                0,
                GUI.colors.bColor
            );

            // Menu Button
            GUI.canv.DrawFilledCircle(
                wData.Position.X + 35,
                wData.Position.Y + 15,
                21,
                Color.LightGray
            );
            GUI.canv.DrawFilledCircle(
                wData.Position.X + 35,
                wData.Position.Y + 15,
                20,
                Color.White
            );

            if (GUI.mx >= wData.Position.X + (35 - 20)
                && GUI.mx <= wData.Position.X + (35 + 20))
            {
                if (GUI.my >= wData.Position.Y + (15 - 20)
                    && GUI.my <= wData.Position.Y + (15 + 20))
                {
                    // TODO: If click then open and
                    // different hover, else hover
                    GUI.canv.DrawFilledCircle(
                        wData.Position.X + 35,
                        wData.Position.Y + 15,
                        20,
                        Color.LightGray
                    );

                    if (MouseManager.MouseState == MouseState.Left
                        && !Manager.IsRunning("Control Menu"))
                        Manager.Start(new ControlMenu {
                            wData = new WindowData {
                                Moveable = false,
                                Position = new Rectangle (
                                    0,
                                    GUI.height - (32 + 460),
                                    350,
                                    450
                                )
                            },
                            Name = "Control Menu",
                            Special = true
                        });
                }
            }

            // CLASS DONE
            // Control Bar class, 2024
        }
    }
}
