using Epsilon.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.Interface.Components.Text
{
    public class Block : Component
    {
        public Color NormalColor { get; set; }
        public Color OutlineColor { get; set; }
        public Color TextColor { get; set; }
        public string Text { get; set; }

        public Block(int x, int y, int width, int height, string text,
            Color nColor, Color oColor, Color tColor) : base(x, y, width, height)
        {
            NormalColor = nColor;
            OutlineColor = oColor;
            TextColor = tColor;
            Text = text;
        }

        public override void Update()
        {
            base.Update();
            GUI.canv.DrawRectangle(
                OutlineColor,
                X - 1,
                Y - 1,
                Width + 1,
                Height + 1
            );
            GUI.canv.DrawFilledRectangle(
                NormalColor,
                X,
                Y,
                Width,
                Height
            );

            GUI.canv.DrawString(
                Text,
                GUI.dFont,
                TextColor,
                X + 8,
                Y + (Height / 2 - GUI.dFont.Height / 2) + 1
            );

            if (CheckHover())
            {
                GUI.crsChanged = true;
                GUI.crs = ESystem.wc;
            }
            else if (GUI.crsChanged) GUI.crsChanged = false;
        }
    }
}
