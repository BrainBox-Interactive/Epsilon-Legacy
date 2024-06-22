using Epsilon.Interface;
using Epsilon.Interface.Components;
using Epsilon.Interface.Components.Text;
using Epsilon.System.Critical.Processing;
using System.Drawing;

namespace Epsilon.Applications.Base
{
    public class Notepad : Process
    {
        public string Content = "";
        Window w;
        Scrollbox sb;
        Box b;
        Button s, o;
        int ofs = 24;

        public override void Start()
        {
            base.Start();
            this.w = new();

            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            int fh = this.w.tSize + h;

            sb = new(x, y + ofs + this.w.tSize, w, h - ofs,
                "", this, true);

            b = new(x, y + this.w.tSize, w - 64 * 2, ofs - 1,
                Color.White, Color.Black, "Filepath", this);
            s = new(x + w - 64, y + this.w.tSize, 64, ofs - 1,
                GUI.colors.btColor, GUI.colors.bthColor, GUI.colors.btcColor,
                "Save", this, delegate() { });
            o = new(x + w - 64 * 2, y + this.w.tSize, 64, ofs - 1,
                GUI.colors.btColor, GUI.colors.bthColor, GUI.colors.btcColor,
                "Open", this, delegate () { });

            this.w.StartAPI(this);
        }

        public override void Run()
        {
            w.DrawB(this);

            GUI.canv.DrawRectangle(
                GUI.colors.tboColor, wData.Position.X - 1,
                sb.Y + sb.Height - 1, sb.state.Length * GUI.dFont.Width + 16 + 1,
                24 + 1);
            GUI.canv.DrawFilledRectangle(
                GUI.colors.mColor, wData.Position.X,
                sb.Y + sb.Height, sb.state.Length * GUI.dFont.Width + 16,
                24);

            sb.X = wData.Position.X;
            sb.Y = wData.Position.Y + w.tSize + ofs;
            sb.Update();

            b.X = wData.Position.X;
            b.Y = wData.Position.Y + w.tSize;
            b.Update();

            w.DrawT(this);
        }
    }
}
