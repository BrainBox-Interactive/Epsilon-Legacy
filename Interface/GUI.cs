using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using Epsilon.Applications.System;
using Epsilon.Interface.System;
using Epsilon.System.Critical.Processing;
using Epsilon.Interface.System.Shell.Screen;
using System.Drawing;
using Epsilon.Applications.Base;

namespace Epsilon.Interface
{
    public static class GUI
    {
        public static int width = 1920,
            height = 1080,
            mx = 0,
            my = 0;
        public static SVGAIICanvas canv;
        public static PCScreenFont dFont
            = PCScreenFont.Default;

        public static Bitmap wp, crs;
        public static Colors colors = new();
        public static Process cProc;

        public static bool clicked = false;
        static int ox = 0, oy = 0;

        public static void Start()
        {
            canv = new SVGAIICanvas(new Mode(
                (uint)width,
                (uint)height,
                ColorDepth.ColorDepth32
            ));

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
                Content = "Hello World",
                Special = false,
                Button = false
            });

            Manager.Start(new MessageBox
            {
                wData = new WindowData {
                    Position = new Rectangle(400, 800, 250, 75),
                    Moveable = true
                },
                Name = "Message Box Test 2",
                Special = false,
                Button = true
            });

            Manager.Start(new Notepad {
                wData = new WindowData {
                    Position = new Rectangle(800, 100, 450, 475),
                    Moveable = true
                },
                Special = false,
                Name = "Notepad"
            });

            Manager.Start(new ControlBar {
                wData = new WindowData {
                    Position = new Rectangle(0, height - 32, width, 32),
                    Moveable = false
                },
                Special = true,
                Name = "Control Bar"
            });
        }

        static int ofs = 5;
        public static void Move()
        {
            if (cProc != null && Manager.pList.Contains(cProc)
                && Manager.toUpdate == cProc
                && cProc.wData.Moveable)
            {
                cProc.wData.Position.X = (int)MouseManager.X - ox;
                cProc.wData.Position.Y = (int)MouseManager.Y - oy;
            }
            else if (MouseManager.MouseState == MouseState.Left && !clicked)
            {
                foreach (var p in Manager.pList)
                {
                    if (!p.wData.Moveable) continue;
                    if (mx >= p.wData.Position.X - ofs
                        && mx <= p.wData.Position.X + p.wData.Position.Width - ofs)
                    {
                        if (my >= p.wData.Position.Y - ofs
                            && my <= p.wData.Position.Y + Window.tSize)
                        {
                            cProc = p;
                            ox = mx - p.wData.Position.X;
                            oy = my - p.wData.Position.Y;
                        }
                    }

                    if (p.Special) continue;
                    if (mx >= p.wData.Position.X + p.wData.Position.Width - Window.cbofs - ofs
                        && mx <= p.wData.Position.X + p.wData.Position.Width + Window.r * 2 - ofs)
                    {
                        if (my >= p.wData.Position.Y + Window.ofs - ofs
                            && my <= p.wData.Position.Y + Window.ofs + Window.r * 2)
                            Manager.pList.Remove(p);
                    }
                }
            }
        }

        public static void Update()
        {
            mx = (int)MouseManager.X; my = (int)MouseManager.Y;

            // Back layer
            canv.DrawImage(wp, 0, 0);

            // Middle layer
            Move();
            Manager.Update();

            // Debug, draw string with all processes name
            canv.DrawString("Current Processes:", dFont, colors.txtColor, 0, 0);
            for (int i = 0; i < Manager.pList.Count; i++)
            {
                canv.DrawString("[" + Manager.pList[i].Name + "]",
                    dFont,
                    colors.txtColor,
                    0,
                    dFont.Height * (Manager.pList.Count - i)
                );
            }

            canv.DrawString("P:curProc", dFont, colors.txtColor, 0, dFont.Height * 12);
            if (cProc != null)
                canv.DrawString("[" + cProc.Name + "]",
                    dFont,
                    colors.txtColor,
                    0,
                    dFont.Height * 13
                );
            else
                canv.DrawString("[NO PROCESS]",
                    dFont,
                    colors.txtColor,
                    0,
                    dFont.Height * 13
                );

            canv.DrawString("LhC:" + Kernel.lastHCol, dFont, colors.txtColor, 0, dFont.Height * 15);
            canv.DrawString("FPS:" + Kernel._fps, dFont, colors.txtColor, 0, dFont.Height * 16);
            canv.DrawString("c:" + clicked, dFont, colors.txtColor, 0, dFont.Height * 18);

            canv.DrawString("Build Information:", dFont, colors.txtColor, width - dFont.Width * "Build Information:".Length, height - (32 + dFont.Height * 2));
            canv.DrawString(
                "Epsilon v." + Kernel.version + " | June 2024 build, Milestone 1",
                dFont,
                colors.txtColor,
                width - dFont.Width
                * ("Epsilon v." + Kernel.version + " | June 2024 build, Milestone 1")
                .Length,
                height - (32 + dFont.Height)
            );

            // Front layer
            canv.DrawImageAlpha(crs, (int)MouseManager.X, (int)MouseManager.Y);

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
                    Button = true
                });
                clicked = true;
            }
            else if (MouseManager.MouseState == MouseState.None && clicked)
            {
                clicked = false;
                cProc = null;
            }

            // Obligatory update to display
            canv.Display();
        }
    }
}
