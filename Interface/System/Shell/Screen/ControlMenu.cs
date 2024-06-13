using Cosmos.System;
using Cosmos.System.Graphics;
using Epsilon.System.Critical.Processing;
using Epsilon.System.Resources;
using PrismAPI.Graphics;
using System.Drawing;

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
                x - 1, y - 1,
                (ushort)(w + 1), (ushort)(h + 1), 0,
                GUI.colors.tboColor
            );

            GUI.canv.DrawFilledRectangle(
                x, y,
                (ushort)w, (ushort)mts, 0,
                GUI.colors.moColor
            );
            GUI.canv.DrawFilledRectangle(
                x, y + mts,
                (ushort)w, (ushort)(h - mts), 0,
                GUI.colors.mColor
            );

            GUI.canv.DrawImage(
                x,
                y,
                PrismAPI.Graphics.Image.FromBitmap(Files.RawDefaultPFP),
                false
            );

            string user = "Live User";
            GUI.canv.DrawString(
                x + mts + 12,
                y + (mts / 2 - GUI.fsy / 2),
                user + " - ",
                GUI.dFont,
                GUI.colors.txtColor
            );

            // TODO: hyperlink
            GUI.canv.DrawString(
                x + w - 12
                - ((user + " - ").Length * GUI.fsx)
                - ("Settings".Length * GUI.fsx),

                y + (mts / 2 - GUI.fsy / 2),

                "Settings",
                GUI.dFont,
                PrismAPI.Graphics.Color.GoogleBlue
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
