using Epsilon.System;
using Epsilon.System.Critical.Processing;
using GrapeGL.Graphics;

namespace Epsilon.Interface.Components.Text
{
    public class Block : Component
    {
        public Color NormalColor { get; set; }
        public Color OutlineColor { get; set; }
        public Color TextColor { get; set; }
        public string Text { get; set; }
        public Process Process { get; set; }

        public Block(int x, int y, int width, int height, string text,
            Color nColor, Color oColor, Color tColor, Process p) : base(x, y, width, height)
        {
            NormalColor = nColor;
            OutlineColor = oColor;
            TextColor = tColor;
            Text = text;
            Process = p;
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
                X,
                Y,
                (ushort)Width,
                (ushort)Height,
                0,
                NormalColor
            );

            GUI.canv.DrawString(
                Text,
                GUI.dFont,
                TextColor,
                X + 8,
                Y + (Height / 2 - GUI.dFont.Height / 2) + 1
            );

            if (CheckHover()
                && !Manager.IsFrontTU(Process))
            {
                GUI.crsChanged = true;
                GUI.crs = ESystem.wc;
            }
            else if (GUI.crsChanged) GUI.crsChanged = false;
        }
    }
}
