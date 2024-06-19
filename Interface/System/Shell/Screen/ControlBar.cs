using Cosmos.System;
using Cosmos.System.Graphics;
using Epsilon.Applications.System;
using Epsilon.System.Critical.Processing;
using Epsilon.System.Resources;
using System;
using System.Drawing;
using System.Linq;
using Console = System.Console;

namespace Epsilon.Interface.System.Shell.Screen
{
    public class ControlBar : Process
    {
        ControlMenu cMenu;
        Bitmap idleCB = new(Files.RawIdleCButton);
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
            //GUI.canv.DrawFilledCircle(
            //    Color.LightGray,
            //    wData.Position.X + 35,
            //    wData.Position.Y + 15,
            //    21
            //);
            //GUI.canv.DrawFilledCircle(
            //    Color.White,
            //    wData.Position.X + 35,
            //    wData.Position.Y + 15,
            //    20
            //);
            GUI.canv.DrawImageAlpha(
                idleCB, wData.Position.X + 15,
                wData.Position.Y - 5
            );

            if (GUI.mx >= wData.Position.X + (35 - 20)
                && GUI.mx <= wData.Position.X + (35 + 20))
            {
                if (GUI.my >= wData.Position.Y + (15 - 20)
                    && GUI.my <= wData.Position.Y + (15 + 20))
                {
                    // TODO: If click then open and
                    // different hover, else hover
                    //GUI.canv.DrawFilledCircle(
                    //    Color.LightGray,
                    //    wData.Position.X + 35,
                    //    wData.Position.Y + 15,
                    //    20
                    //);

                    if (MouseManager.MouseState == MouseState.Left
                        && !GUI.clicked)
                        if (!Manager.IsRunning("Control Menu")) {
                            Manager.Start(cMenu = new ControlMenu
                            {
                                wData = new WindowData
                                {
                                    Moveable = false,
                                    Position = new Rectangle(
                                        0,
                                        GUI.height - (32 + 460),
                                        350,
                                        450
                                    )
                                },
                                Name = "Control Menu",
                                Special = true
                            });
                            GUI.clicked = true;
                        }
                        else if (Manager.IsRunning("Control Menu"))
                        {
                            cMenu.Remove();
                            cMenu = null;
                            GUI.clicked = true;
                        }
                }
            }

            // CLASS DONE
            // Control Bar class, 2024
        }
    }
}
