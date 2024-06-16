using Epsilon.Interface;
using Epsilon.Interface.Components.Text;
using Epsilon.System.Critical.Processing;
using System.Drawing;

namespace Epsilon.Applications.System.Setup
{
    // Out of Box Experience
    public class OOBE : Process
    {
        Window win;
        Box username, password;

        public override void Start()
        {
            base.Start();
            wData.Position.Width = GUI.width - 768;
            wData.Position.Height = GUI.height - 512;
            wData.Position.X = GUI.width / 2 - wData.Position.Width / 2;
            wData.Position.Y = GUI.height / 2 - wData.Position.Height / 2;

            win = new();
            win.StartAPI(this);

            username = new(wData.Position.X, wData.Position.Y + win.tSize,
                256, 24, Color.White, Color.Black, "Username"
            );
            password = new(wData.Position.X, wData.Position.Y + win.tSize + 24,
                256, 24, Color.White, Color.Black, "Password"
            ); password.Password = true;
        }

        public override void Run()
        {
            win.DrawT(this); win.DrawB(this);

            username.X = wData.Position.X;
            username.Y = wData.Position.Y + win.tSize;
            username.Update();

            password.X = wData.Position.X;
            password.Y = wData.Position.Y + win.tSize + 24;
            password.Update();
        }
    }
}
