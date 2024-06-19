using Epsilon.Interface;
using Epsilon.System.Critical.Processing;
using Cosmos.System.Graphics;
using Epsilon.System.Resources;
using Epsilon.Interface.Components;
using System.Drawing;
using System.Collections.Generic;
using Cosmos.System;

namespace Epsilon.Applications.System.Setup
{
    public class PSetup : Process
    {
        Bitmap setupImg = new(Files.RawSetupImage);
        Button next;
        string nButton = "Next";
        int currentPage = 0;
        string[] strings = {
@"Welcome to the Epsilon setup wizard!
This tool is designed to assist you in installing Epsilon
onto your hard drive.
Please click the 'Next' button when you are ready to begin.

Important! Using this tool will erase all data on your hard drive.
Please back up any important data before proceeding. Use with caution.

Warning! The Epsilon Project is still in its early stages.
Using it may cause irreversible damage to your data or other issues.
Please proceed with caution.
",
@"Page 2",
@"Page 3",
@"Page 4"
        };
        string AcceptedCharacters
            = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_-\"\' .,;:!?$*&()=";
        bool clicked = false;

        public override void Start()
        {
            base.Start();
            next = new(
                546, 441,
                67, 24,
                GUI.colors.btColor,
                GUI.colors.bthColor,
                GUI.colors.btcColor,
                nButton,
                delegate()
                {
                    if (!clicked
                        && strings.Length > currentPage)
                        currentPage++;
                    clicked = true;
                }
            );
            currentPage = 0;
        }

        int ofs = GUI.dFont.Height + 2;
        int index = 0, lineIndex = 0;
        List<string> stringsToDraw = new List<string>();
        public override void Run()
        {
            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            next.Content = nButton;

            if (MouseManager.MouseState == MouseState.None
                && clicked)
                clicked = false;

            GUI.canv.DrawImage(
                setupImg,
                0, 0
            );

            GUI.canv.DrawString(Kernel._fps.ToString(), GUI.dFont, Color.White, 0, 0);

            index = 0; lineIndex = 0;
            stringsToDraw.Clear();
            stringsToDraw.Add(string.Empty);
            if (strings.Length >= currentPage)
                foreach (char c in strings[currentPage])
                    if (c == '\n')
                    {
                        lineIndex++;
                        stringsToDraw.Add(string.Empty);
                    }
                    else if (AcceptedCharacters.Contains(c))
                        stringsToDraw[lineIndex] += c;
            for (int i = 0; i < stringsToDraw.Count; i++)
                GUI.canv.DrawString(stringsToDraw[i], GUI.dFont, Color.White,
                    x + 32, y + 64 + 8 + ofs * i);
            next.Update();
        }
    }
}
