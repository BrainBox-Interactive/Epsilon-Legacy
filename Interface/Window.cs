using Cosmos.System.Graphics.Fonts;
using Epsilon.Interface.System;
using Epsilon.System.Critical.Processing;
using System.Drawing;

namespace Epsilon.Interface
{
    public static class Window
    {
        public static int tSize = 24;

        public static int r = 6,
            ofs = tSize / 2 - (r / 2) / 2 + (r / 4),
            by;

        public static int cbofs = (r / 2) * 2
                - ((r / 2) - (r + 4));

        public static void DrawT(Process p)
        {
            by = p.wData.Position.Y + ofs;

            // Titlebar
            Drawing.DrawTopRoundedRectangle(
                p.wData.Position.X,
                p.wData.Position.Y,
                p.wData.Position.Width,
                tSize,
                4,
                GUI.colors.tbColor
            );

            // Close Button
            int cbx = p.wData.Position.X
                + p.wData.Position.Width
                - cbofs;
            //GUI.canv.DrawFilledCircle(
            //    GUI.colors.qbColor,
            //    cbx,
            //    by,
            //    r
            //);
            if (GUI.mx >= cbx - r
                && GUI.mx <= cbx + r
                && GUI.my >= by - r
                && GUI.my <= by + r)
            {
                GUI.canv.DrawFilledCircle(
                    GUI.colors.qbhColor,
                    cbx,
                    by,
                    r
                );
            } else
                GUI.canv.DrawFilledCircle(
                    GUI.colors.qbColor,
                    cbx,
                    by,
                    r
                );

            // Title
            GUI.canv.DrawString(
                p.Name,
                GUI.dFont,
                GUI.colors.txtColor,
                p.wData.Position.X + 8,
                p.wData.Position.Y + tSize / 2
                - GUI.dFont.Height / 2
            );
        }
    }
}
