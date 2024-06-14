using Cosmos.System;
using Epsilon.System.Critical.Processing;

namespace Epsilon.Interface.Components.Titlebar.Base
{
    public class CloseButton : Button
    {
        Process p;
        public CloseButton(int x, int y, Process process)
            : base(x, y, 12, 12, GUI.colors.qbColor, GUI.colors.qbhColor, GUI.colors.qboColor, "") {
            X = x;
            Y = y;
            Width = 12;
            Height = 12;
            p = process;
            OutlineColor = GUI.colors.qboColor;
        }

        public override void Update()
        {
            base.Update();
            if (CheckHover())
                if (MouseManager.MouseState == MouseState.Left)
                    OnClick(0);
        }

        public override void OnClick(int mIndex)
        {
            base.OnClick(mIndex);
            if (mIndex == 0
                && this != null
                && GUI.cProc == null
                && GUI.clicked)
                p.Remove();
        }
    }
}
