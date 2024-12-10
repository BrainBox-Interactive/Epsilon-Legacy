using Epsilon.Interface;
using Cosmos.System.Graphics;
using Cosmos.System.Audio;
using Cosmos.HAL.Drivers.Audio;
using Cosmos.System.Audio.IO;
using Epsilon.System.Resources;
using Cosmos.System;
using s = System;
using System.IO;
using Epsilon.System.Debug;
using Epsilon.Interface.System.Shell.Screen;
using Epsilon.System.Critical.Processing;
using System.Drawing;
using System.Threading;
using Cosmos.System.Graphics.Fonts;
using Epsilon.Applications.System;
using System;
using Epsilon.Applications.Base;
using Epsilon.Applications.System.Performance;
using Epsilon.Applications.FS;
using Cosmos.HAL.BlockDevice;

namespace Epsilon.System;

public static class ESystem
{
    public static AudioMixer mixer;
    public static AC97 driver;
    public static AudioManager audioManager;

    public static Bitmap dc = new Bitmap(Files.RawDefaultCursor),
        hc = new Bitmap(Files.RawHandCursor),
        wc = new Bitmap(Files.RawWriteCursor),
        mc = new Bitmap(Files.RawMoveCursor);
    public static Bitmap idleCB = new(Files.RawIdleCButton),
        hoverCB = new(Files.RawHoverCButton),
        clickCB = new(Files.RawClickCButton);

    public static Bitmap banner = new(Files.RawMenuBanner);

    public static string Drive = "0:\\",
        SystemPath = Drive + "Epsilon\\",
        SettingsPath = SystemPath + "Settings\\",
        LoginInfoPath = SettingsPath + "User\\LDE2Susr.cfg";

    public static string CurrentUser;

    public static void OnBoot()
    {
        s.Console.Clear();
        //if (VMTools.IsVirtualBox) Drive = "1:\\";
        SystemPath = Drive + "Epsilon\\";

        if (Kernel.vfs.GetDisks()[0].Partitions.Count < 1) Format();
        if (!Directory.Exists(SystemPath)) CreateDirectory(SystemPath);
        if (!Directory.Exists(SettingsPath)) CreateDirectory(SettingsPath);
        if (!Directory.Exists(SettingsPath + "User")) CreateDirectory(SettingsPath + "User");

        Log.Info("Launching GUI Interface");
        GUI.Start();
        Kernel.isGUI = true;
        SetUpImages();
        //Manager.Start(new Bokensha("0:\\")
        //{
        //    wData = new WindowData
        //    {
        //        Position = new(0, 0, 250, 400),
        //        Moveable = true
        //    },
        //    Name = "Bokensha",
        //    Special = false
        //});
    }

    public static void CreateDirectory(string path)
    {
        // hack that should be figured out later
        //if (!VMTools.IsVirtualBox)
        //{
        Log.Info("Creating folder: " + path);
        Directory.CreateDirectory(path);
        //}
    }

    public static void WriteFile(string path, string? contents,
        bool msg = false)
    {
        // hack that should be figured out later
        //if (!VMTools.IsVirtualBox)
        //{
        Log.Info("Writing file: " + path);
        File.WriteAllText(path, contents);
        if (msg)
            Manager.Start(new MessageBox
            {
                wData =
                {
                    Position = new(
                        GUI.width / 2 -
                        (("File " + path + " has been successfully saved.").Length * GUI.dFont.Width + 16) / 2,
                        GUI.height / 2 - 75 / 2,
                        ("File " + path + " has been successfully saved.").Length * GUI.dFont.Width + 16, 75),
                    Moveable = true
                },
                Content = "File " + path + " has been successfully saved.",
                Name = "Saved",
                Special = false,
                Button = true
            });
        //}
    }

    public static string? ReadFile(string path)
    {
        // hack that should be figured out later
        //if (!VMTools.IsVirtualBox)
        //{
        Log.Info("Reading file: " + path);
        return File.ReadAllText(path);
        //}
        //return null;
    }

