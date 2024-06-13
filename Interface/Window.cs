using Cosmos.System.Graphics.Fonts;
using Epsilon.Interface.Components.Titlebar.Base;
using Epsilon.Interface.System;
using Epsilon.System.Critical.Processing;
using System.Drawing;

namespace Epsilon.Interface
{
    public class Window
    {
        public int tSize = 24;

        static int r = 6,
            ofs,
            by;

        int cbofs = (r / 2) * 2
                - ((r / 2) - (r + 4));
        CloseButton b_close;

        public void StartAPI(Process p)
        {
            ofs = tSize / 2 - (r / 2) / 2 + (r / 4);
            b_close = new(
                p.wData.Position.X
                + p.wData.Position.Width
                - cbofs,
                by,
                p
            );
        }

        public void DrawT(Process p)
        {
            by = p.wData.Position.Y + ofs;
            b_close.X = p.wData.Position.X + p.wData.Position.Width - cbofs;
            b_close.Y = by;

            // Outline
            GUI.canv.DrawRectangle(
                p.wData.Position.X - 1,
                p.wData.Position.Y - 1,
                (ushort)(p.wData.Position.Width + 1),
                (ushort)(tSize + 1), 0,
                GUI.colors.tboColor
            );

            // Titlebar
            GUI.canv.DrawFilledRectangle(
                p.wData.Position.X,
                p.wData.Position.Y,
                (ushort)p.wData.Position.Width,
                (ushort)tSize, 0,
                GUI.colors.tbColor
            );

            // Title
            GUI.canv.DrawString(
                p.wData.Position.X + 8,
                p.wData.Position.Y + tSize / 2
                - GUI.fsy / 2,
                p.Name,
                GUI.dFont,
                GUI.colors.txtColor
            );

            b_close.Update();
        }

        public void DrawB(Process p)
        {
            // Outline
            GUI.canv.DrawRectangle(
                p.wData.Position.X - 1,
                p.wData.Position.Y + tSize,
                (ushort)(p.wData.Position.Width + 1),
                (ushort)(p.wData.Position.Height - tSize), 0,
                GUI.colors.moColor
            );

            // Window Bottom
            GUI.canv.DrawFilledRectangle(
                p.wData.Position.X,
                p.wData.Position.Y + tSize,
                (ushort)p.wData.Position.Width,
                (ushort)(p.wData.Position.Height - tSize), 0,
                GUI.colors.mColor
            );
        }
    }
}
