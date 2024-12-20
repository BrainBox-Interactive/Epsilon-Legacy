﻿using Epsilon.System.Debug;
using System;
using System.IO;
using System.Threading;

namespace Epsilon.System.Shell
{
    public static class Commands
    {
        public static void Run(string input)
        {
            string[] str = input.Split(' ');
            if (str.Length > 0)
            {
                switch (str[0])
                {
                    case "clr":
                        Console.Clear();
                        break;

                    case "si":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(Log.Center("Epsilon Kernel - " + Kernel.version));
                        Console.WriteLine(Log.Center("May 2024 version // Experimental version"));

                        for (int i = 0; i < Console.WindowWidth; i++)
                            Console.Write("-");
                        Console.WriteLine();

                        Console.Write(Log.Center("Copyright (C) BrainBox Interactive, 2024"));
                        Console.Write(Log.Center("https://github.com/BrainBox-Interactive/Epsilon"));
                        Console.ForegroundColor = ConsoleColor.White;
                        break;

                    case "p":
                        Console.ForegroundColor = ConsoleColor.Gray;
                        if (str.Length > 1)
                            Console.WriteLine(input.Replace(str[0] + " ", ""));
                        else
                            Log.Error("No arguments specified. Use \"print <string>\"");
                        break;

                    case "fs":
                        if (str.Length > 1)
                        {
                            switch (str[1])
                            {
                                case "fsp":
                                    long free = Kernel.vfs.GetAvailableFreeSpace(Kernel.curPath) / (1024 * 1024);
                                    Log.Info("Free space: " + free + " MB");
                                    break;

                                case "fmt":
                                    ESystem.Format();
                                    break;

                                case "ls":
                                    var dirs = Directory.GetDirectories(Kernel.curPath);
                                    var files = Directory.GetFiles(Kernel.curPath);

                                    if (dirs.Length > 0) Log.Info("Directories (" + dirs.Length + "):");
                                    for (int i = 0; i < dirs.Length; i++)
                                        Console.WriteLine("- " + dirs[i].Replace(Kernel.curPath + "\\", ""));
                                    if (dirs.Length > 0) Console.WriteLine();

                                    if (files.Length > 0) Log.Info("Files (" + files.Length + "):");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    for (int i = 0; i < files.Length; i++)
                                        Console.WriteLine("- " + files[i].Replace(Kernel.curPath + "\\", ""));
                                    break;

                                case "d":
                                    // fs d mk <dir>
                                    // fs d rm <dir>
                                    if (str.Length > 2)
                                        switch (str[2])
                                        {
                                            case "mk":
                                                Directory.CreateDirectory(Kernel.curPath + "\\" + str[3]);
                                                break;

                                            case "rm":
                                                Directory.Delete(Kernel.curPath + "\\" + str[3], true);
                                                break;

                                            default:
                                                Log.Error("Unknown command. Use \"fs dir <command> <arguments>\"");
                                                break;
                                        }
                                    else Log.Error("No arguments specified. Use \"fs dir <command> <arguments>\"");
                                    break;

                                case "wr":
                                    // write to file
                                    if (str.Length > 3)
                                        File.WriteAllText(
                                            Kernel.curPath + "\\" + str[2],
                                            input.Replace(str[0] + " " + str[1] + " " + str[2] + " ", "")
                                                 .Replace("\\n", "\n")
                                        );
                                    else
                                        Log.Error("No arguments specified. Use \"fs wr <file> <content>\"");
                                    break;

                                case "rd":
                                    // read from file
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                    if (str.Length > 2)
                                        Console.WriteLine(File.ReadAllText(Kernel.curPath + "\\" + str[2]));
                                    else
                                        Log.Error("No arguments specified. Use \"fs rd <file>\"");
                                    break;

                                case "rm":
                                    // delete file
                                    if (str.Length > 2)
                                        if (File.Exists(Kernel.curPath + "\\" + str[2]))
                                            File.Delete(Kernel.curPath + "\\" + str[2]);
                                        else
                                            Log.Error("File not found: " + str[2] + " in " + Kernel.curPath);
                                    else
                                        Log.Error("No arguments specified. Use \"fs del <file>\"");
                                    break;

                                case "cd":
                                    // change directory
                                    if (str.Length > 2)
                                        // check if // or \\
                                        if (str[2] == ".." && Kernel.curPath != "0:\\")
                                            if (Kernel.curPath.EndsWith("\\")
                                                && Directory.Exists(Kernel.curPath.Substring(0,
                                                Kernel.curPath.Remove(Kernel.curPath.LastIndexOf('\\')).LastIndexOf('\\'))))
                                                Kernel.curPath = Kernel.curPath.Substring(0, Kernel.curPath.LastIndexOf('\\')) + '\\';
                                            else if (Directory.Exists(Kernel.curPath.Substring(0, Kernel.curPath.LastIndexOf('\\'))))
                                                Kernel.curPath = Kernel.curPath.Substring(0, Kernel.curPath.LastIndexOf('\\')) + '\\';
                                        else if (Kernel.curPath.EndsWith("\\")
                                                && Directory.Exists(Kernel.curPath + str[2]))
                                            Kernel.curPath = Kernel.curPath + str[2];
                                        else if (!Kernel.curPath.EndsWith("\\")
                                                && Directory.Exists(Kernel.curPath + "\\" + str[2]))
                                            Kernel.curPath = Kernel.curPath + "\\" + str[2];
                                        else
                                            Log.Error("Directory not found: " + str[2] + " in " + Kernel.curPath);
                                    else
                                        Log.Error("No arguments specified. Use \"fs cd <dir>\"");
                                    break;
                            }
                        }
                        else
                            Log.Error("No arguments specified. Use \"fs <command> <arguments>\"");
                        break;

                    case "gui":
                        ESystem.OnBoot();
                        break;

                    case "set":
                        if (str.Length != 2) break;
                        var n = str[1].Split('=');
                        switch (n[0])
                        {
                            case "topb":
                                if (n[1] == "true") Global.topBarActivated = true;
                                else if (n[1] == "false") Global.topBarActivated = false;
                                break;

                            case "ctrlb":
                                if (n[1] == "true") Global.controlBarActivated = true;
                                else if (n[1] == "false") Global.controlBarActivated = false;
                                break;

                            default:
                                break;
                        }
                        break;

                    default:
                        Log.Error("Unknown command: " + str[0]);
                        break;
                }
            }
        }
    }
}
