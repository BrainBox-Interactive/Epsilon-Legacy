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
                GUI.colors.tboColor,
                p.wData.Position.X - 1,
                p.wData.Position.Y - 1,
                p.wData.Position.Width + 1,
                tSize + 1
            );

            // Titlebar
            GUI.canv.DrawFilledRectangle(
                GUI.colors.tbColor,
                p.wData.Position.X,
                p.wData.Position.Y,
                p.wData.Position.Width,
                tSize
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

            b_close.Update();
        }

        public void DrawB(Process p)
        {
            // Outline
            GUI.canv.DrawRectangle(
                GUI.colors.moColor,
                p.wData.Position.X - 1,
                p.wData.Position.Y + tSize,
                p.wData.Position.Width + 1,
                p.wData.Position.Height - tSize + 1
            );

            // Window Bottom
            GUI.canv.DrawFilledRectangle(
                GUI.colors.mColor,
                p.wData.Position.X,
                p.wData.Position.Y + tSize,
                p.wData.Position.Width,
                p.wData.Position.Height - tSize
            );
        }
    }
}
