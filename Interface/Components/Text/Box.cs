using Cosmos.System;
using Epsilon.System;
using Epsilon.System.Critical.Processing;
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
        public string Content { get; set; } = string.Empty;
        public bool Password = false;
        public string Placeholder { get; set; }
        public string AcceptedCharacters
            = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_-\"\'\n\t /\\,;:!?$*&()=";
        public bool Multiline { get; set; }
        public Process Process { get; set; }

        public Box(int x, int y, int width, int height,
            Color bColor, Color tColor, string placeholder,
            Process p, bool multiline = false)
            : base(x, y, width, height)
        {
            X = x; Y = y;
            Width = width; Height = height;
            BackColor = bColor; TextColor = tColor;
            Placeholder = placeholder;
            Multiline = multiline;
            Process = p;
        }

        public bool isFocused = false,
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

            if (Placeholder != "" && Content == "")
                GUI.canv.DrawString(Placeholder, GUI.dFont, Color.Gray,
                    X + 8, Y + Height / 2 - GUI.dFont.Height / 2);

            if (!Password) GUI.canv.DrawString(Content, GUI.dFont, TextColor,
                X + 8, Y + Height / 2 - GUI.dFont.Height / 2);
            else GUI.canv.DrawString(new string('*', Content.Length), GUI.dFont, TextColor,
                X + 8, Y + Height / 2 - GUI.dFont.Height / 2);

            if (MouseManager.MouseState == MouseState.Left
                && !GUI.clicked && isFocused && !CheckHover())
                isFocused = false;

            if (CheckHover()
                && !Manager.IsFrontTU(Process))
            {
                GUI.crsChanged = true;
                GUI.crs = ESystem.wc;
            } else if (GUI.crsChanged) GUI.crsChanged = false;

            if (isFocused)
            {
                DrawBlinkingCursor();
                if (!isPressed && Kernel.IsKeyPressed)
                    if (Kernel.k.Key == ConsoleKeyEx.Backspace
                        && Content.Length > 0)
                        Content = Content.Substring(0, Content.Length - 1);
                    else if (Kernel.k.Key == ConsoleKeyEx.Tab
                        && (Content.Length * GUI.dFont.Width) < (Width - (GUI.dFont.Width * 2)))
                        Content += "    ";
                    //else if (Kernel.k.Key == ConsoleKeyEx.Enter)
                        //Content += '\n';
                    else if (Kernel.k.Key != ConsoleKeyEx.Backspace && Kernel.k.Key != ConsoleKeyEx.Enter
                        && Content.Length > -1 && (Content.Length * GUI.dFont.Width) < (Width - GUI.dFont.Width * 2))
                        if ((int)Kernel.k.KeyChar >= 32
                            && (int)Kernel.k.KeyChar <= 127)
                            Content += Kernel.k.KeyChar;
            } isPressed = Kernel.IsKeyPressed;
        }

        public override bool CheckHover()
        {
            if (GUI.mx >= X + 1
                && GUI.mx <= X + Width - 1
                && GUI.my >= Y + 1
                && GUI.my <= Y + Height - 1)
                return true;
            return false;
        }

        public override void OnClick(int mIndex)
        {
            base.OnClick(mIndex);
            if (mIndex == 0)
                if (CheckHover()) isFocused = true;
                else if (isFocused) isFocused = false;
        }

        //int timer = 0;
        public void DrawBlinkingCursor()
        {
            //if (timer < 5000)
            //    GUI.canv.DrawFilledRectangle(TextColor, X + 8 + GUI.dFont.Width * Content.Length,
            //        Y + Height / 2 - GUI.dFont.Height / 2, 1, GUI.dFont.Height);
            //else if (timer >= 10000) timer = 0;
            //timer++;

            // TODO: make it actually BLINK
            GUI.canv.DrawFilledRectangle(TextColor, X + 8 + GUI.dFont.Width * Content.Length,
                Y + Height / 2 - GUI.dFont.Height / 2, 1, GUI.dFont.Height);
        }
    }
}
