using Cosmos.System;
using Epsilon.System.Critical.Processing;
using GrapeGL.Graphics;

namespace Epsilon.Interface.Components.Titlebar.Base
{
    public class InfoButton : Button
    {
        Process p;
        public InfoButton(int x, int y, Process process)
            : base(x, y, 12, 12, Color.DeepBlue, Color.GoogleBlue,
                  Color.DeepBlue, "", process) {
            X = x;
            Y = y;
            Width = 12;
            Height = 12;
            p = process;
            OutlineColor = Color.DeepBlue;
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
