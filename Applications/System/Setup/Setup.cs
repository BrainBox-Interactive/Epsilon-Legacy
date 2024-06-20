using Epsilon.Interface;
using Epsilon.System.Critical.Processing;
using Cosmos.System.Graphics;
using Epsilon.System.Resources;
using Epsilon.Interface.Components;
using System.Drawing;
using System.Collections.Generic;
using Cosmos.System;
using Epsilon.System;

namespace Epsilon.Applications.System.Setup
{
    public class PSetup : Process
    {
        Bitmap setupImg = new(Files.RawSetupImage);
        Checkbox cbox;
        Button next;
        string nButton = "Next";
        int currentPage = 0;
        static int ncWidth = GUI.dFont.Width * 9 + 2;
        string[] strings = {
@$"Welcome to the Epsilon setup wizard!
This tool is designed to assist you in installing Epsilon
onto your hard drive.
Please click the 'Next' button when you are ready to begin.

{new string('-', ncWidth)}

Important! Using this tool will erase all data on your hard drive.
Please back up any important data before proceeding. Use with caution.
",

@$"Please read the following terms and conditions carefully.
If you do not accept the terms and conditions, please exit the setup tool.

{new string('-', ncWidth)}

1. Introduction

    Welcome to Epsilon, an open-source operating system based on the
Cosmos kernel and developed by BrainBox Interactive. By using Epsilon,
you agree to comply with and be bound by the following terms and conditions.
Please review them carefully.

2. Company Information

    Company Name: BrainBox Interactive
    Company Address: TBD
",
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
                delegate() { NextPage(); }
            );
            cbox = new(24, 445,
                "I accept the terms and conditions", false);
            currentPage = 0;
            ProcessStrings();
        }

        private void NextPage()
        {
            if (!clicked)
            {
                switch (currentPage)
                {
                    default:
                        if (!clicked
                            && currentPage < strings.Length - 1)
                        {
                            currentPage++;
                            ProcessStrings();
                            ESystem.PlayAudio(Files.RawPageTurnAudio);
                        }
                        break;

                    case 1:
                        string s = "Please accept the terms and conditions.";
                        if (cbox.Checked && !clicked
                            && currentPage < strings.Length - 1)
                        {
                            currentPage++;
                            ProcessStrings();
                            ESystem.PlayAudio(Files.RawPageTurnAudio);
                        }
                        else if (!clicked && !cbox.Checked
                            && currentPage < strings.Length - 1)
                            Manager.Start(new MessageBox
                            {
                                wData =
                                {
                                    Position = new(GUI.width / 2 - s.Length * GUI.dFont.Width / 2,
                                    GUI.height / 2 - (32 + GUI.dFont.Height) / 2, s.Length * GUI.dFont.Width + 16,
                                    32 + GUI.dFont.Height*2),
                                    Moveable = true,
                                },
                                Button = true,
                                Content = s,
                                Name = "License Agreement",
                                Special = true
                            });
                        break;
                }
            }
            clicked = true;
        }

        int ofs = GUI.dFont.Height + 2;
        List<string> stringsToDraw = new List<string>();
        private void ProcessStrings()
        {
            stringsToDraw.Clear();
            stringsToDraw.Add(string.Empty);
            int lineIndex = 0;

            foreach (char c in strings[currentPage])
                if (c == '\n') {
                    lineIndex++;
                    stringsToDraw.Add(string.Empty);
                }
                else if (AcceptedCharacters.Contains(c))
                    stringsToDraw[lineIndex] += c;
        }
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
            GUI.canv.DrawString(Kernel._fps.ToString(),
                GUI.dFont, Color.White, 0, 0);
            GUI.canv.DrawString(currentPage.ToString(),
                GUI.dFont, Color.White, 0, GUI.dFont.Height);

            if (strings.Length > currentPage)
                for (int i = 0; i < stringsToDraw.Count; i++)
                    if (!string.IsNullOrWhiteSpace(stringsToDraw[i]))
                        GUI.canv.DrawString(stringsToDraw[i],
                            GUI.dFont, Color.White, 24,
                            64 + 8 + ofs * i);
            next.Update();
            
            switch (currentPage)
            {
                case 1:
                    cbox.Update();
                    break;
            }

            // TODO: stuff
        }
    }
}
