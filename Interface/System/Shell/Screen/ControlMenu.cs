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

            //Drawing.DrawButton(x + 16, y + 16, w - 64, 32, "Test");

            //// Close Button
            //int cbx = wData.Position.X
            //    + wData.Position.Width
            //    - cbofs;
            //by = wData.Position.Y + 16;
            //if (GUI.mx >= cbx - r
            //    && GUI.mx <= cbx + r
            //    && GUI.my >= by - r
            //    && GUI.my <= by + r)
            //{
            //    GUI.canv.DrawFilledCircle(
            //        GUI.colors.qbhColor,
            //        cbx,
            //        by,
            //        r
            //    );
            //    if (MouseManager.MouseState == MouseState.Left)
            //        Manager.pList.Remove(this);
            //}
            //else
            //    GUI.canv.DrawFilledCircle(
            //        GUI.colors.qbColor,
            //        cbx,
            //        by,
            //        r
            //    );
        }
    }
}
