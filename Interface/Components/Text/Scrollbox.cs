using System;
using System.Collections.Generic;
using System.Drawing;

namespace Epsilon.Interface.Components.Text
{
    public class ScrollBox : Component
    {
        public string Content { get; set; }
        private List<string> sContent
            = new List<string>();
        String str;
        Button up, down;
        private int cSect = 0;
        private int lPerSect;

        public ScrollBox(int x, int y, int width, int height, string content)
            : base(x, y, width, height)
        {
            X = x; Y = y;
            Width = width;
            Height = height;
            Content = content;

            str = new(x + 8, y + 8,
                "", Color.Black);
            up = new(x + width - 16, y, 16, 16,
                GUI.colors.btColor, GUI.colors.bthColor,
                GUI.colors.btcColor, "▲",
                delegate () { if (cSect > 0) cSect--; });
            down = new(x + width - 16, y + height - 16,
                16, 16, GUI.colors.btColor, GUI.colors.bthColor,
                GUI.colors.btcColor, "▼",
                delegate () {
                    if (cSect > sContent.Count - 1)
                        cSect++;
                });

            lPerSect = (Height - 32) / (GUI.dFont.Height + 2);
            sContentSect();
        }

        private void sContentSect()
        {
            string[] words = Content.Split(new[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);
            List<string> cLine = new List<string>(),
                cSect = new List<string>();

            foreach (string w in words)
            {
                string pLine = string.Join(" ", cLine)
                    + (cLine.Count > 0 ? " " : "") + w;
                if ((pLine.Length * GUI.dFont.Width) <= Width - 32)
                    cLine.Add(w);
                else
                {
                    cSect.Add(string.Join(" ", cLine));
                    cLine.Clear();
                    cLine.Add(w);

                    if (cSect.Count >= lPerSect)
                    {
                        sContent.Add(string.Join("\n", cSect));
                        cSect.Clear();
                    }
                }
            }

            if (cLine.Count > 0)
                cSect.Add(string.Join(" ", cLine));
            if (cSect.Count > 0)
                sContent.Add(string.Join("\n", cSect));
        }

        public override void Update()
        {
            base.Update();
            GUI.canv.DrawRectangle(Color.Black, X - 1,
                Y - 1, Width + 1, Height + 1);
            GUI.canv.DrawFilledRectangle(Color.White, X, Y,
                Width, Height);

            if (sContent.Count > 0 && cSect < sContent.Count)
            {
                str.Text = sContent[cSect];
                str.X = X + 8; str.Y = Y + 8;
                str.Update();
            }

            up.X = X + Width - 16; up.Y = Y;
            up.Update();
            down.X = X + Width - 16; down.Y = Y + Height - 16;
            down.Update();
        }
    }
}