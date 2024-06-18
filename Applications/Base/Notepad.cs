using Cosmos.System;
using Epsilon.Interface;
using Epsilon.System.Critical.Processing;
using System.Drawing;

namespace Epsilon.Applications.Base
{
    public class Notepad : Process
    {
        public string Content = "";
        Window w;
        bool isPressed = false;

        public override void Start()
        {
            base.Start();
            this.w = new();

            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            int fh = this.w.tSize + h;

            this.w.StartAPI(this);
        }

        public override void Run()
        {
            this.w.DrawB(this); this.w.DrawT(this);

            if (Kernel.IsKeyPressed && !isPressed
                && Manager.toUpdate == this)
            {
                if (Kernel.k.Key == ConsoleKeyEx.Backspace
                    && Content.Length > 0)
                    Content = Content.Substring(0, Content.Length - 1);
                else if (Kernel.k.Key != ConsoleKeyEx.Backspace && Kernel.k.Key != ConsoleKeyEx.Enter
                    && Kernel.k.Key != ConsoleKeyEx.OEM102 && Kernel.k.Key != ConsoleKeyEx.OEM5
                    && Content.Length > -1)
                    Content += Kernel.k.KeyChar;
            }
            isPressed = Kernel.IsKeyPressed;

            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            int fh = this.w.tSize + h;
            GUI.canv.DrawString(
                Content,
                GUI.dFont,
                GUI.colors.txtColor,
                x + 10,
                y + 5 + this.w.tSize
            );

            if (Manager.toUpdate == this)
                GUI.canv.DrawFilledRectangle(
                    Color.White,
                    x + (Content.Length + 1) * GUI.dFont.Width + 4,
                    y + 5 + this.w.tSize + (GUI.dFont.Height / 2),
                    GUI.dFont.Width,
                    GUI.dFont.Height / 4
                );
        }
    }
}
