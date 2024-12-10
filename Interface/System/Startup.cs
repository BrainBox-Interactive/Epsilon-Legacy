using Cosmos.System.Graphics;
using Epsilon.System;
using Epsilon.System.Critical.Processing;
using Epsilon.System.Resources;
using GrapeGL.Graphics;

namespace Epsilon.Interface.System
{
    public class Startup : Process
    {
        GrapeGL.Graphics.Canvas b = GrapeGL.Graphics.Image.FromBitmap(Files.RawTEPBanner);
        Window win;

        public override void Start()
        {
            base.Start();
            
            win = new();
            win.StartAPI(this, false);

            wData.Position.Width = (int)b.Width;
            wData.Position.X = GUI.width / 2 - wData.Position.Width / 2;
            wData.Position.Height = wData.Position.Y + win.tSize + 1 + 24 * 2 + (int)b.Height + 24 + 4 * 2;
            wData.Position.Y = GUI.height / 2 - wData.Position.Height / 2;

            // ESystem.BootUp();
        }

        public override void Run()
        {
            base.Run();
            win.DrawB(this); win.DrawT(this, false);
            GUI.canv.DrawImage(b, wData.Position.X, wData.Position.Y + win.tSize + 1);
        }
    }
}
