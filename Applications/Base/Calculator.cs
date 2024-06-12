using Epsilon.Interface;
using Epsilon.Interface.Components;
using Epsilon.Interface.Components.Text;
using Epsilon.System.Critical.Processing;
using System.Drawing;

namespace Epsilon.Applications.Base
{
    public class Calculator : Process
    {
        public string Content = "";
        Window w;
        Block tb;

        Button one, two, three,
            four, five, six,
            seven, eight, nine;

        public override void Start()
        {
            base.Start();
            this.w = new();

            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            int fh = this.w.tSize + h;

            this.w.StartAPI(this);
            tb = new(x, y + this.w.tSize, w, 20, "Calculator", Color.White, Color.Black, Color.Black);

            int div3 = w / 3;
            one = new(x, y + this.w.tSize + 20 + (48 * 0), div3, 48, GUI.colors.btColor, GUI.colors.bthColor, "1");
            two = new(x + div3, y + this.w.tSize + 20 + (48 * 0), div3, 48, GUI.colors.btColor, GUI.colors.bthColor, "2");
            three = new(x + div3 * 2, y + this.w.tSize + 20 + (48 * 0), div3, 48, GUI.colors.btColor, GUI.colors.bthColor, "3");

            four = new(x, y + this.w.tSize + 20 + (48 * 1), div3, 48, GUI.colors.btColor, GUI.colors.bthColor, "4");
            five = new(x + div3, y + this.w.tSize + 20 + (48 * 1), div3, 48, GUI.colors.btColor, GUI.colors.bthColor, "5");
            six = new(x + div3 * 2, y + this.w.tSize + 20 + (48 * 1), div3, 48, GUI.colors.btColor, GUI.colors.bthColor, "6");

            seven = new(x, y + this.w.tSize + 20 + (48 * 2), div3, 48, GUI.colors.btColor, GUI.colors.bthColor, "7");
            eight = new(x + div3, y + this.w.tSize + 20 + (48 * 2), div3, 48, GUI.colors.btColor, GUI.colors.bthColor, "8");
            nine = new(x + div3 * 2, y + this.w.tSize + 20 + (48 * 2), div3, 48, GUI.colors.btColor, GUI.colors.bthColor, "9");
        }

        public override void Run()      
        {
            w.DrawT(this); w.DrawB(this);
            tb.X = wData.Position.X; tb.Y = wData.Position.Y + w.tSize;
            tb.Update();

            int div3 = wData.Position.Width / 3;
            one.X = wData.Position.X; one.Y = wData.Position.Y + w.tSize + 20; one.Update();
            two.X = wData.Position.X + div3; two.Y = wData.Position.Y + w.tSize + 20; two.Update();
            three.X = wData.Position.X + div3 * 2; three.Y = wData.Position.Y + w.tSize + 20; three.Update();

            four.X = wData.Position.X; four.Y = wData.Position.Y + w.tSize + 20 + (48 * 1); four.Update();
            five.X = wData.Position.X + div3; five.Y = wData.Position.Y + w.tSize + 20 + (48 * 1); five.Update();
            six.X = wData.Position.X + div3 * 2; six.Y = wData.Position.Y + w.tSize + 20 + (48 * 1); six.Update();

            seven.X = wData.Position.X; seven.Y = wData.Position.Y + w.tSize + 20 + (48 * 2); seven.Update();
            eight.X = wData.Position.X + div3; eight.Y = wData.Position.Y + w.tSize + 20 + (48 * 2); eight.Update();
            nine.X = wData.Position.X + div3 * 2; nine.Y = wData.Position.Y + w.tSize + 20 + (48 * 2); nine.Update();
        }
    }
}
