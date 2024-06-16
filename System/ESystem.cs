using Epsilon.Interface;
using Cosmos.System.Graphics;
using Cosmos.System.Audio;
using Cosmos.HAL.Drivers.Audio;
using Cosmos.System.Audio.IO;
using Epsilon.System.Resources;
using Cosmos.System;
using s = System;

namespace Epsilon.System;

public static class ESystem
{
    public static AudioMixer mixer;
    public static AC97 driver;
    public static AudioManager audioManager;

    public static Bitmap dc = new Bitmap(Files.RawDefaultCursor),
        hc = new Bitmap(Files.RawHandCursor);

    public static void OnBoot()
    {
        Kernel.isGUI = true;
        GUI.Start();
        SetUpImages();
        PlayAudio(Files.RawStartupAudio);

        if (!VMTools.IsVMWare)
        {
            try {
                mixer = new AudioMixer();
                driver = AC97.Initialize(bufferSize: 4096);
                audioManager = new AudioManager()
                {
                    Stream = mixer,
                    Output = driver
                };
                audioManager.Enable();
            } catch { s.Console.Beep(600, 75); }
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
            } catch { s.Console.Beep(600, 75); }
        else s.Console.Beep(600, 75);
    }
}
