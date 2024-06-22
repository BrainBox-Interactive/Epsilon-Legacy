using Cosmos.System;
using Epsilon.System;
using Epsilon.System.Critical.Processing;
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
        public bool Editable { get; set; }
        public Process Process { get; set; }

        String str;
        Button up, down;
        private int cSect = 0;
        private int lPerSect, cPerSect;

        int ofs = GUI.dFont.Height + 2;
        bool clicked = false;

        List<string> temp;
        public Scrollbox(int x, int y, int width, int height, string content,
            Process p, bool editable = false)
            : base(x, y, width, height)
        {
            X = x; Y = y;
            Width = width;
            Height = height;
            Content = content;
            Editable = editable;
            Process = p;

            temp = Content.Split('\n')
                .ToList();
            up = new(x + width - 16, y, 16, 16,
                GUI.colors.btColor, GUI.colors.bthColor,
                GUI.colors.btcColor, "/\\", p,
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
                GUI.colors.btcColor, "\\/", p,
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
            = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_-\"\' /\\.,;:!?$*&()=";
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
                foreach (char c in s)
                    if (!AcceptedCharacters.Contains(c))
                        s = s.Replace(c, ' ');
                temp[i] = s;
                stringsToDraw.Add(s);
                line++;
            }
        }

        bool isFocused = false,
            isPressed = false;
        public override void Update()
        {
            base.Update();
            GUI.canv.DrawRectangle(Color.Black, X - 1,
                Y - 1, Width + 1, Height + 1);
            GUI.canv.DrawFilledRectangle(Color.White, X, Y,
                Width, Height);

            GUI.canv.DrawFilledRectangle(Color.Gray,
                X + Width - 16, Y + up.Height, 16, Height - up.Height - down.Height);
            GUI.canv.DrawLine(Color.Black, X + Width - 16, Y + up.Height,
                X + Width - 16, Y + up.Height + Height - down.Height);
            up.X = X + Width - 16; up.Y = Y;
            up.Update();
            down.X = X + Width - 16; down.Y = Y + Height - 16;
            down.Update();

            if (isFocused && Editable
                && cSect == temp.Count / lPerSect)
                GUI.canv.DrawString(
                    "Page " + (cSect + 1).ToString() + " - Writing",
                    GUI.dFont, Color.White, X, Y + Height + 4);
            else GUI.canv.DrawString("Page " + (cSect + 1).ToString(),
                    GUI.dFont, Color.White, X, Y + Height + 4);

            if (MouseManager.MouseState == MouseState.Left
                && !GUI.clicked && isFocused && !CheckHover())
                isFocused = false;

            if (CheckHover() && isFocused
                && Editable && !Manager.IsFrontTU(Process))
            {
                GUI.crsChanged = true;
                GUI.crs = ESystem.wc;
            }
            else if (GUI.crsChanged) GUI.crsChanged = false;

            if (MouseManager.MouseState == MouseState.None)
                clicked = false;

            if (isFocused && Editable
                && !Manager.IsFrontTU(Process))
            {
                // TODO: wr cursor & next-prev page key
                //GUI.canv.DrawFilledRectangle(Color.Black,
                //    X + (temp[temp.Count - 1].Length * GUI.dFont.Width) + GUI.dFont.Width + 2,
                //    Y + ofs * stringsToDraw.Count * GUI.dFont.Height,// - (GUI.dFont.Height / 2),
                //    1, GUI.dFont.Height);

                if (!isPressed && Kernel.IsKeyPressed
                    && cSect == temp.Count / (lPerSect + 1))
                {
                    if (Kernel.k.Key == ConsoleKeyEx.Backspace
                        && Content.Length > 0)
                    {
                        Content = Content.Substring(0, Content.Length - 1);
                        temp = Content.Split('\n')
                            .ToList();
                        //if (temp.Count < lPerSect * (cSect-1)
                        //    && cSect > 0) cSect--;
                    }
                    else if (Kernel.k.Key == ConsoleKeyEx.Tab
                        && (Content.Length * GUI.dFont.Width) < (Width - (GUI.dFont.Width * 2)))
                        Content += "    ";
                    else if (Kernel.k.Key == ConsoleKeyEx.Enter)
                    {
                        Content += '\n';
                        temp = Content.Split('\n')
                            .ToList();
                        //if ((cSect + 1) * lPerSect < (temp.Count - 1)) cSect++;
                    }
                    else if (Kernel.k.Key != ConsoleKeyEx.Backspace && Kernel.k.Key != ConsoleKeyEx.Enter
                        && Content.Length > -1
                        && (temp[temp.Count - 1].Length * GUI.dFont.Width) < ((Width - 16) - GUI.dFont.Width * 2))
                        if (Kernel.k.KeyChar >= 32
                            && Kernel.k.KeyChar <= 127)
                            Content += Kernel.k.KeyChar;
                    temp = Content.Split('\n')
                        .ToList();
                    ProcessStrings();
                }
            } isPressed = Kernel.IsKeyPressed;

            for (int i = 0; i < stringsToDraw.Count; i++)
                    GUI.canv.DrawString(stringsToDraw[i],
                        GUI.dFont, Color.Black, X + 8,
                        Y - 12 + ofs * i);
        }

        public override bool CheckHover()
        {
            if (GUI.mx >= X
                && GUI.mx <= X + Width - 16
                && GUI.my >= Y
                && GUI.my <= Y + Height)
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
    }
}
