using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using Epsilon.Applications.System;
using Epsilon.Interface.System;
using Epsilon.System.Critical.Processing;
using System.Drawing;
using Epsilon.System;
using System.IO;
using Epsilon.Applications.System.Setup;
using Epsilon.System.Resources;
using Cosmos.System.Coroutines;
using Epsilon.System.Critical;
using Epsilon.Properties;
using GrapeGL.Graphics;
using Canvas = GrapeGL.Graphics.Canvas;
using Image = GrapeGL.Graphics.Image;
using GrapeGL.Graphics.Fonts;
using GrapeGL.Hardware.GPU;

namespace Epsilon.Interface
{
    public static class GUI
    {
        public static int width,
            height,
            mx = 0,
            my = 0;
        public static Display canv;
        public static AcfFontFace dFont
            = new(new MemoryStream(Files.RawDefaultFont));

        public static Canvas wp, crs;
        public static Colors colors = new();
        public static Process cProc;

        public static bool clicked = false,
            crsChanged = false;
        static int ox = 0, oy = 0;

        public static void Start()
        {
            canv.Clear();
            ushort width = canv.Width, height = canv.Height;

            MouseManager.ScreenWidth = width;
            MouseManager.ScreenHeight = height;
            MouseManager.X = (uint)width / 2;
            MouseManager.Y = (uint)height / 2;

            // Processes
            // Login Details Epsilon Encryption System . Config
            if (!File.Exists(ESystem.LoginInfoPath))
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
                if (crs != ESystem.mc) crs = ESystem.mc;
                crsChanged = true;

                // TODO: do for all sides
                cProc.wData.Position.X = (int)MouseManager.X - ox;
                cProc.wData.Position.Y = (int)MouseManager.Y - oy;
            }
            else if (MouseManager.MouseState == MouseState.Left) //&& !clicked)
            {
                foreach (var p in Manager.pList)
                {
                    if (!p.wData.Moveable) continue;
                    if (mx >= p.wData.Position.X
                        && mx <= p.wData.Position.X + dFont.MeasureString(p.Name) + 16)
                    {
                        if (my >= p.wData.Position.Y
                            && my <= p.wData.Position.Y + w.tSize)
                        {
                            if (Manager.IsFrontTU(p) && Manager.toUpdate != p) return;
                            if (Manager.spList.Count <= 1)
                            {
                                cProc = p;
                                Manager.toUpdate = p;
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

        public static bool DrawCursor = true;
        public static void Update()
        {
            canv.Clear(GrapeGL.Graphics.Color.Black);
            mx = (int)MouseManager.X; my = (int)MouseManager.Y;

            // Back layer
            canv.DrawImage(0, 0, wp, false);
            canv.DrawString(0, 32, "FPS:" + Kernel._fps, dFont, colors.txtColor);
            canv.DrawString(width - dFont.MeasureString("Build Information:"),
                height - (32 + dFont.GetHeight() * 2),
                "Build Information:", dFont, colors.txtColor);
            canv.DrawString(
                width - dFont.MeasureString("Epsilon v." + Kernel.version + VersionInfo.revision),
                height - (32 + dFont.GetHeight()),
                "Epsilon v." + Kernel.version + VersionInfo.revision,
                dFont,
                colors.txtColor
            );

            // Middle layer
            Move();
            Manager.Update();

            // Front layer
            if (DrawCursor)
            {
                canv.DrawImage((int)MouseManager.X - 9, (int)MouseManager.Y - 9, crs, true);
                if (!crsChanged && crs != ESystem.dc) crs = ESystem.dc;
            }

            // Conditions
            if ((MouseManager.MouseState == MouseState.Left ||
                MouseManager.MouseState == MouseState.Middle ||
                MouseManager.MouseState == MouseState.Right)
                && !clicked) clicked = true;
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
