using PrismAPI.Graphics;

namespace Epsilon.Interface.Components
{
    public class Button : Component
    {
        public Color NormalColor { get; set; }
        public Color HoverColor { get; set; }
        public string Content { get; set; }

        // public Action onClick { get; set; }

        public Button(int x, int y, int width, int height, Color nColor, Color hColor, string content)
            : base(x, y, width, height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            NormalColor = nColor;
            HoverColor = hColor;
            Content = content;
        }

        public override void Update()
        {
            base.Update();

            GUI.canv.DrawRectangle(X, Y, (ushort)Width, (ushort)Height, 0, Color.Black);
            if (CheckHover()) GUI.canv.DrawFilledRectangle(X + 1, Y + 1, (ushort)(Width - 1), (ushort)(Height - 1), 0, HoverColor);
            else GUI.canv.DrawFilledRectangle(X + 1, Y + 1, (ushort)(Width - 1), (ushort)(Height - 1), 0, NormalColor);

            GUI.canv.DrawString(
                X + (Width / 2 - GUI.fsx * Content.Length / 2),
                Y + (Height / 2 - GUI.fsy / 2) + 1,
                Content, GUI.dFont, GUI.colors.btxtColor
            );
        }

        public override void OnClick(int mIndex)
            => base.OnClick(mIndex);
    }
}
