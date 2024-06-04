using Cosmos.System;
using Epsilon.Interface;
using Epsilon.Interface.System;
using Epsilon.System.Critical.Processing;
using System.Numerics;

namespace Epsilon.Applications.System
{
    public class MessageBox : Process
    {
        public bool Button;
        public string Content = "Default Window Text";
        public override void Run()
        {
            Window.DrawT(this);

            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            Drawing.DrawBottomRoundedRectangle(
                x, y + Window.tSize,
                w, h - Window.tSize,
                4,
                GUI.colors.mColor
            );

            GUI.canv.DrawString(
                Content,
                GUI.dFont,
                GUI.colors.txtColor,
                x + 10,
                y + 5 + Window.tSize
            );

            int fh = Window.tSize + h;
            if (Button)
            {
                Drawing.DrawButton(
                    x + ((w / 2) - 16),
                    y + (fh - 32 - 13),
                    32,
                    16,
                    "OK"
                );

                if (MouseManager.MouseState == MouseState.Left 
                    && !GUI.clicked
                    && (Manager.toUpdate == this || Manager.toUpdate == null))
                {
                    if (GUI.mx >= x + ((w / 2) - 16)
                        && GUI.mx <= x + ((w / 2) - 16) + 32)
                    {
                        if (GUI.my >= y + (fh - 32 - 13)
                            && GUI.my <= y + (fh - 32 - 13) + 16)
                            Manager.pList.Remove(this);
                    }
                }

                // DONE
            }
        }
    }
}
