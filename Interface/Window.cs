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
        CloseButton b_close;
        DummyButton b_1, b_2, b_3;

        public void StartAPI(Process p, bool button = true)
        {
            if (button)
            {
                b_close = new(
                    p.wData.Position.X + p.wData.Position.Width - 12,
                    p.wData.Position.Y + tSize - 24,
                    p
                );
                b_1 = new(
                    p.wData.Position.X + p.wData.Position.Width - 24,
                    p.wData.Position.Y + tSize - 12,
                    p
                );
                b_2 = new(
                    p.wData.Position.X + p.wData.Position.Width - 24,
                    p.wData.Position.Y + tSize - 24,
                    p
                );
                b_3 = new(
                    p.wData.Position.X + p.wData.Position.Width - 12,
                    p.wData.Position.Y + tSize - 12,
                    p
                );
            }
        }

        public void DrawT(Process p, bool button = true)
        {
            // Outline
            GUI.canv.DrawRectangle(
                GUI.colors.tboColor,
                p.wData.Position.X - 1,
                p.wData.Position.Y - 1,
                p.Name.Length * GUI.dFont.Width + 16 * 2 + 8 + 1,
                tSize + 1
            );

            // Titlebar
            GUI.canv.DrawFilledRectangle(
                GUI.colors.tbColor,
                p.wData.Position.X,
                p.wData.Position.Y,
                p.Name.Length * GUI.dFont.Width + 16 * 2 + 8,
                tSize
            );

            if (p.wData.Icon != null)
                GUI.canv.DrawImageAlpha(
                    p.wData.Icon,
                    p.wData.Position.X + 8,
                    p.wData.Position.Y + tSize / 2
                    - (int)p.wData.Icon.Height / 2
                );

            // Title
            GUI.canv.DrawString(
                p.Name,
                GUI.dFont,
                GUI.colors.txtColor,
                p.wData.Position.X + (int)p.wData.Icon.Width + 8 * 2,
                p.wData.Position.Y + tSize / 2
                - GUI.dFont.Height / 2
            );

            if (button)
            {
                b_close.X = p.wData.Position.X + p.wData.Position.Width - 12; b_close.Y = p.wData.Position.Y + tSize - 24;
                b_1.X = p.wData.Position.X + p.wData.Position.Width - 24; b_1.Y = p.wData.Position.Y + tSize - 12;
                b_2.X = p.wData.Position.X + p.wData.Position.Width - 24; b_2.Y = p.wData.Position.Y + tSize - 24;
                b_3.X = p.wData.Position.X + p.wData.Position.Width - 12; b_3.Y = p.wData.Position.Y + tSize - 12;

                b_close.Update(); b_1.Update();
                b_2.Update(); b_3.Update();
            }
        }

        public void DrawB(Process p)
        {
            // Outline
            GUI.canv.DrawRectangle(
                GUI.colors.moColor,
                p.wData.Position.X - 1,
                p.wData.Position.Y + tSize - 1,
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
