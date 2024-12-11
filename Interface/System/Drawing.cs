using Cosmos.System.Graphics.Fonts;
using GrapeGL.Graphics;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.Interface.System
{
    // Courtesy of Szymekk
    public static class Drawing
    {
        // ALERT!! Deprecated
        // Please use the component system instead
        public static void DrawButton(int x, int y, int width, int height, string content)
        {
            GUI.canv.DrawRectangle(Color.Black, x, y, width, height);

            if (GUI.mx >= x
                && GUI.mx <= x + width
                && GUI.my >= y
                && GUI.my <= y + height)
                GUI.canv.DrawFilledRectangle(x + 1, y + 1, (ushort)(width - 2), (ushort)(height - 2),
                    0, GUI.colors.bthColor);
            else GUI.canv.DrawFilledRectangle(x + 1, y + 1, (ushort)(width - 2), (ushort)(height - 2), 0, GUI.colors.btColor);

            GUI.canv.DrawString(content, GUI.dFont, GUI.colors.btxtColor,
                x + (width / 2 - GUI.dFont.Width * content.Length / 2),
                y + (height / 2 - GUI.dFont.Height / 2) + 1
            );
        }

        // Simple String, TODO: rich text component
        static List<string> stringsToDraw = new();
        static int index, lineIndex, ofs = GUI.dFont.Height + 2;
        static string AcceptedCharacters
            = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_-\"\'\n\t ,;:!?$*&()=";
        public static void DrawString(int x, int y, Color color, string text)
        {
            index = 0; lineIndex = 0;
            stringsToDraw.Clear();
            stringsToDraw.Add(string.Empty);
            foreach (char c in text)
                if (c == '\n')
                {
                    lineIndex++;
                    stringsToDraw.Add(string.Empty);
                }
                else if (AcceptedCharacters.Contains(c))
                    stringsToDraw[lineIndex] += c;
            for (int i = 0; i < stringsToDraw.Count; i++)
                GUI.canv.DrawString(stringsToDraw[i], GUI.dFont, Color.White,
                    x + 32, y + 64 + 8 + ofs * i);
        }
    }
}
