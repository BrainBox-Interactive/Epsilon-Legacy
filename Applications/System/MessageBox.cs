using Cosmos.System;
using Epsilon.Interface;
using Epsilon.Interface.Components;
using Epsilon.Interface.Components.Buttons;
using Epsilon.Interface.System;
using Epsilon.System.Critical.Processing;
using System;
using System.Numerics;

namespace Epsilon.Applications.System
{
    public class MessageBox : Process
    {
        public bool button;
        public string Content = "Default Window Text";
        OKButton b_button;

        public override void Start()
        {
            base.Start();

            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            int fh = Window.tSize + h;
            b_button = new(
                x + ((w / 2) - 16),
                y + (fh - 32 - 13),
                this
            );
        }

        public override void Remove()
        {
            if (b_button != null)
                b_button = null;
            base.Remove();
        }

        public override void Run()
        {
            Window.DrawT(this); Window.DrawB(this);

            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            GUI.canv.DrawString(
                Content,
                GUI.dFont,
                GUI.colors.txtColor,
                x + 10,
                y + 5 + Window.tSize
            );

            int fh = Window.tSize + h;
            if (button && b_button != null)
            {
                b_button.X = x + ((w / 2) - 16);
                b_button.Y = y + (fh - 32 - 13);
                b_button.Update();
            }
        }
    }
}
