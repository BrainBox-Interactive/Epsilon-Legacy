using Epsilon.Interface.Components;
using Epsilon.Interface;
using Epsilon.System.Critical.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.Applications.System
{
    public class Setup : Process
    {
        Window w;

        public override void Start()
        {
            base.Start();
            this.w = new();

            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            int fh = this.w.tSize + h;

            this.w.StartAPI(this);
        }

        public override void Run()
        {
            this.w.DrawT(this); this.w.DrawB(this);

            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            int fh = this.w.tSize + h;
        }
    }
}
