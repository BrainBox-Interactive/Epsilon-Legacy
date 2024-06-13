using PrismAPI.Graphics;

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
                X - 1,
                Y - 1,
                (ushort)(Width + 1),
                (ushort)(Height + 1),
                0,
                OutlineColor
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
                X + 8,
                Y + (Height / 2 - GUI.fsy / 2) + 1,
                Text,
                GUI.dFont,
                TextColor
            );
        }
    }
}
