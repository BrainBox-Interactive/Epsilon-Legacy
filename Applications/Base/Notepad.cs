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
            Window.DrawT(this);

            int x = wData.Position.X, y = wData.Position.Y;
            int w = 450, h = 475;
            Drawing.DrawBottomRoundedRectangle(
                x, y + Window.tSize,
                w, h - Window.tSize,
                4,
                GUI.colors.mColor
            );

            if (Manager.toUpdate == this)
            {
                // add to data with keyboard
            }
        }
    }
}
