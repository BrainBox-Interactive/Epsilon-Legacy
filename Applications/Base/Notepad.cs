using Cosmos.System;
using Epsilon.Applications.System;
using Epsilon.Interface;
using Epsilon.Interface.Components;
using Epsilon.Interface.Components.Text;
using Epsilon.System;
using Epsilon.System.Critical.Processing;
using System;
using System.Drawing;
using System.IO;

namespace Epsilon.Applications.Base
{
    public class Notepad : Process
    {
        public string Content = string.Empty,
            Path = string.Empty;
        Window w;
        Scrollbox sb;
        Box b;
        Button s, o;
        int ofs = 24;

        public override void Start()
        {
            base.Start();
            this.w = new();
            Name = "Notepad";
            wData.Position.Width = 400;
            wData.Position.Height = 300;
            if (Path != string.Empty)
                Content = File.ReadAllText(Path);

            int x = wData.Position.X, y = wData.Position.Y;
            int w = wData.Position.Width, h = wData.Position.Height;
            int fh = this.w.tSize + h;

            sb = new(x, y + ofs + this.w.tSize, w, h - ofs,
                "", this, true);
            if (!VMTools.IsVirtualBox) sb.ChangeContent(Content);

            b = new(x, y + this.w.tSize, w - 64 * 2, ofs - 1,
                Color.White, Color.Black, "Filepath", this);
            if (Path != string.Empty) b.Content = Path;
            s = new(x + w - 64, y + this.w.tSize - 1, 64, ofs,
                GUI.colors.btColor, GUI.colors.bthColor, GUI.colors.btcColor,
                "Save", this, delegate()
                {
                    try { ESystem.WriteFile(b.Content, sb.Content, true); }
                    catch (Exception ex)
                    {
                        Manager.Start(new MessageBox
                        {
                            wData =
                            {
                                Position = new(
                                    GUI.width / 2
                                    - (ex.ToString().Length * GUI.dFont.Width + 16) / 2,
                                    GUI.height / 2 - 75 / 2,
                                    (ex.ToString().Length * GUI.dFont.Width + 16), 75),
                                Icon = wData.Icon,
                                Moveable = true
                            },
                            Content = ex.ToString(),
                            Name = "Error",
                            Special = false,
                            Button = true
                        });
                    }
                });
            o = new(x + w - 64 * 2, y + this.w.tSize - 1, 64, ofs,
                GUI.colors.btColor, GUI.colors.bthColor, GUI.colors.btcColor,
                "Open", this, delegate () {
                    try { sb.ChangeContent(ESystem.ReadFile(b.Content)); }
                    catch (Exception ex)
                    {
                        Manager.Start(new MessageBox
                        {
                            wData =
                            {
                                Position = new(
                                    GUI.width / 2
                                    - (ex.ToString().Length * GUI.dFont.Width + 16) / 2,
                                    GUI.height / 2 - 75 / 2,
                                    (ex.ToString().Length * GUI.dFont.Width + 16), 75),
                                Icon = wData.Icon,
                                Moveable = true
                            },
                            Content = ex.ToString(),
                            Name = "Error",
                            Special = false,
                            Button = true
                        });
                    }
                });
            this.w.StartAPI(this);
        }

        public override void Run()
        {
            w.DrawB(this); w.DrawT(this);

            GUI.canv.DrawRectangle(
                GUI.colors.tboColor, wData.Position.X - 1,
                sb.Y + sb.Height - 1, sb.state.Length * GUI.dFont.Width + 16 + 1,
                24 + 1);
            GUI.canv.DrawFilledRectangle(
                GUI.colors.mColor, wData.Position.X,
                sb.Y + sb.Height, sb.state.Length * GUI.dFont.Width + 16,
                24);

            sb.X = wData.Position.X;
            sb.Y = wData.Position.Y + w.tSize + ofs;
            sb.Update();

            b.X = wData.Position.X;
            b.Y = wData.Position.Y + w.tSize;
            b.Update();
            s.X = wData.Position.X + wData.Position.Width - 64;
            s.Y = wData.Position.Y + w.tSize - 1;
            s.Update();
            o.X = wData.Position.X + wData.Position.Width - 64 * 2;
            o.Y = wData.Position.Y + w.tSize - 1;
            o.Update();
        }
    }
}
