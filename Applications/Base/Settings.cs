using Epsilon.Interface;
using Epsilon.System.Critical.Processing;

namespace Epsilon.Applications.Base
{
    public class Settings : Process
    {
        Window win;

        public override void Start()
        {
            win = new();
            win.StartAPI(this);
        }

        public override void Run()
        {
            win.DrawB(this); win.DrawT(this);
            
            // TODO: List of settings (bg, font)
        }
    }
}
