using Cosmos.System;
using Epsilon.System.Critical.Processing;

namespace Epsilon.Interface.System.Shell.Screen
{
    public class ControlMenu : Process
    {
        static int r = 6,
            by;

        static int cbofs = (r / 2) * 2
                - ((r / 2) - (r + 6));

        public override void Run()
        {
            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            Drawing.DrawMenuRoundedRectangle(
                x - 1, y - 1,
                w + 2, h + 2,
                16,
                GUI.colors.moColor
            );

            Drawing.DrawMenuRoundedRectangle(
                x, y,
                w, h,
                16,
                GUI.colors.mColor
            );

            // if clicks off the menu
            if (GUI.mx < x
                || GUI.mx > x + w
                || GUI.my < y
                || GUI.my > y + h)
                if (MouseManager.MouseState == MouseState.Left
                    && !GUI.clicked)
                    Manager.pList.Remove(this);
        }
    }
}
