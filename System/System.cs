using Epsilon.Interface;
using Cosmos.System.Graphics;
using Cosmos.System.Audio;
using Cosmos.HAL.Drivers.Audio;
using Cosmos.System.Audio.IO;
using Epsilon.System.Resources;
using Cosmos.System;
using s = System;

namespace Epsilon.System;

public static class System
{
    public static void OnBoot()
    {
        Kernel.isGUI = true;
        GUI.Start();
        SetUpImages();
        PlayAudio(Files.RawStartupAudio);
    }

    private static void SetUpImages()
    {
        GUI.wp = new Bitmap(Files.RawAlphaWallpaper);
        GUI.crs = new Bitmap(Files.RawDefaultCursor);
    }

    public static void PlayAudio(byte[] stream)
    {
        if (!VMTools.IsVMWare)
        {
            try
            {
                var mixer = new AudioMixer();
                var audioStream = MemoryAudioStream.FromWave(stream);
                var driver = AC97.Initialize(bufferSize: 4096);
                mixer.Streams.Add(audioStream);

                var audioManager = new AudioManager()
                {
                    Stream = mixer,
                    Output = driver
                };
                audioManager.Enable();
            }
            catch
            {
                s.Console.Beep(600, 100);
            }
        }
        else s.Console.Beep(600, 100);
    }
}
