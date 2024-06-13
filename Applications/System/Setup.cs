using Epsilon.Interface;
using Epsilon.System.Critical.Processing;
using Epsilon.System.Resources;
using Epsilon.Interface.Components;
using PrismAPI.Graphics;

namespace Epsilon.Applications.System
{
    public class Setup : Process
    {
        Canvas setupImg = Image.FromBitmap(Files.RawSetupImage);
        Button next;

        public override void Start()
        {
            base.Start();
            next = new(
                546, 441,
                67, 24,
                GUI.colors.btColor,
                GUI.colors.bthColor,
                "Next"
            );
        }

        public override void Run()
        {
            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;

            GUI.canv.DrawImage(
                0, 0, setupImg
            );

            next.Update();
        }
    }
}
