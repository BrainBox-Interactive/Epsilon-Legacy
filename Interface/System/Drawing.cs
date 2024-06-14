using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.Interface.System
{
    // Courtesy of Szymekk
    public static class Drawing
    {
        public static void DrawFullRoundedRectangle(int x, int y, int width, int height, int radius, Color col)
        {
        //    GUI.canv.DrawFilledRectangle(col, x + radius, y, width - 2 * radius, height);
        //    GUI.canv.DrawFilledRectangle(col, x, y + radius, radius, height - 2 * radius);
        //    GUI.canv.DrawFilledRectangle(col, x + width - radius, y + radius, radius, height - 2 * radius);
        //    GUI.canv.DrawFilledCircle(col, x + radius, y + radius, radius);
        //    GUI.canv.DrawFilledCircle(col, x + width - radius - 1, y + radius, radius);
        //    GUI.canv.DrawFilledCircle(col, x + radius, y + height - radius - 1, radius);
        //    GUI.canv.DrawFilledCircle(col, x + width - radius - 1, y + height - radius - 1, radius);
            DrawTopRoundedRectangle(x, y, width, height/2, radius, col);
            DrawBottomRoundedRectangle(x, y + height/2, width, height/2, radius, col);
        }

        public static void DrawMenuRoundedRectangle(int x, int y, int width, int height, int radius, Color col)
        {
            GUI.canv.DrawFilledRectangle(col, x + radius, y, width - 2 * radius, height);
            GUI.canv.DrawFilledRectangle(col, x, y, radius, height);
            GUI.canv.DrawFilledRectangle(col, x + width - radius, y + radius, radius, height - 2 * radius);
            GUI.canv.DrawFilledCircle(col, x + width - radius - 1, y + radius, radius);
            GUI.canv.DrawFilledCircle(col, x + width - radius - 1, y + height - radius - 1, radius);
        }

        public static void DrawTopRoundedRectangle(int x, int y, int width, int height, int radius, Color col)
        {
            GUI.canv.DrawFilledRectangle(col, x + radius, y, width - 2 * radius, height);
            GUI.canv.DrawFilledRectangle(col, x, y + radius, width, height - radius);
            GUI.canv.DrawFilledCircle(col, x + radius, y + radius, radius);
            GUI.canv.DrawFilledCircle(col, x + width - radius - 1, y + radius, radius);
        }

        public static void DrawBottomRoundedRectangle(int x, int y, int width, int height, int radius, Color col)
        {
            GUI.canv.DrawFilledRectangle(col, x, y, width, height - radius);
            GUI.canv.DrawFilledRectangle(col, x + radius, y + height - radius, width - 2 * radius, radius);
            GUI.canv.DrawFilledCircle(col, x + radius, y + height - radius - 1, radius);
            GUI.canv.DrawFilledCircle(col, x + width - radius - 1, y + height - radius - 1, radius);
        }

        // ALERT!! Deprecated
        // Please use the component system instead
        public static void DrawButton(int x, int y, int width, int height, string content)
        {
            GUI.canv.DrawRectangle(Color.Black, x, y, width, height);

            if (GUI.mx >= x
                && GUI.mx <= x + width
                && GUI.my >= y
                && GUI.my <= y + height)
                GUI.canv.DrawFilledRectangle(GUI.colors.bthColor, x + 1, y + 1, width - 2, height - 2);
            else GUI.canv.DrawFilledRectangle(GUI.colors.btColor, x + 1, y + 1, width - 2, height - 2);

            GUI.canv.DrawString(content, GUI.dFont, GUI.colors.btxtColor,
                x + (width / 2 - GUI.dFont.Width * content.Length / 2),
                y + (height / 2 - GUI.dFont.Height / 2) + 1
            );
        }
    }
}
