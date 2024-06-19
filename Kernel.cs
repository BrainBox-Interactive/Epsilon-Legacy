using System;
using Sys = Cosmos.System;
using Epsilon.System.Shell;
using Cosmos.System.FileSystem;
using System.Threading;
using Cosmos.Core.Memory;
using Cosmos.HAL;
using Epsilon.System.Critical.Processing;
using Epsilon.System.Critical;
using Cosmos.System;
using Console = System.Console;
using Epsilon.System.Resources;

namespace Epsilon
{
    public class Kernel : Sys.Kernel
    {
        public static string curPath = @"0:\",
            version = "1.0_alpha-bluelagoon";
        public static CosmosVFS vfs;
        public static bool isGUI;

        public static int lastHCol;

        public static int _frames;
        public static int _fps = 200;
        public static int _deltaT = 0;

        protected override void BeforeRun()
        {
            vfs = new CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(vfs, true);
            System.ESystem.OnBoot();
        }

        public static KeyEvent k;
        public static bool IsKeyPressed;
        protected override void Run()
        {
            try
            {
                if (lastHCol < 15) {
                    Interface.GUI.Update();

                    if (_deltaT != RTC.Second)
                    {
                        _fps = _frames;
                        _frames = 0;
                        _deltaT = RTC.Second;
                    }
                    _frames++;

                    k = null;
                    IsKeyPressed = KeyboardManager.TryReadKey(out k);
                }
            }
            catch (Exception ex)
            {
                if (!VMTools.IsVirtualBox) Interface.GUI.canv.Disable();
                Manager.pList.Clear();
                Heap.Collect();
                Crash.Message(ex);
                if (!VMTools.IsVirtualBox) isGUI = false;
            }

            if (lastHCol >= 50)
            {
                Heap.Collect();
                lastHCol = 0;
            } else lastHCol++;
        }
    }
}
