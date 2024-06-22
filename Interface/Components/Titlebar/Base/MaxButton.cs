using Cosmos.System;
using Epsilon.System.Critical.Processing;
using System.Drawing;

namespace Epsilon.Interface.Components.Titlebar.Base
{
    public class MaxButton : Button
    {
        Process p;
        public MaxButton(int x, int y, Process process)
            : base(x, y, 12, 12, Color.Gold, Color.LightGoldenrodYellow,
                  Color.Goldenrod, "", process) {
            X = x;
            Y = y;
            Width = 12;
            Height = 12;
            p = process;
            OutlineColor = Color.Goldenrod;
        }

        public override void Update()
        {
            base.Update();
            if (CheckHover())
                if (MouseManager.MouseState == MouseState.Left
                    && !GUI.clicked)
                    OnClick(0);
        }

        public override void OnClick(int mIndex)
        {
            base.OnClick(mIndex);
            //if (mIndex == 0
            //    && this != null
            //    && GUI.cProc == null
            //    && GUI.clicked)
            //    p.Remove();
        }
    }
}
