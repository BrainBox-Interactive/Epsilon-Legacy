using Cosmos.System;
using Cosmos.System.Graphics;
using Epsilon.Applications.System;
using Epsilon.System;
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
        Bitmap curImage;
        Bitmap idleCB = new(Files.RawIdleCButton),
            hoverCB = new(Files.RawHoverCButton),
            clickCB = new(Files.RawClickCButton);
        bool clicked = false;

        int x, y, w, h;
        public override void Run()
        {
            x = wData.Position.X; y = wData.Position.Y;
            w = wData.Position.Width; h = wData.Position.Height;
            GUI.canv.DrawLine(GUI.colors.mooColor,
                x, y - 1, x + w, y - 1);
            GUI.canv.DrawFilledRectangle(
                GUI.colors.bColor, x, y, w, h);

            if (GUI.mx >= wData.Position.X - 3 + 11
                && GUI.mx <= wData.Position.X - 3 + (11 + 41))
            {
                if (GUI.my >= wData.Position.Y - 17 + 11
                    && GUI.my <= wData.Position.Y - 17 + (11 + 41))
                {
                    GUI.crsChanged = true;
                    GUI.crs = ESystem.hc;
                    if (curImage != hoverCB) curImage = hoverCB;

                    if (MouseManager.MouseState == MouseState.Left)
                    {
                        if (curImage != clickCB) curImage = clickCB;
                        if (!clicked && !GUI.clicked)
                        {
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
                            clicked = true;
                        }
                    }
                } else {
                    if (curImage != idleCB) curImage = idleCB;
                    if (GUI.crsChanged) GUI.crsChanged = false;
                }
            } else {
                if (curImage != idleCB) curImage = idleCB;
                if (GUI.crsChanged) GUI.crsChanged = false;
            }

            if (MouseManager.MouseState == MouseState.None)
                clicked = false;

            // Menu Button
            GUI.canv.DrawImageAlpha(
                curImage, wData.Position.X - 3,
                wData.Position.Y - 17
            );

            // CLASS DONE
            // Control Bar class, 2024
        }
    }
}