    private static void SetUpImages()
    {
        Bitmap wp = new Bitmap(Files.RawAlphaWallpaper);
        //wp.Resize((uint)GUI.width, (uint)GUI.height);
        GUI.wp = wp;
        GUI.crs = new Bitmap(Files.RawDefaultCursor);
    }

    public static void PlayAudio(byte[] stream)
    {
        if (!VMTools.IsVMWare)
            try {
                mixer = new AudioMixer();
                driver = AC97.Initialize(bufferSize: 4096);
                audioManager = new AudioManager()
                {
                    Stream = mixer,
                    Output = driver
                };
                audioManager.Enable();
                var audioStream = MemoryAudioStream.FromWave(stream);
                mixer.Streams.Add(audioStream);
            } catch { s.Console.Beep(600, 25); }
        else s.Console.Beep(600, 25);
    }

    public static TopBar tBar;
    public static ControlBar cBar;
    public static void LogIn(bool GuestMode = false, bool GuestModeMB = true)
    {
        foreach (Process p in Manager.pList)
            if (p.Name == "Control Bar"
                || p.Name == "Top Bar")
                return;

        if (GuestMode)
        {
            CurrentUser = "Guest";
            if (GuestModeMB) 
                Manager.Start(new MessageBox
                {
                    wData = new WindowData
                    {
                        Position = new Rectangle(GUI.width / 2 -
                        (("You are in guest mode, any modification you bring will not be retained.".Length
                        * GUI.dFont.Width) + 16) / 2,
                        GUI.height / 2 - (int)(75 / 2),
                        ("You are in guest mode, any modification you bring will not be retained.".Length
                        * GUI.dFont.Width) + 16, 75),
                        Moveable = true
                    },
                    Name = "Guest Mode",
                    Content = "You are in guest mode, any modification you bring will not be retained.",
                    Special = false,
                    Button = true
                });
        }

        SpawnBars();
        PlayAudio(Files.RawStartupAudio);
    }

    public static void SpawnBars()
    {
        if (Global.topBarActivated)
            Manager.Start(tBar = new TopBar
            {
                wData = new WindowData
                {
                    Position = new Rectangle(0, 0, (int)GUI.width, 24),
                    Moveable = false
                },
                Special = false,
                Name = "Top Bar"
            });

        if (Global.controlBarActivated)
            Manager.Start(cBar = new ControlBar
            {
                wData = new WindowData
                {
                    Position = new Rectangle(0, (int)GUI.height - 32, (int)GUI.width, 32),
                    Moveable = false
                },
                Special = false,
                Name = "Control Bar"
            });
    }

    public static void Format()
    {
        if (Kernel.vfs.Disks[0].Partitions.Count > 0)
            for (int i = 0; i < Kernel.vfs.Disks[0].Partitions.Count; i++)
                Kernel.vfs.Disks[0].DeletePartition(i);
        Kernel.vfs.Disks[0].Clear();

        Kernel.vfs.Disks[0].CreatePartition(
            (int)(Kernel.vfs.Disks[0].Size / (1024 * 1024))
        );
        Kernel.vfs.Disks[0].FormatPartition(0, "FAT32");

        Log.Info("Formatted disk");
        Log.Warning("Rebooting in 3 seconds...");
        s.Threading.Thread.Sleep(3000);
        Power.Reboot();
    }

    public static void LogOff()
    {
        foreach (Process p in Manager.pList)
            if (p.Name == "Log into Epsilon")
                return;
        Manager.importantPList.Clear();

        CurrentUser = null;
        
        Manager.Start(new Login
        {
            wData = new WindowData
            {
                Position = new Rectangle(0, 0, (int)GUI.width, (int)GUI.height),
                Moveable = false
            },
            Name = "Log into Epsilon",
            Special = true
        });

        tBar.Remove(); cBar.Remove();
        // TODO: not make it crash the entire fucking system
        if (Manager.pList.Count > 0)
            foreach (Process p in Manager.pList)
                if (p.Name != "Log into Epsilon")
                    p.Remove();
    }
}
