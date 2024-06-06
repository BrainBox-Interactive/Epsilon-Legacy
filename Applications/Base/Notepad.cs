using Cosmos.System;
using Epsilon.Interface;
using Epsilon.Interface.System;
using Epsilon.System.Critical.Processing;

namespace Epsilon.Applications.Base
{
    public class Notepad : Process
    {
        string data = null;
        public override void Run()
        {
            wData.Position.Width = 450;
            wData.Position.Height = 475;
            Window.DrawT(this); Window.DrawB(this);

            int x = wData.Position.X, y = wData.Position.Y,
                w = wData.Position.Width, h = wData.Position.Height;
            if (Manager.toUpdate == this)
            {
                // add to data with keyboard
            }
        }
    }
}
