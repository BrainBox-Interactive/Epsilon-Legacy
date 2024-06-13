using Cosmos.System;
using Epsilon.Applications.System;
using Epsilon.Interface.System;
using Epsilon.System.Critical.Processing;
using Epsilon.Interface.System.Shell.Screen;
using System.Drawing;
using PrismAPI.Hardware.GPU;
using Font = PrismAPI.Graphics.Fonts.Font;
using Epsilon.System.Resources;
using PrismAPI.Graphics;

namespace Epsilon.Interface
{
    public static class GUI
    {
        // TODO: IMPORTANT!! MAKE AND USE IsInFront() FUNCTION!!!
        public static int width = 1024,
            height = 768,
            mx = 0,
            my = 0;
        public static Display canv;
        public static Font dFont
            = Font.Fallback;

        public static ushort fsx = dFont.MeasureString(" "),
            fsy = dFont.Size;

        public static Canvas wp
            = Image.FromBitmap(Files.Raw1024x768Wallpaper),
            crs = Image.FromBitmap(Files.RawDefaultCursor);
        public static Colors colors = new();
        public static Process cProc;

        public static bool clicked = false;
        static int ox = 0, oy = 0;

        public static void Start()
        {
            canv = Display.GetDisplay((ushort)width, (ushort)height);

            MouseManager.ScreenWidth = (uint)width;
            MouseManager.ScreenHeight = (uint)height;
            MouseManager.X = (uint)width / 2;
            MouseManager.Y = (uint)height / 2;

            // Processes
            Manager.Start(new MessageBox {
                wData = new WindowData {
                    Position = new Rectangle(100, 100, 200, 50),
                    Moveable = true
                },
                Name = "Message Box Test",
                Content = "Hello World!",
                Special = false,
                button = false
            });

            //Manager.Start(new Notepad {
            //    wData = new WindowData {
            //        Position = new Rectangle(400, 100, 450, 475),
            //        Moveable = true
            //    },
            //    Special = false,
            //    Name = "Notepad"
            //});

            //Manager.Start(new Calculator {
            //    wData = new WindowData {
            //        Position = new Rectangle(400, 100, 256, 200),
            //        Moveable = true
            //    },
            //    Special = false,
            //    Name = "Calculator"
            //});

            // TODO: deprecate special windowdata parameter
            if (Epsilon.System.Global.topBarActivated)
                Manager.Start(new TopBar {
                    wData = new WindowData {
                        Position = new Rectangle(0, 0, width, 24),
                        Moveable = false
                    },
                    Special = false,
                    Name = "Top Bar"
                });

            if (Epsilon.System.Global.controlBarActivated)
                Manager.Start(new ControlBar {
                    wData = new WindowData {
                        Position = new Rectangle(0, height - 32, width, 32),
                        Moveable = false
                    },
                    Special = false,
                    Name = "Control Bar"
                });

            //Manager.Start(new Setup
            //{
            //    wData = new WindowData
            //    {
            //        Position = new Rectangle(0, 0, width, height),
            //        Moveable = false
            //    },
            //    Name = "Epsilon Setup",
            //    Special = true
            //});
        }

