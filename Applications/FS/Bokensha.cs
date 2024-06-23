using Cosmos.System;
using Epsilon.Applications.Base;
using Epsilon.Applications.System;
using Epsilon.Interface;
using Epsilon.Interface.Components.Text;
using Epsilon.System.Critical.Processing;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Epsilon.Applications.FS
{
    public class Bokensha : Process
    {
        public string Path { get; set; }
        public Bokensha(string path)
            => Path = path;

        public List<string>
            DirectoryStruct = new(),
            FileStruct = new();
        Window win;
        public override void Start()
        {
            base.Start();
            foreach (string s in Directory.GetDirectories(Path))
                DirectoryStruct.Add(s);
            foreach (string s in Directory.GetFiles(Path))
                FileStruct.Add(s);
            win = new();
            int tItems = DirectoryStruct.Count + FileStruct.Count;
            wData.Position.Height = tItems * GUI.dFont.Height
                + (2 * ofs) + 3*2;
            wData.Position.Width = 200;
            win.StartAPI(this);
        }

        int index = 0, i2 = 0,
            ofs = GUI.dFont.Height + 2;
        List<Hyperlink> fhl = new(),
            dhl = new();
        string dPath = string.Empty;
        bool clicked = false;
        public override void Run()
        {
            base.Run();
            win.DrawB(this); win.DrawT(this);

            index = 0;
            i2 = 0;
            foreach (string s in DirectoryStruct)
            {
                if (!dhl.Exists(b => b.Text == s))
                    dhl.Add(new(wData.Position.X,
                        wData.Position.Y + win.tSize + ofs * index,
                        Color.LightGray, Color.LightGray, s, delegate ()
                        {
                            dPath = Path;
                            if (!dPath.EndsWith('\\')) dPath += '\\';
                            if (Directory.Exists(dPath + s)
                                && !clicked)
                                Manager.Start(new Bokensha(dPath + s)
                                {
                                    wData =
                                    {
                                        Position = new(wData.Position.X + wData.Position.Width / 4,
                                        wData.Position.Y + wData.Position.Height / 4, GUI.width, GUI.height),
                                        Moveable = true,
                                    },
                                    Name = s,
                                    Special = false
                                });
                            clicked = true;
                        }, this));
                else
                {
                    dhl[i2].X = wData.Position.X + 4;
                    dhl[i2].Y = wData.Position.Y + win.tSize + 3 + ofs * index;
                    dhl[i2].Update();
                }
                index++; i2++;
            }
            i2 = 0;
            foreach (string s in FileStruct)
            {
                if (!fhl.Exists(b => b.Text == s))
                    fhl.Add(new(wData.Position.X,
                        wData.Position.Y + win.tSize + ofs * index,
                        Color.White, Color.White, s, delegate ()
                        {
                            dPath = Path;
                            if (!dPath.EndsWith('\\')) dPath += '\\';
                            var a = s.Split('.');
                            //fhl.Find(b => b.Text == s).Text = dPath + s;
                            if ((a[1] == "txt"
                                || a[1] == "log"
                                || a[1] == "cfg")
                                && !clicked)
                                Manager.Start(new Notepad
                                {
                                    wData =
                                    {
                                        Position = new(wData.Position.X + wData.Position.Width / 4,
                                        wData.Position.Y + wData.Position.Height / 4, 400, 300),
                                        Moveable = true,
                                    },
                                    Special = false,
                                    Path = dPath + s
                                });
                            clicked = true;
                        }, this));
                else
                {
                    fhl[i2].X = wData.Position.X + 4;
                    fhl[i2].Y = wData.Position.Y + win.tSize + 3 + ofs * index;
                    fhl[i2].Update();
                }
                index++; i2++;
            }
            if (MouseManager.MouseState == MouseState.None
                && clicked) clicked = false;
        }
    }
}
