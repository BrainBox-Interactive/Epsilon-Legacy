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
            version = "a.0014";
        public static CosmosVFS vfs;
        public static bool isGUI;

        public static int lastHCol;

        public static int _frames;
        public static int _fps = 200;
        public static int _deltaT = 0;

        protected override void BeforeRun()
        {
            Console.SetWindowSize(90, 30);
            Console.OutputEncoding = Sys.ExtendedASCII.CosmosEncodingProvider
                .Instance.GetEncoding(437);

            isGUI = false;
            vfs = new CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(vfs, true);
            Thread.Sleep(2000);

            Console.Clear();
            Thread.Sleep(750);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Epsilon Kernel - " + version);
            Console.WriteLine("June 2024 version // Experimental version\n");
            Console.ForegroundColor = ConsoleColor.White;
            //System.System.PlayAudio(Files.RawStartupAudio);
        }

        public static KeyEvent k;
        public static bool IsKeyPressed;
        protected override void Run()
        {
            if (isGUI)
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
                        if (IsKeyPressed)
                        {
                            if (k.Key == ConsoleKeyEx.F11)
                            {
                                Interface.GUI.canv.Disable();
                                Manager.pList.Clear();
                                Console.Clear();
                                isGUI = false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Interface.GUI.canv.Disable();
                    Manager.pList.Clear();
                    Heap.Collect();
                    Crash.Message(ex);
                    isGUI = false;
                }
            else
            {
                Console.ResetColor();
                Console.Write(curPath + "> ");
                var input = Console.ReadLine();
                Commands.Run(input);
            }

            if (lastHCol >= 50)
            {
                Heap.Collect();
                lastHCol = 0;
            } else lastHCol++;
        }
    }
}