        static int ofs = 5;
        static Window w = new();
        public static void Move()
        {
            if (cProc != null && Manager.pList.Contains(cProc)
                && Manager.toUpdate == cProc
                && cProc.wData.Moveable
                && Manager.spList.Count <= 1)
            {
                // TODO: do for all sides
                // Currently hangs the system
                //if ((cProc.wData.Position.X > 0 && mx < cProc.wData.Position.X  )
                //    && cProc.wData.Position.X < width - cProc.wData.Position.Width)
                //if ((cProc.wData.Position.X >= 0 + ofs || mx > cProc.wData.Position.Width / 2)
                //    && (cProc.wData.Position.X <= width - cProc.wData.Position.Width + ofs
                //    || mx > cProc.wData.Position.Width / 2))
                cProc.wData.Position.X = (int)MouseManager.X - ox;
                
                //if (cProc.wData.Position.Y >= 0
                //    && cProc.wData.Position.Y <= height - cProc.wData.Position.Height)
                cProc.wData.Position.Y = (int)MouseManager.Y - oy;
            }
            else if (MouseManager.MouseState == MouseState.Left) //&& !clicked)
            {
                foreach (var p in Manager.pList)
                {
                    if (!p.wData.Moveable) continue;
                    if (mx >= p.wData.Position.X
                        && mx <= p.wData.Position.X + p.wData.Position.Width)
                    {
                        if (my >= p.wData.Position.Y
                            && my <= p.wData.Position.Y + w.tSize)
                        {
                            if (Manager.spList.Count <= 1)
                            {
                                cProc = p;
                                // if (p.wData.Position.X <= 0 || mx > p.wData.Position.Width / 2)
                                //if ((p.wData.Position.X >= 0 - ofs || mx > p.wData.Position.Width / 2)
                                //    && (p.wData.Position.X <= width - p.wData.Position.Width + ofs
                                //    || mx > p.wData.Position.Width / 2))
                                ox = mx - p.wData.Position.X;
                                oy = my - p.wData.Position.Y;
                            }
                        }
                    }
                }
            }

            // DONE
        }

        public static void Update()
        {
            mx = (int)MouseManager.X; my = (int)MouseManager.Y;
            canv.Clear(PrismAPI.Graphics.Color.LightGray);

            // Back layer
            canv.DrawImage(0, 0, wp, false);

            // Debug, draw string with all processes name
            //canv.DrawString(0, 24, "Current Processes:", dFont, colors.txtColor);
            //for (int i = 0; i < Manager.pList.Count; i++)
            //{
            //    canv.DrawString(
            //        0,
            //        24 + fsy * (Manager.pList.Count - i),
            //        "[" + Manager.pList[i].Name + "]",
            //        dFont,
            //        colors.txtColor
            //    );
            //}

            //canv.DrawString(0, fsy * 12, "P:curProc", dFont, colors.txtColor);
            //if (cProc != null)
            //    canv.DrawString(
            //        0,
            //        fsy * 13,
            //        "[" + cProc.Name + "]",
            //        dFont,
            //        colors.txtColor
            //    );
            //else
            //    canv.DrawString(
            //        0,
            //        fsy * 13,
            //        "[NO PROCESS]",
            //        dFont,
            //        colors.txtColor
            //    );

            //canv.DrawString(0, fsy * 15, "LhC:" + Kernel.lastHCol, dFont, colors.txtColor);
            canv.DrawString(0, fsy * 16, "FPS:" + Kernel._fps, dFont, colors.txtColor);
            //canv.DrawString(0, fsy * 18, "c:" + clicked, dFont, colors.txtColor);

            canv.DrawString(width - dFont.MeasureString("Build Information:"), height - (32 + fsy * 2), "Build Information:", dFont, colors.txtColor);
            canv.DrawString(
                width - dFont.MeasureString("Epsilon v." + Kernel.version + " | June 2024 build, Milestone 2"),
                height - (32 + fsy),
                "Epsilon v." + Kernel.version + " | June 2024 build, Milestone 2",
                dFont,
                colors.txtColor
            );

            // Middle layer
            Move();
            Manager.Update();

            // Front layer
            canv.DrawImage((int)MouseManager.X, (int)MouseManager.Y, crs, true);

            // Conditions
            if (MouseManager.MouseState == MouseState.Left) clicked = true;
            else if (MouseManager.MouseState == MouseState.Middle && !clicked)
            {
                Manager.Start(new MessageBox
                {
                    wData = new WindowData
                    {
                        Position = new Rectangle(mx - 250 / 2, my - 75 / 4, 250, 75),
                        Moveable = true
                    },
                    Name = "mbPNb::" + Manager.pList.Count,
                    Content = "process" + Manager.pList.Count + " || Cpt=" + Manager.toUpdate,
                    Special = false,
                    button = true
                });
                clicked = true;
            }
            else if (MouseManager.MouseState == MouseState.None && clicked)
            {
                clicked = false;
                cProc = null;
            }

            // Obligatory update to display
            canv.Update();
        }
    }
}
