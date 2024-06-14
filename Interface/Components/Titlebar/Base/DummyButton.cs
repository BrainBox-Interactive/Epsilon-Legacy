using Cosmos.System;
using Epsilon.System.Critical.Processing;
using System.Drawing;

namespace Epsilon.Interface.Components.Titlebar.Base
{
    public class DummyButton : Button
    {
        Process p;
        public DummyButton(int x, int y, Process process)
            : base(x, y, 12, 12, Color.Gray, Color.LightGray, Color.DarkGray, "") {
            X = x;
            Y = y;
            Width = 12;
            Height = 12;
            p = process;
            OutlineColor = Color.DarkGray;
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
            //if (mIndex == 0
            //    && this != null
            //    && GUI.cProc == null
            //    && GUI.clicked)
            //    p.Remove();
        }
    }
}
