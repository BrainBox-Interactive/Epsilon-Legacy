using Cosmos.System;
using Cosmos.System.Graphics;
using Epsilon.Applications.System;
using Epsilon.Interface.Components;
using Epsilon.System;
using Epsilon.System.Critical.Processing;
using Epsilon.System.Resources;
using System;
using System.Drawing;
using System.Linq;
using Console = System.Console;

namespace Epsilon.Interface.System.Shell.Screen
{
    public class ControlButton : Component
    {
        Bitmap curImage;
        Bitmap idleCB = new(Files.RawIdleCButton),
            hoverCB = new(Files.RawHoverCButton),
            clickCB = new(Files.RawClickCButton);
        ControlMenu cMenu;
        Process Process { get; set; }
        bool clicked = false;

        public ControlButton(int x, int y, Process p) : base(x, y,
            (int)new Bitmap(Files.RawIdleCButton).Width,
            (int)new Bitmap(Files.RawIdleCButton).Height)
        {
            Process = p;
            X = x; Y = y;
        }

        public override void Update()
        {
            base.Update();
            if (CheckHover())
            {
                GUI.crsChanged = true;
                GUI.crs = ESystem.hc;
                if (curImage != hoverCB) curImage = hoverCB;

                if (MouseManager.MouseState == MouseState.Left)
                {
                    if (curImage != clickCB) curImage = clickCB;
                    if (!clicked && !GUI.clicked
                        && !Manager.IsRunning("Control Menu"))
                    {
                        Manager.Start(cMenu = new ControlMenu
                        {
                            wData = new WindowData
                            {
                                Moveable = false,
                                Position = new Rectangle(
                                    0,
                                    GUI.height - (32 + 450),
                                    350,
                                    450
                                )
                            },
                            Name = "Control Menu",
                            Special = false
                        });
                        clicked = true;
                    }
                }
                else
                {
                    if (curImage != idleCB) curImage = idleCB;
                    if (GUI.crsChanged) GUI.crsChanged = false;
                }
            }
            else
            {
                if (curImage != idleCB) curImage = idleCB;
                if (GUI.crsChanged) GUI.crsChanged = false;
            }

            if (MouseManager.MouseState == MouseState.None)
                clicked = false;

            // Menu Button
            GUI.canv.DrawImageAlpha(curImage, X, Y);
        }

        public override bool CheckHover()
        {
            if (GUI.mx >= X + 11
                && GUI.mx <= X + Width - 11
                && GUI.my >= Y + 11
                && GUI.my <= Y + Height - 11)
                return true;
            return false;
        }
    }

    public class ControlBar : Process
    {
        int x, y, w, h;
        ControlButton c;
        Bitmap cBar = new(Files.RawCBar);

        public override void Start()
        {
            base.Start();
            c = new ControlButton(x, y, this);
            cBar.Resize((uint)GUI.width, 32);
        }

        public override void Run()
        {
            x = wData.Position.X; y = wData.Position.Y;
            w = wData.Position.Width; h = wData.Position.Height;
            GUI.canv.DrawLine(GUI.colors.mooColor,
                x, y - 1, x + w, y - 1);
            GUI.canv.DrawFilledRectangle(
                GUI.colors.bColor, x, y, w, h);
            //GUI.canv.DrawImageAlpha(cBar, x, y);
            //GUI.canv.DrawImage(cBar, x, y);
            c.X = x + 3; c.Y = y - 17;
            c.Update();
        }
    }
}
