using System.Drawing;

namespace Epsilon.Interface.Components
{
    public class Checkbox : Component
    {
        public string Text { get; set; }
        public bool State { get; set; }

        public Color CheckColor { get; set; }
        public Color TextColor { get; set; }

        public Checkbox(int x, int y, string text, Color cColor,
            Color tColor, bool initialState = false)
            : base(x, y, 16, 16)
        {
            X = x; Y = y;
            Text = text;
            State = initialState;
            TextColor = tColor;
            CheckColor = cColor;
        }

        public override void Update()
        {
            base.Update();
            GUI.canv.DrawRectangle(Color.Black, X, Y, Width, Height);
            GUI.canv.DrawFilledRectangle(CheckColor, X+1, Y+1, Width-1, Height-1);
            GUI.canv.DrawString(Text, GUI.dFont, TextColor, X + Width + 8,
                Y + Height / 2 - GUI.dFont.Height / 2 + 2);
        }
    }
}
