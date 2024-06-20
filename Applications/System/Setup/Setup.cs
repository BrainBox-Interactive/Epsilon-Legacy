using Epsilon.Interface;
using Epsilon.System.Critical.Processing;
using Cosmos.System.Graphics;
using Epsilon.System.Resources;
using Epsilon.Interface.Components;
using System.Drawing;
using System.Collections.Generic;
using Cosmos.System;
using Epsilon.System;
using Epsilon.Interface.Components.Text;

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
",
@"Page 3",
@"Page 4"
        };
        string AcceptedCharacters
            = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_-\"\' .,;:!?$*&()=";
        bool clicked = false;
        string tc =
$@"The Epsilon Project License - Last updated: 21/06/2024.

------------------------------------------------------------------

1. Introduction

    Welcome to Epsilon, an open-source operating system based on the
Cosmos kernel and developed by BrainBox Interactive. By using Epsilon,
you agree to comply with and be bound by the following terms and
conditions.
Please review them carefully.

------------------------------------------------------------------

2. Company Information

    Company Name: BrainBox Interactive
    Company Address: TBD

------------------------------------------------------------------

3. Description of Epsilon

    Epsilon is an open-source operating system designed to be fast
and efficient. It is currently in its very early stages of
development. Epsilon is built on the Cosmos kernel and is
available for use under the terms set forth in this document.

------------------------------------------------------------------

4. License and Use

    Epsilon is free to use for both personal and commercial purposes.
By using Epsilon, you agree to the following conditions:

    - You may not sell Epsilon or any derivative works for a price.
    - You must not engage in any activities that infringe
on the copyright of Epsilon or BrainBox Interactive.

------------------------------------------------------------------

5. Data Collection and Privacy

    Epsilon does not collect any user data and will never do so.
Your privacy is of utmost importance to us, and we ensure that
no personal data is collected, stored, or transmitted through
the use of Epsilon.

------------------------------------------------------------------

6. Updates and Patches

    Updates and patches for Epsilon will be handled through a
specific tool called the ""Epsilon Management Center"".
This tool will download updates and patches from a GitHub
repository to ensure that you always have the latest
version of Epsilon.

------------------------------------------------------------------

7. Support

    While Epsilon is in its early stages, BrainBox Interactive
does not guarantee official customer support. However,
users can seek help and support through the community
forums and the GitHub repository where Epsilon is hosted.

------------------------------------------------------------------

8. Liability and Disclaimers

    Epsilon is provided ""as is,"" without any warranties or
guarantees of any kind. BrainBox Interactive is not liable
for any damages, losses, or issues that may arise from the
use of Epsilon. This includes, but is not limited to, data
loss, hardware damage, or any other issues.

------------------------------------------------------------------

9. Governing Law

    These terms and conditions are governed by the laws of the
jurisdiction where BrainBox Interactive is based.
Any disputes arising from the use of Epsilon will be subject
to the exclusive jurisdiction of the courts in that jurisdiction.

------------------------------------------------------------------

10. Miscellaneous

    These terms and conditions constitute the entire agreement
between you and BrainBox Interactive regarding the use of Epsilon.
    If any provision of these terms and conditions is found
to be invalid or unenforceable, the remaining provisions will
continue to be valid and enforceable.
    BrainBox Interactive's failure to enforce any right or
provision in these terms and conditions will not constitute a
waiver of such right or provision unless acknowledged and agreed
to by BrainBox Interactive in writing.

------------------------------------------------------------------

By using Epsilon, you acknowledge that you have read,
understood, and agree to be bound by these terms and conditions.
";
        Scrollbox sbox;

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
            sbox = new(24, 54 + GUI.dFont.Height + 8 * 2 + 16 * 3,
                GUI.width - (24 * 2), 208, tc);
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
                                    GUI.height / 2 - (32 + GUI.dFont.Height) / 2,
                                    s.Length * GUI.dFont.Width + 16,
                                    32 + GUI.dFont.Height * 2 + 8),
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
                    sbox.Update();
                    break;
            }

            // TODO: stuff
        }
    }
}
