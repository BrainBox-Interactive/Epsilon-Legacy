using Epsilon.System.Critical.Processing;

namespace Epsilon.Interface.System.Shell.Screen
{
    public class TopBar : Process
    {
        public override void Run()
        {
            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            GUI.canv.DrawFilledRectangle(
                x, y + 1,
                (ushort)w, 1, 0,
                GUI.colors.tboColor
            );

            GUI.canv.DrawFilledRectangle(
                x, y,
                (ushort)w, (ushort)h, 0,
                GUI.colors.tbColor
            );

            GUI.canv.DrawString(
                x, y,
                "TopBar",
                GUI.dFont,
                GUI.colors.txtColor
            );

            int cx = 0;
            for (int i = 0; i < Manager.pList.Count; i++)
            {
                if (Manager.pList[i].Name != "Control Bar"
                    && Manager.pList[i].Name != "Top Bar"
                    && Manager.pList[i].Name != "Control Menu")
                {
                    GUI.canv.DrawFilledRectangle(
                        (x + 128) * cx + 8 * cx, y,
                        128, (ushort)h, 0,
                        GUI.colors.tboColor
                    );

                    GUI.canv.DrawString(
                        (x + 128) * cx + 8 * cx, y,
                        Manager.pList[i].Name,
                        GUI.dFont,
                        GUI.colors.txtColor
                    );
                    cx++;
                }
            }

            // todo: stuff
        }

        // DONE
    }
}
