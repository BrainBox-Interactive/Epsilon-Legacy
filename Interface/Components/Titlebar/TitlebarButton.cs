using Cosmos.HAL;
using Cosmos.System;
using Epsilon.System.Critical.Processing;
using PrismAPI.Graphics;

namespace Epsilon.Interface.Components.Titlebar
{
    public class TitlebarButton : Component
    {
        public Process p;
        public Color NormalColor { get; set; }
        public Color HoverColor { get; set; }

        public TitlebarButton(int x, int y, Process process, Color nColor, Color hColor)
            : base(x, y, 6, 6)
        {
            p = process;
            NormalColor = nColor;
            HoverColor = hColor;
        }

        public override void Update()
        {
            //GUI.canv.DrawCircle(Color.Black, X, Y, 7);
            if (CheckHover())
            {
                GUI.canv.DrawFilledCircle(X, Y, 6, HoverColor);
                if (MouseManager.MouseState == MouseState.Left)
                    OnClick(0);
            }
            else GUI.canv.DrawFilledCircle(X, Y, 6, NormalColor);
        }

        public override bool CheckHover()
        {
            if (GUI.mx >= X - Width
                && GUI.mx <= X + Width
                && GUI.my >= Y - Height
                && GUI.my <= Y + Height)
                return true;
            return false;
        }
    }
}
