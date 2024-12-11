using Cosmos.System;
using Epsilon.System.Critical.Processing;
using GrapeGL.Graphics;

namespace Epsilon.Interface.Components
{
    public class Checkbox : Component
    {
        public string Text { get; set; }
        public bool Checked { get; set; }
        public Process Process { get; set; }

        public Color CheckColor { get; set; } = Color.White;
        public Color CheckHoverColor { get; set; } = Color.LightGray;
        public Color CheckClickColor { get; set; } = Color.DeepGray;
        public Color TextColor { get; set; } = GUI.colors.txtColor;

        public Checkbox(int x, int y, string text,
            Process p, bool initialState = false)
            : base(x, y, 16, 16)
        {
            X = x; Y = y;
            Text = text;
            Checked = initialState;
            Process = p;
        }

        private const int ofs = 5;
        bool clicked = false;
        public override void Update()
        {
            base.Update();
            GUI.canv.DrawRectangle(Color.Black, X, Y, Width, Height);
            if (CheckHover()
                && !Manager.IsFrontTU(Process))
                GUI.canv.DrawFilledRectangle(X + 1, Y + 1,
                    (ushort)(Width - 1), (ushort)(Height - 1), 0, CheckHoverColor);
            else GUI.canv.DrawFilledRectangle(X + 1, Y + 1,
                (ushort)(Width - 1), (ushort)(Height - 1), 0, CheckColor);
            GUI.canv.DrawString(Text, GUI.dFont, TextColor, X + Width + 12,
                Y + Height / 2 - GUI.dFont.Height / 2 + 1);
            if (Checked) GUI.canv.DrawFilledRectangle(X + ofs, Y + ofs,
                (ushort)(Width - (ofs * 2 - 1)), (ushort)(Height - (ofs * 2 - 1)), 0, Color.Black);
            if (MouseManager.MouseState == MouseState.Left
                && CheckHover()
                && !Manager.IsFrontTU(Process))
            {
                if (!GUI.clicked)
                {
                    if (!clicked) Checked = !Checked;
                    clicked = true;
                }
                GUI.canv.DrawFilledRectangle(X + 1, Y + 1,
                    (ushort)(Width - 1), (ushort)(Height - 1), 0, CheckClickColor);
            }
            else if (MouseManager.MouseState == MouseState.None
                && clicked)
                clicked = false;
        }
    }
}