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
using Epsilon.System.Critical.Processing;
using System.Drawing;
using System.Threading;
using Cosmos.System.Graphics.Fonts;
using Epsilon.Applications.System;
using System;

namespace Epsilon.System;

public static class ESystem
{
    public static AudioMixer mixer;
    public static AC97 driver;
    public static AudioManager audioManager;

    public static Bitmap dc = new Bitmap(Files.RawDefaultCursor),
        hc = new Bitmap(Files.RawHandCursor);

    public static string Drive = "0:\\";

    public static string CurrentUser;

    public static void OnBoot()
    {
        s.Console.Clear();
        if (VMTools.IsVirtualBox) Drive = "1:\\";

        if (!VMTools.IsVMWare)
        {
            try
            {
                mixer = new AudioMixer();
                driver = AC97.Initialize(bufferSize: 4096);
                audioManager = new AudioManager()
                {
                    Stream = mixer,
                    Output = driver
                };
                audioManager.Enable();
            }
            catch { }
        }

        Log.Info("Launching GUI Interface");
        GUI.Start();
        Kernel.isGUI = true;
        SetUpImages();
        PlayAudio(Files.RawPageTurnAudio);
    }

    public static void CreateDirectory(string path)
    {
        // hack that should be figured out later
        if (!VMTools.IsVirtualBox)
        {
            Log.Info("Creating folder: " + path);
            Directory.CreateDirectory(path);
        }
    }

    private static void SetUpImages()
    {
        GUI.wp = new Bitmap(Files.RawAlphaWallpaper);
        GUI.crs = new Bitmap(Files.RawDefaultCursor);
    }

    public static void PlayAudio(byte[] stream)
    {
        if (!VMTools.IsVMWare)
            try {
                var audioStream = MemoryAudioStream.FromWave(stream);
                mixer.Streams.Add(audioStream);
            } catch { s.Console.Beep(600, 25); }
        else s.Console.Beep(600, 25);
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
        Thread.Sleep(3000);
        Power.Reboot();
    }
}
