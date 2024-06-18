using Epsilon.Interface;
using Epsilon.System.Critical.Processing;
using Cosmos.System.Graphics;
using Epsilon.System.Resources;
using Epsilon.Interface.Components;

namespace Epsilon.Applications.System.Setup
{
    public class PSetup : Process
    {
        Bitmap setupImg = new(Files.RawSetupImage);
        Button next;

        public override void Start()
        {
            base.Start();
            next = new(
                546, 441,
                67, 24,
                GUI.colors.btColor,
                GUI.colors.bthColor,
                GUI.colors.btcColor,
                "Next"
            );
        }

        public override void Run()
        {
            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;

            GUI.canv.DrawImage(
                setupImg,
                0, 0
            );

            next.Update();
        }
    }
}
