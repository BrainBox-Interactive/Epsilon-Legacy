using Cosmos.System;
using Cosmos.System.Graphics;
using Epsilon.Interface.Components.Buttons;
using Epsilon.Interface.Components.Text;
using Epsilon.System;
using Epsilon.System.Critical.Processing;
using Epsilon.System.Resources;
using System.Drawing;

namespace Epsilon.Interface.System.Shell.Screen
{
    public class ControlMenu : Process
    {
        static int mts = 24, bsx = 96;
        static Hyperlink settings;
        static ProfilePicture picture;

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
                delegate () { Remove(); }
            );
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

            // PLACEHOLDER, WILL BE REPLACED WITH IMAGE
            GUI.canv.DrawFilledRectangle(
                GUI.colors.moColor,
                x, y + mts,
                bsx, h - mts
            );

            picture.X = x; picture.Y = y;
            picture.Update();

            GUI.canv.DrawString(
                ESystem.CurrentUser,
                GUI.dFont,
                GUI.colors.txtColor,
                x + mts + 12,
                y + (mts / 2 - GUI.dFont.Height / 2)
            );

            settings.X = x + w - ("Settings".Length * GUI.dFont.Width + 8);
            settings.Y = y + (mts / 2 - GUI.dFont.Height / 2);
            settings.Update();

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
