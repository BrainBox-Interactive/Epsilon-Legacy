using Cosmos.System;
using Epsilon.Interface;
using Epsilon.Interface.Components.Text;
using Epsilon.System.Critical.Processing;
using System.Drawing;

namespace Epsilon.Applications.Base
{
    public class Notepad : Process
    {
        public string Content = "";
        Window w;
        Scrollbox sb;

        public override void Start()
        {
            base.Start();
            this.w = new();

            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            int fh = this.w.tSize + h;

            sb = new(x, y, w, h-1,
                "", this, true);

            this.w.StartAPI(this);
        }

        public override void Run()
        {
            w.DrawB(this);

            sb.X = wData.Position.X;
            sb.Y = wData.Position.Y + w.tSize+1;
            sb.Update();

            w.DrawT(this);
        }
    }
}
