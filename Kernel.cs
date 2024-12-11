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
using System.Drawing;
using Epsilon.Interface;

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
            Cosmos.HAL.Global.PIT.T0Frequency = 1000;
            vfs = new CosmosVFS();
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(vfs, true);
            System.ESystem.OnBoot();
            if (!isGUI)
            {
                Console.SetWindowSize(90, 30);
                Console.OutputEncoding = Sys.ExtendedASCII.CosmosEncodingProvider
                    .Instance.GetEncoding(437);
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Epsilon Kernel - " + version);
                Console.WriteLine("June 2024 version // Experimental version\n");
                Console.ForegroundColor = ConsoleColor.White;
                //System.System.PlayAudio(Files.RawStartupAudio);
            }
        }

        public static KeyEvent k;
        public static bool IsKeyPressed;
        protected override void Run()
        {
            if (isGUI)
                try
                {
                    if (lastHCol < 15) {
                        GUI.Update();

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
                                GUI.canv.Dispose();
                                Manager.pList.Clear();
                                Console.Clear();
                                isGUI = false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Manager.ClearAll();
                    Heap.Collect();
                    Manager.Start(new ErrorHandler
                    {
                        wData = new WindowData
                        {
                            Position = new Rectangle(0, 0, (int)GUI.width, (int)GUI.height),
                            Moveable = false
                        },
                        Name = "Error Handler",
                        Special = true,
                        Message = ex.ToString()
                    });
                }
            else
            {
                Console.ResetColor();
                Console.Write(curPath + "> ");
                var input = Console.ReadLine();
                Commands.Run(input);
            }

            if (lastHCol >= 12)
            {
                //Heap.Collect();
                lastHCol = 0;
            } else lastHCol++;
        }
    }
}
