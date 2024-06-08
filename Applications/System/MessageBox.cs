using Cosmos.HAL.BlockDevice.Ports;
using Cosmos.System;
using Epsilon.Interface;
using Epsilon.Interface.Components;
using Epsilon.Interface.Components.Buttons;
using Epsilon.Interface.Components.Titlebar.Base;
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
        Window w;

        public override void Start()
        {
            base.Start();
            this.w = new();

            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            int fh = this.w.tSize + h;
            b_button = new(
                x + ((w / 2) - 16),
                y + (fh - 32 - 13),
                this
            );

            this.w.StartAPI(this);
        }

        public override void Remove()
        {
            if (b_button != null)
                b_button = null;
            base.Remove();
        }

        public override void Run()
        {
            this.w.DrawT(this); this.w.DrawB(this);

            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            GUI.canv.DrawString(
                Content,
                GUI.dFont,
                GUI.colors.txtColor,
                x + 10,
                y + 5 + this.w.tSize
            );

            int fh = this.w.tSize + h;
            if (button && b_button != null)
            {
                b_button.X = x + ((w / 2) - 16);
                b_button.Y = y + (fh - 32 - 13);
                b_button.Update();
            }
        }
    }
}
