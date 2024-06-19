using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using Epsilon.Applications.System;
using Epsilon.Interface.System;
using Epsilon.System.Critical.Processing;
using Epsilon.Interface.System.Shell.Screen;
using System.Drawing;
using Epsilon.Applications.Base;
using Epsilon.System;
using System;
using System.IO;
using Epsilon.Applications.System.Setup;
using Epsilon.System.Resources;

namespace Epsilon.Interface
{
    public static class GUI
    {
        public static int width,
            height,
            mx = 0,
            my = 0;
        public static Canvas canv;
        public static PCScreenFont dFont
            = PCScreenFont.LoadFont(Files.RawPowerlineFont);

        public static Bitmap wp, crs;
        public static Colors colors = new();
        public static Process cProc;

        public static bool clicked = false,
            crsChanged = false;
        static int ox = 0, oy = 0;

        public static void Start()
        {
            canv = FullScreenCanvas.GetFullScreenCanvas();
            width = (int)canv.Mode.Width; height = (int)canv.Mode.Height;

            MouseManager.ScreenWidth = (uint)width;
            MouseManager.ScreenHeight = (uint)height;
            MouseManager.X = (uint)width / 2;
            MouseManager.Y = (uint)height / 2;

            // Processes
            // Login Details Epsilon Encryption System . Config
            if (!File.Exists(ESystem.LoginInfoPath) && !VMTools.IsVirtualBox)
                Manager.Start(new OOBE
                {
                    wData = new WindowData
                    {
                        Position = new Rectangle(0, 0, (int)width, (int)height),
                        Moveable = false
                    },
                    Name = "Finish your Installation",
                    Special = true
                });
            else if (VMTools.IsVirtualBox)
                // TODO: guest mode wallpaper
                ESystem.LogIn(true);
            else
                Manager.Start(new Login
                {
                    wData = new WindowData
                    {
                        Position = new Rectangle(0, 0, (int)width, (int)height),
                        Moveable = false
                    },
                    Name = "Log into Epsilon",
                    Special = true
                });
        }

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
                        && mx <= p.wData.Position.X + p.Name.Length * dFont.Width + 16)
                    {
                        if (my >= p.wData.Position.Y
                            && my <= p.wData.Position.Y + w.tSize)
                        {
                            if (Manager.IsFrontTU() && Manager.toUpdate != p) return;
                            if (Manager.spList.Count <= 1)
                            {
                                cProc = p;
                                ox = mx - p.wData.Position.X;
                                oy = my - p.wData.Position.Y;
                            }
                        }
                    }
                    // DONE
                }
            }

            // DONE
        }

        public static void Update()
        {
            mx = (int)MouseManager.X; my = (int)MouseManager.Y;

            // Back layer
            canv.DrawImage(wp, 0, 0);

            // Debug, draw string with all processes name
            canv.DrawString("Current Processes:", dFont, colors.txtColor, 0, 24);
            for (int i = 0; i < Manager.pList.Count; i++)
            {
                canv.DrawString("[" + Manager.pList[i].Name + "]",
                    dFont,
                    colors.txtColor,
                    0,
                    24 + dFont.Height * (Manager.pList.Count - i)
                );
            }

            canv.DrawString("P:toUpdate", dFont, colors.txtColor, 0, dFont.Height * 12);
            if (Manager.toUpdate != null)
                canv.DrawString("[" + Manager.toUpdate.Name + "]",
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

            canv.DrawString("Build Information:", dFont, colors.txtColor, (int)width - dFont.Width * "Build Information:".Length, (int)height - (32 + dFont.Height * 2));
            canv.DrawString(
                "Epsilon v." + Kernel.version + " | June 2024 build, Milestone 3",
                dFont,
                colors.txtColor,
                (int)width - dFont.Width
                * ("Epsilon v." + Kernel.version + " | June 2024 build, Milestone 3")
                .Length,
                (int)height - (32 + dFont.Height)
            );

            // Middle layer
            Move();
            Manager.Update();

            // Front layer
            canv.DrawImageAlpha(crs, (int)MouseManager.X - 9, (int)MouseManager.Y - 9);
            if (!crsChanged && crs != ESystem.dc) crs = ESystem.dc;

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
