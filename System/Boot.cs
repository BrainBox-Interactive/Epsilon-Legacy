using Epsilon.Interface;
using Cosmos.System.Graphics;

namespace Epsilon.System
{
    public static class Boot
    {
        public static void OnBoot()
        {
            Kernel.isGUI = true;
            SetUpImages();
            GUI.Start();
        }

        private static void SetUpImages()
        {
            GUI.wp = new Bitmap(Resources.Files.RawDefaultWallpaper);
            GUI.crs = new Bitmap(Resources.Files.RawDefaultCursor);
        }
    }
}
