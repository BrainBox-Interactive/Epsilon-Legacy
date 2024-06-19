using Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;
using Epsilon.Applications.System;
using Epsilon.Interface.System;
using Epsilon.System.Critical.Processing;
using System.Drawing;
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
            canv = FullScreenCanvas.GetFullScreenCanvas(new Mode(640, 480, ColorDepth.ColorDepth32));
            width = (int)canv.Mode.Width; height = (int)canv.Mode.Height;

            MouseManager.ScreenWidth = (uint)width;
            MouseManager.ScreenHeight = (uint)height;
            MouseManager.X = (uint)width / 2;
            MouseManager.Y = (uint)height / 2;

            // Processes
            Manager.Start(new PSetup
            {
                wData =
                {
                    Moveable = false,
                    Position = new(0, 0, 500, 500)
                },
                Special = true,
                Name = "Epsilon Setup"
            });
        }

        public static void Update()
        {
            mx = (int)MouseManager.X; my = (int)MouseManager.Y;

            // Middle layer
            Manager.Update();

            // Front layer
            canv.DrawImageAlpha(crs, (int)MouseManager.X - 9, (int)MouseManager.Y - 9);
            if (!crsChanged && crs != ESystem.dc) crs = ESystem.dc;

            // Conditions
            if (MouseManager.MouseState == MouseState.Left) clicked = true;
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
