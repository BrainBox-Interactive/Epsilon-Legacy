using Epsilon.Interface;
using Epsilon.Interface.Components;
using Epsilon.System;
using Epsilon.System.Critical.Processing;
using Epsilon.System.Resources;
using System;

namespace Epsilon.Applications.System
{
    public class MessageBox : Process
    {
        public bool Button = true;
        public string Content = "Default Window Text";
        Button b_button;
        Window w;
        public Action Action = delegate() { Dummy(); };

        static void Dummy() { }

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
                32, 16,
                GUI.colors.btColor,
                GUI.colors.bthColor,
                GUI.colors.btcColor,
                "OK", this,
                delegate()
                {
                    Action.Invoke();
                    Remove();
                }
            );

            this.w.StartAPI(this);
            //ESystem.PlayAudio(Files.RawErrorAudio);
            ESystem.PlayAudio(Files.RawErrorAudio);
        }

        public override void Remove()
        {
            if (b_button != null)
                b_button = null;
            base.Remove();
        }

        public override void Run()
        {
            this.w.DrawB(this); this.w.DrawT(this);

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
            if (Button && b_button != null)
            {
                b_button.X = x + ((w / 2) - 16);
                b_button.Y = y + (fh - 32 - 13);
                b_button.Update();
            }
        }
    }
}
