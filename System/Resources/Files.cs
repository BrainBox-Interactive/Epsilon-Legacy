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
        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Images.Wallpapers.Alpha-2.bmp")]
        public static byte[] RawAlphaWallpaper;
        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Images.Wallpapers.640x480.bmp")]
        public static byte[] Raw640x480Wallpaper;
        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Images.Wallpapers.1024x768.bmp")]
        public static byte[] Raw1024x768Wallpaper;
        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Images.Wallpapers.Test.bmp")]
        public static byte[] RawTestWallpaper;

        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Images.Setup.bmp")]
        public static byte[] RawSetupImage;
        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Images.TEPBanner.bmp")]
        public static byte[] RawTEPBanner;
        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Images.MenuBanner.bmp")]
        public static byte[] RawMenuBanner;

        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Images.Bars.CBar.bmp")]
        public static byte[] RawCBar;
        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Images.Bars.TBar.bmp")]
        public static byte[] RawTBar;

        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Images.Profile.Default.bmp")]
        public static byte[] RawDefaultPFP;

        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Images.Buttons.CB_idle.bmp")]
        public static byte[] RawIdleCButton;
        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Images.Buttons.CB_hover.bmp")]
        public static byte[] RawHoverCButton;
        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Images.Buttons.CB_click.bmp")]
        public static byte[] RawClickCButton;

        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Images.Cursors.Default.bmp")]
        public static byte[] RawDefaultCursor;
        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Images.Cursors.Hand.bmp")]
        public static byte[] RawHandCursor;
        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Images.Cursors.Write.bmp")]
        public static byte[] RawWriteCursor;
        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Images.Cursors.Moving.bmp")]
        public static byte[] RawMoveCursor;

        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Images.Icons.Default.bmp")]
        public static byte[] RawDefaultIcon;

        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Fonts.zap-ext-light16.psf")]
        public static byte[] RawDefaultFont;
        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Fonts.ter-powerline-v16n.psf")]
        public static byte[] RawPowerlineFont;

        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Audio.Startup.wav")]
        public static byte[] RawStartupAudio;
        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Audio.Shutdown.wav")]
        public static byte[] RawShutdownAudio;
        [ManifestResourceStream(ResourceName = "Epsilon.System.Resources.Audio.Error.wav")]
        public static byte[] RawErrorAudio;
    }
}
