using Epsilon.Interface;
using Epsilon.Interface.Components.Text;
using Epsilon.System.Critical.Processing;
using GrapeGL.Graphics;
using System.Threading;

namespace Epsilon.System.Critical
{
    public class ErrorHandler : Process
    {
        public string Message { get; set; }
        String String { get; set; }

        public override void Start()
        {
            base.Start();

            Special = true;
            Name = "Critical Error";
            wData.Moveable = false;

            wData.Position.X = 0;
            wData.Position.Y = 0;
            wData.Position.Width = GUI.width;
            wData.Position.Height = GUI.height;

            //foreach (Process p in Manager.pList)
            //    if (p.Name != "Critical Error")
            //        p.Remove();

            GUI.canv.DrawFilledRectangle(Color.Black,
                wData.Position.X, wData.Position.Y,
                wData.Position.Width, wData.Position.Height);
            Thread.Sleep(1000);

            GUI.DrawCursor = false;
            String = new String(10, 10, $@"An error has occurred!
---------------------------------
{Message}
---------------------------------
Please reboot the system or
press F1 to try to continue!", Color.White);
        }

        public override void Run()
        {
            base.Run();

            GUI.canv.DrawFilledRectangle(Color.Red,
                wData.Position.X, wData.Position.Y,
                wData.Position.Width, wData.Position.Height);
            String.Update();

            if (Kernel.IsKeyPressed
                && Kernel.k.Key == Cosmos.System.ConsoleKeyEx.F1)
            {
                GUI.DrawCursor = true;
                ESystem.SpawnBars();
                Remove();
            }
        }
    }
}
