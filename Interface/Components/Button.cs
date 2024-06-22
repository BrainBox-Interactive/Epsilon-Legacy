using Cosmos.System;
using Epsilon.Interface.System;
using Epsilon.System.Critical.Processing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.Interface.Components
{
    public class Button : Component
    {
        public Color NormalColor { get; set; }
        public Color HoverColor { get; set; }
        public Color ClickColor { get; set; }
        public string Content { get; set; }

        public Color OutlineColor { get; set; } = Color.Black;
        public Process Process { get; set; }
        public Action Action { get; set; } = delegate() { Dummy(); };

        public Button(int x, int y, int width, int height,
            Color nColor, Color hColor, Color cColor, string content,
            Process p, Action action = null)
            : base(x, y, width, height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            NormalColor = nColor;
            HoverColor = hColor;
            ClickColor = cColor;
            Content = content;
            Process = p;
            if (action != null) Action = action;
        }

        public static void Dummy() { }

        public override void Update()
        {
            base.Update();

            GUI.canv.DrawRectangle(OutlineColor, X, Y, Width, Height);
            if (CheckHover() && !Manager.IsFrontTU(Process))
                if (MouseManager.MouseState == MouseState.Left)
                {
                    GUI.canv.DrawFilledRectangle(ClickColor, X + 1, Y + 1, Width - 1, Height - 1);
                    if (!GUI.clicked) OnClick(0);
                }
                else GUI.canv.DrawFilledRectangle(HoverColor, X + 1, Y + 1, Width - 1, Height - 1);
            else GUI.canv.DrawFilledRectangle(NormalColor, X + 1, Y + 1, Width - 1, Height - 1);

            GUI.canv.DrawString(Content, GUI.dFont, GUI.colors.btxtColor,
                X + (Width / 2 - GUI.dFont.Width * Content.Length / 2),
                Y + (Height / 2 - GUI.dFont.Height / 2) + 1
            );
        }

        public override void OnClick(int mIndex)
        {
            if (Manager.IsFrontTU(Process)) return;
            base.OnClick(mIndex);
            Action.Invoke();
        }

        public override bool CheckHover()
        {
            if (GUI.mx >= X + 1
                && GUI.mx <= X + Width - 1
                && GUI.my >= Y + 1
                && GUI.my <= Y + Height - 1)
                return true;
            return false;
        }
    }
}
