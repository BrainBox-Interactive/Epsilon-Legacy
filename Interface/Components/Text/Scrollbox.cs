using Cosmos.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;

namespace Epsilon.Interface.Components.Text
{
    public class Scrollbox : Component
    {
        public string Content { get; set; }
        String str;
        Button up, down;
        private int cSect = 0;
        private int lPerSect, cPerSect;

        int ofs = GUI.dFont.Height + 2;
        bool clicked = false;

        List<string> temp;
        public Scrollbox(int x, int y, int width, int height, string content)
            : base(x, y, width, height)
        {
            X = x; Y = y;
            Width = width;
            Height = height;
            Content = content;

            temp = Content.Split('\n')
                .ToList();
            up = new(x + width - 16, y, 16, 16,
                GUI.colors.btColor, GUI.colors.bthColor,
                GUI.colors.btcColor, "^",
                delegate () {
                    if (!clicked && cSect > 0)
                    {
                        cSect--;
                        ProcessStrings();
                    }
                    clicked = true;
                });
            down = new(x + width - 16, y + height - 16,
                16, 16, GUI.colors.btColor, GUI.colors.bthColor,
                GUI.colors.btcColor, "v",
                delegate () {
                    if (!clicked)
                    {
                        cSect++;
                        ProcessStrings();
                    }
                    clicked = true;
                });

            lPerSect = Height / ofs;
            cPerSect = Content.Length / lPerSect;
            ProcessStrings();
        }

        List<string> stringsToDraw = new List<string>();
        string AcceptedCharacters
            = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_-\"\' .,;:!?$*&()=";
        private void ProcessStrings()
        {
            stringsToDraw.Clear();
            stringsToDraw.Add(string.Empty);
            int line = 0;
            if (lPerSect * cSect >= temp.Count) return;
            for (int i = lPerSect * cSect; i < temp.Count; i++)
            {
                if (line >= lPerSect) break;
                string s = temp[i];
                if (string.IsNullOrEmpty(s)) continue;
                foreach (char c in s)
                    if (!AcceptedCharacters.Contains(c))
                        s = s.Replace(c, ' ');
                temp[i] = s;
                stringsToDraw.Add(s);
                line++;
            }
        }

        public override void Update()
        {
            base.Update();
            GUI.canv.DrawRectangle(Color.Black, X - 1,
                Y - 1, Width + 1, Height + 1);
            GUI.canv.DrawFilledRectangle(Color.White, X, Y,
                Width, Height);

            up.X = X + Width - 16; up.Y = Y;
            up.Update();
            down.X = X + Width - 16; down.Y = Y + Height - 16;
            down.Update();
            GUI.canv.DrawString(cSect.ToString(),
                GUI.dFont, Color.White, X, Y + Height);

            if (MouseManager.MouseState == MouseState.None)
                clicked = false;

            for (int i = 0; i < stringsToDraw.Count; i++)
                if (!string.IsNullOrWhiteSpace(stringsToDraw[i]))
                    GUI.canv.DrawString(stringsToDraw[i],
                        GUI.dFont, Color.Black, X + 8,
                        Y - 12 + ofs * i);
        }
    }
}
