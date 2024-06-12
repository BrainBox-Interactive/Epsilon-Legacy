using Epsilon.System.Critical.Processing;

namespace Epsilon.Interface.Components.Titlebar.Base
{
    public class CloseButton : TitlebarButton
    {
        public Process p;
        public CloseButton(int x, int y, Process process)
            : base(x, y, process, GUI.colors.qbColor, GUI.colors.qbhColor) {
            p = process;
        }

        public override void Update()
            => base.Update();

        public override void OnClick(int mIndex)
        {
            if (mIndex == 0
                && this != null
                && GUI.cProc == null
                && Manager.toUpdate == p)
                p.Remove();
        }
    }
}
