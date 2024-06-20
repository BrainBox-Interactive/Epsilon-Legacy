using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Epsilon.Interface.Components.Text
{
    public class String : Component
    {
        public string Text { get; set; }
        public Color Color { get; set; }

        string AcceptedCharacters
            = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_-\"\' .,;:!?$*&()=";

        public String(int x, int y, string text, Color color)
            : base(x, y, 0, 0)
        {
            X = x; Y = y;
            Width = text.Length
                * GUI.dFont.Width;
            Height = GUI.dFont.Height
                * text.Count(x => x == '\n');
            Text = text;
            Color = color;
            ProcessStrings();
        }

        int ofs = GUI.dFont.Height + 2;
        List<string> stringsToDraw = new List<string>();
        private void ProcessStrings()
        {
            stringsToDraw.Clear();
            stringsToDraw.Add(string.Empty);
            int lineIndex = 0;

            foreach (char c in Text)
                if (c == '\n')
                {
                    lineIndex++;
                    stringsToDraw.Add(string.Empty);
                }
                else if (AcceptedCharacters.Contains(c))
                    stringsToDraw[lineIndex] += c;
        }

        public override void Update()
        {
            base.Update();
            for (int i = 0; i < stringsToDraw.Count; i++)
                if (!string.IsNullOrWhiteSpace(stringsToDraw[i]))
                    GUI.canv.DrawString(stringsToDraw[i],
                        GUI.dFont, Color.White, X,
                        Y + ofs * i);
        }
    }
}