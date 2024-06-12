using Cosmos.System;
using Cosmos.System.Graphics;
using Epsilon.System.Critical.Processing;
using Epsilon.System.Resources;

namespace Epsilon.Interface.System.Shell.Screen
{
    public class ControlMenu : Process
    {
        static int r = 6,
            by;

        static int cbofs = (r / 2) * 2
                - ((r / 2) - (r + 6)),
            mts = 24;

        public override void Run()
        {
            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            GUI.canv.DrawRectangle(
                GUI.colors.moColor,
                x - 1, y - 1,
                w + 1, h + 1
            );

            GUI.canv.DrawFilledRectangle(
                GUI.colors.moColor,
                x, y,
                w, mts
            );
            GUI.canv.DrawFilledRectangle(
                GUI.colors.mColor,
                x, y + mts,
                w, h - mts
            );

            GUI.canv.DrawImage(
                new Bitmap(Files.RawDefaultPFP),
                x,
                y
            );
            GUI.canv.DrawString(
                "Live User",
                GUI.dFont,
                GUI.colors.txtColor,
                x + mts + 4,
                y
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
