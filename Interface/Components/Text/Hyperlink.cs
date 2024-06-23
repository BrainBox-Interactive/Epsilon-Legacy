using Cosmos.System;
using Epsilon.System;
using Epsilon.System.Critical.Processing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.Interface.Components.Text
{
    public class Hyperlink : Component
    {
        public Color NormalColor { get; set; }
        public Color HoverColor { get; set; }
        public string Text { get; set; }
        public Action Action { get; set; }
        public Process Process { get; set; }

        public Hyperlink(int x, int y, Color nColor, Color hColor, string text,
            Action action, Process process)
            : base(x, y, text.Length * GUI.dFont.Width, GUI.dFont.Height)
        {
            X = x; Y = y;
            NormalColor = nColor; HoverColor = hColor;
            Text = text;
            Action = action;
            Process = process;
        }

        public override void Update()
        {
            base.Update();
            if (CheckHover()
                && !Manager.IsFrontTU(Process)
                && GUI.cProc == null)
            {
                GUI.canv.DrawString(
                    Text, GUI.dFont,
                    HoverColor, X, Y
                );
                GUI.canv.DrawLine(
                    HoverColor,
                    X, Y + GUI.dFont.Height - 3,
                    X + Text.Length * GUI.dFont.Width,
                    Y + GUI.dFont.Height - 3
                );
                GUI.crsChanged = true;
                GUI.crs = ESystem.hc;
                //Manager.toUpdate = Process;

                if (MouseManager.MouseState == MouseState.Left
                    && !GUI.clicked)
                    OnClick(0);
            }
            else
            {
                GUI.canv.DrawString(
                    Text, GUI.dFont,
                    NormalColor, X, Y
                );
                GUI.crsChanged = false;
            }
        }

        public override void OnClick(int mIndex)
        {
            base.OnClick(mIndex);
            GUI.crsChanged = false;
            Action.Invoke();
        }
    }
}
