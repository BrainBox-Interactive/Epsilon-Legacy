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
                GUI.colors.tboColor,
                x, y + 1,
                w, 1
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
        }

        // DONE
    }
}
