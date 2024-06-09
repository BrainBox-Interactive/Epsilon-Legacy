using IL2CPU.API.Attribs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.System.Resources
{
    public static class Files
    {
        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Images.Wallpapers.Default.bmp")]
        public static byte[] RawDefaultWallpaper;
        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Images.Wallpapers.1024x768.bmp")]
        public static byte[] Raw1024x768Wallpaper;

        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Images.Cursors.Default.bmp")]
        public static byte[] RawDefaultCursor;

        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Fonts.arial.ttf")]
        public static byte[] RawDefaultFont;
    }
}
