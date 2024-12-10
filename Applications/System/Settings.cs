using Epsilon.Interface;
using Epsilon.Interface.Components.Text;
using Epsilon.Interface.System;
using Epsilon.System.Critical.Processing;
using GrapeGL.Graphics;

namespace Epsilon.Applications.System
{
    public class Settings : Process
    {
        Window w;
        Box user, pass;

        public override void Start()
        {
            this.w = new();
            this.w.StartAPI(this);

            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            user = new(x + "Username: ".Length * GUI.dFont.Width, y + this.w.tSize,
                GUI.dFont.Width * 24, GUI.dFont.Height, Color.White, Color.Black, "Username", this);
        }

        public override void Run()
        {
            this.w.DrawB(this); this.w.DrawT(this);

            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            GUI.canv.DrawString("Username: ", GUI.dFont, Color.White, x, y + this.w.tSize);
            user.Update();

            // TODO: List of settings (bg, font)
        }
    }
}
