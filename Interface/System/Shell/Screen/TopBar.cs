using Cosmos.System.Graphics;
using Epsilon.System.Critical.Processing;
using Epsilon.System.Resources;
using System.Drawing;

namespace Epsilon.Interface.System.Shell.Screen
{
    public class TopBar : Process
    {
        Bitmap tb = new Bitmap(Files.RawTBar);
        int x, y, w, h;

        public override void Start()
        {
            base.Start();
            tb.Resize((uint)GUI.width, 24);
        }

        public override void Run()
        {
            x = wData.Position.X; y = wData.Position.Y;
            w = wData.Position.Width; h = wData.Position.Height;
            GUI.canv.DrawLine(GUI.colors.mooColor,
                x, y + 1, x + w, y + 1);
            GUI.canv.DrawFilledRectangle(
                GUI.colors.tbColor,
                x, y,
                w, h
            );
            //GUI.canv.DrawImage(tb, x, y);

            GUI.canv.DrawString(
                "TopBar",
                GUI.dFont,
                GUI.colors.txtColor,
                x, y
            );

            int tbx = 0, cx = 0;
            for (int i = 0; i < Manager.pList.Count; i++)
            {
                if (Manager.pList[i].Name != "Control Bar"
                    && Manager.pList[i].Name != "Top Bar"
                    && Manager.pList[i].Name != "Control Menu"
                    && (tbx + (8 * cx)) < GUI.width - Manager.pList[i].Name.Length * GUI.dFont.Width)
                {
                    int s = Manager.pList[i].Name.Length * GUI.dFont.Width
                        + (int)Manager.pList[i].wData.Icon.Width;
                    GUI.canv.DrawFilledRectangle(Color.Black,
                        tbx + (8 * cx),
                        y, s, h);

                    GUI.canv.DrawImageAlpha(
                        Manager.pList[i].wData.Icon, tbx + (8 * cx), y);
                    GUI.canv.DrawString(Manager.pList[i].Name,
                        GUI.dFont,
                        GUI.colors.txtColor,
                        tbx + (8 * cx) + (int)Manager.pList[i].wData.Icon.Width,
                        y);
                    tbx += s;
                    cx++;
                }
            }

            // todo: stuff
        }

        // DONE
    }
}
