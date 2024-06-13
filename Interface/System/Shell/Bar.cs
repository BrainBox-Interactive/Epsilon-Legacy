using Epsilon.Interface;
using Epsilon.Interface.System;
using Epsilon.System.Critical.Processing;

namespace Epsilon.Interface.System.Shell
{
    public class Bar : Process
    {
        public override void Run()
        {
            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            GUI.canv.DrawFilledRectangle(
                x, y,
                (ushort)w, (ushort)h,
                0,
                GUI.colors.bColor
            );
        }
    }
}
