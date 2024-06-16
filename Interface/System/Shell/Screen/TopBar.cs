using Epsilon.System.Critical.Processing;

namespace Epsilon.Interface.System.Shell.Screen
{
    public class TopBar : Process
    {
        public override void Run()
        {
            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            //GUI.canv.DrawFilledRectangle(
            //    GUI.colors.tboColor,
            //    x, y + 1,
            //    w, 1
            //);
            GUI.canv.DrawLine(
                GUI.colors.tboColor,
                x, y + 1, x + w, y + 1
            );

            GUI.canv.DrawFilledRectangle(
                GUI.colors.tbColor,
                x, y,
                w, h
            );

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
                    && Manager.pList[i].Name != "Control Menu")
                {
                    int s = Manager.pList[i].Name.Length * GUI.dFont.Width;
                    GUI.canv.DrawFilledRectangle(
                        GUI.colors.tboColor,
                        tbx + (8 * cx), y,
                        s, h
                    );

                    GUI.canv.DrawString(
                        Manager.pList[i].Name,
                        GUI.dFont,
                        GUI.colors.txtColor,
                        tbx + (8 * cx), y
                    );
                    tbx += Manager.pList[i].Name.Length * GUI.dFont.Width;
                    cx++;
                }
            }

            // todo: stuff
        }

        // DONE
    }
}
