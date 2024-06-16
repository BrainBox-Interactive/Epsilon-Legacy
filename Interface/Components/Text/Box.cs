using Cosmos.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.Interface.Components.Text
{
    public class Box : Component
    {
        public Color BackColor { get; set; }
        public Color TextColor { get; set; }
        public string Content { get; set; }

        public Box(int x, int y, int width, int height, Color bColor, Color tColor)
            : base(x, y, width, height)
        {
            X = x; Y = y;
            Width = width; Height = height;
            BackColor = bColor; TextColor = tColor;
        }

        bool isFocused = false,
            isPressed = false;
        public override void Update()
        {
            base.Update();
            GUI.canv.DrawRectangle(
                Color.Black, X - 1, Y - 1,
                Width + 1, Height + 1
            );
            GUI.canv.DrawFilledRectangle(
                BackColor, X, Y,
                Width, Height
            );

            if (MouseManager.MouseState == MouseState.Left
                && !GUI.clicked)
                if (CheckHover()) isFocused = true;
                else isFocused = false;

            if (isFocused)
            {
                GUI.canv.DrawFilledRectangle(TextColor, X + 8 + GUI.dFont.Width * (Content.Length + 1),
                    Y + Height / 2 + GUI.dFont.Height / 2, 1, GUI.dFont.Height);
                if (!isPressed)
                    if (Kernel.k.Key == ConsoleKeyEx.Backspace
                    && Content.Length > 0)
                        Content = Content.Substring(0, Content.Length - 1);
                    else if (Kernel.k.Key != ConsoleKeyEx.Backspace && Kernel.k.Key != ConsoleKeyEx.Enter
                        && Kernel.k.Key != ConsoleKeyEx.OEM102 && Kernel.k.Key != ConsoleKeyEx.OEM5
                        && Content.Length > -1)
                        Content += Kernel.k.KeyChar;
            } isPressed = Kernel.IsKeyPressed;

            GUI.canv.DrawString(Content, GUI.dFont, TextColor,
                X + 8, Y + Height / 2 + GUI.dFont.Height / 2);
        }
    }
}
