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

            // Outline
            Drawing.DrawTopRoundedRectangle(
                p.wData.Position.X - 1,
                p.wData.Position.Y - 1,
                p.wData.Position.Width + 2,
                tSize + 1,
                4,
                GUI.colors.tboColor
            );

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
            //if (GUI.mx >= cbx - r
            //    && GUI.mx <= cbx + r
            //    && GUI.my >= by - r
            //    && GUI.my <= by + r)
            //{

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

        public static void DrawB(Process p)
        {
            // Outline
            Drawing.DrawBottomRoundedRectangle(
                p.wData.Position.X - 1,
                p.wData.Position.Y + tSize,
                p.wData.Position.Width + 2,
                p.wData.Position.Height - tSize + 1,
                4,
                GUI.colors.moColor
            );

            // Window Bottom
            Drawing.DrawBottomRoundedRectangle(
                p.wData.Position.X,
                p.wData.Position.Y + tSize,
                p.wData.Position.Width,
                p.wData.Position.Height - tSize,
                4,
                GUI.colors.mColor
            );
        }
    }
}
