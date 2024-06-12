using Cosmos.System;
using Epsilon.Applications.System;
using Epsilon.System.Critical.Processing;
using System;
using System.Drawing;
using System.Linq;
using Console = System.Console;

namespace Epsilon.Interface.System.Shell.Screen
{
    public class ControlBar : Process
    {
        public override void Run()
        {
            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            GUI.canv.DrawFilledRectangle(
                GUI.colors.boColor,
                x,
                y - 1,
                w,
                1
            );
            GUI.canv.DrawFilledRectangle(
                GUI.colors.bColor,
                x,
                y,
                w,
                h
            );

            // Menu Button
            GUI.canv.DrawFilledCircle(
                Color.LightGray,
                wData.Position.X + 35,
                wData.Position.Y + 15,
                21
            );
            GUI.canv.DrawFilledCircle(
                Color.White,
                wData.Position.X + 35,
                wData.Position.Y + 15,
                20
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
                        Color.LightGray,
                        wData.Position.X + 35,
                        wData.Position.Y + 15,
                        20
                    );

                    if (MouseManager.MouseState == MouseState.Left
                        && !Manager.IsRunning("Control Menu"))
                        Manager.Start(new ControlMenu {
                            wData = new WindowData {
                                Moveable = false,
                                Position = new Rectangle(
                                    0,
                                    GUI.height - (32 + 665),
                                    450,
                                    650
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
