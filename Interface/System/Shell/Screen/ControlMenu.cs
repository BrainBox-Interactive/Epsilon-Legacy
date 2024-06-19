using Cosmos.System;
using Cosmos.System.Graphics;
using Epsilon.Applications.Base;
using Epsilon.Applications.System;
using Epsilon.Interface.Components;
using Epsilon.Interface.Components.Buttons;
using Epsilon.Interface.Components.Text;
using Epsilon.System;
using Epsilon.System.Critical.Processing;
using Epsilon.System.Resources;
using System;
using System.Drawing;

namespace Epsilon.Interface.System.Shell.Screen
{
    public class ProfileMenu : Process
    {
        Window win;
        Button lgo, shtd, rst;
        int div3;

        public override void Start()
        {
            base.Start();
            wData.Position.Width = 240;
            wData.Position.Height = 128;

            win = new();
            win.StartAPI(this);

            div3 = (wData.Position.Width - 6) / 3;
            lgo = new(wData.Position.X + 2, wData.Position.Y + win.tSize,
                div3, 24, GUI.colors.btColor, GUI.colors.bthColor, GUI.colors.btcColor,
                "Log off", delegate () { Remove();
                    try
                    {
                        foreach (Process p in Manager.pList)
                            if (p.Name == "Log into Epsilon")
                                return;
                        ESystem.LogOff(); 
                    }
                    catch (Exception ex)
                    {
                        Manager.Start(new MessageBox
                        {
                            wData = new WindowData
                            {
                                Position = new Rectangle(GUI.width / 2 -
                                (int)((ex.ToString().Length * GUI.dFont.Width + 16) / 2),
                                GUI.height / 2 - (int)(75 / 2) + wData.Position.Height + 32,
                                ex.ToString().Length * GUI.dFont.Width + 16, 75),
                                Moveable = true
                            },
                            Name = "Error",
                            Content = ex.ToString(),
                            Special = false,
                            Button = true
                        });
                    }
                });
            shtd = new(wData.Position.X + 2*2 + div3, wData.Position.Y + win.tSize,
                div3, 24, GUI.colors.btColor, GUI.colors.bthColor, GUI.colors.btcColor,
                "Shutdown", delegate () { Remove(); Power.Shutdown(); });
            rst = new(wData.Position.X + 2*3 + div3*2, wData.Position.Y + win.tSize,
                div3, 24, GUI.colors.btColor, GUI.colors.bthColor, GUI.colors.btcColor,
                "Restart", delegate () { Remove(); Power.Reboot(); });
        }

        public override void Run()
        {
            base.Run();
            win.DrawB(this); win.DrawT(this);

            lgo.X = wData.Position.X + 2;
            lgo.Y = wData.Position.Y + win.tSize + 4;
            lgo.Update();

            shtd.X = wData.Position.X + 2 + (div3 + 1);
            shtd.Y = wData.Position.Y + win.tSize + 4;
            shtd.Update();

            rst.X = wData.Position.X + 2 + (div3 + 1) * 2;
            rst.Y = wData.Position.Y + win.tSize + 4;
            rst.Update();
        }
    }

    public class ControlMenu : Process
    {
        int mts = 24, bsx = 96;
        Hyperlink settings;
        ProfilePicture picture;
        Button notepad, calculator;
        Bitmap banner = new(Files.RawMenuBanner);
        bool spawned = false;

        public override void Start()
        {
            base.Start();

            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            settings = new(
                x + w - ("Settings".Length * GUI.dFont.Width + 8),
                y + (mts / 2 - GUI.dFont.Height / 2),
                Color.LightGray,
                Color.LightGray,
                "Settings",
                delegate() { Remove(); }
            );
            picture = new(
                x, y, new Bitmap(Files.RawDefaultPFP),
                delegate ()
                {
                    if (!spawned)
                        Manager.Start(new ProfileMenu()
                        {
                            wData = new WindowData
                            {
                                Position = new Rectangle(GUI.width / 2 - 240 / 2,
                                GUI.height / 2 - 128 / 2, 240, 128),
                                Moveable = false
                            },
                            Name = "Profile Menu",
                            Special = true
                        });
                    spawned = true;
                    Remove();
                }
            );

            notepad = new(x + bsx + 8,
                y + mts + 8, (w - bsx)/2 - 8*2, 32,
                GUI.colors.btColor, GUI.colors.bthColor,
                GUI.colors.btcColor,
                "Notepad", delegate()
                {
                    if (!spawned)
                        Manager.Start(new Notepad()
                        {
                            wData = new WindowData
                            {
                                Position = new Rectangle(GUI.width / 2 - 400 / 2,
                                GUI.height / 2 - 300 / 2, 400, 300),
                                Moveable = true
                            },
                            Name = "Notepad",
                            Special = false
                        });
                    spawned = true;
                    Remove();
                });

            calculator = new(x + bsx + 8*2 + ((w - bsx) / 2 - 8 * 2),
                y + mts + 8, (w - bsx) / 2 - 8 * 2, 32,
                GUI.colors.btColor, GUI.colors.bthColor,
                GUI.colors.btcColor,
                "Calculator", delegate ()
                {
                    if (!spawned)
                        Manager.Start(new Calculator()
                        {
                            wData = new WindowData
                            {
                                Position = new Rectangle(GUI.width / 2 - (int)(151 / 2),
                                GUI.height / 2 - 200 / 2, 151, 200),
                                Moveable = true
                            },
                            Name = "Calculator",
                            Special = false
                        });
                    spawned = true;
                    Remove();
                });
        }

        public override void Run()
        {
            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            GUI.canv.DrawRectangle(
                GUI.colors.tboColor,
                x - 1, y - 1,
                w + 1, h + 1
            );

            GUI.canv.DrawFilledRectangle(
                GUI.colors.moColor,
                x, y,
                w, mts
            );
            GUI.canv.DrawFilledRectangle(
                GUI.colors.mColor,
                x + bsx, y + mts,
                w - bsx, h - mts
            );

            GUI.canv.DrawImage(
                banner, x, y + mts
            );
            GUI.canv.DrawLine(
                GUI.colors.moColor,
                x + (int)banner.Width, y + mts,
                x + (int)banner.Width,
                y + mts + (int)banner.Height
            );

            picture.X = x; picture.Y = y;
            picture.Update();

            GUI.canv.DrawString(
                ESystem.CurrentUser,
                GUI.dFont,
                GUI.colors.txtColor,
                x + mts + 8,
                y + (mts / 2 - GUI.dFont.Height / 2)
            );

            settings.X = x + w - ("Settings".Length * GUI.dFont.Width + 8);
            settings.Y = y + (mts / 2 - GUI.dFont.Height / 2);
            settings.Update();

            // Menu Program Entries
            notepad.X = x + bsx + 8 + 4; notepad.Y = y + mts + 8;
            notepad.Update();

            calculator.X = x + bsx + 8 * 2 + ((w - bsx) / 2 - 8 * 2) + 4;
            calculator.Y = y + mts + 8;
            calculator.Update();

            // if clicks off the menu
            if (GUI.mx < x
                || GUI.mx > x + w
                || GUI.my < y
                || GUI.my > y + h)
                if (MouseManager.MouseState == MouseState.Left
                    && !GUI.clicked)
                    Manager.pList.Remove(this);
        }
    }
}
