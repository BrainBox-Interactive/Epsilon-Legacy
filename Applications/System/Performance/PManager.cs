using Cosmos.Core;
using Cosmos.System;
using Epsilon.Interface;
using Epsilon.Interface.Components;
using Epsilon.Interface.Components.Text;
using Epsilon.System.Critical.Processing;
using System.Collections.Generic;
using System.Drawing;

namespace Epsilon.Applications.System.Performance
{
    public class PManager : Process
    {
        ulong fs, s;
        Window win;
        private const uint div = 1048576;

        public override void Start()
        {
            base.Start();
            win = new(); win.StartAPI(this);
            fs = CPU.GetAmountOfRAM();
            s = (CPU.GetEndOfKernel() + 1024) / div;
            wData.Position.Width = 400;
            wData.Position.Height = 400;
        }

        int ofs = GUI.dFont.Height + 2,
            bw = 84;
        List<Hyperlink> hl = new();
        bool clicked = false;
        public override void Run()
        {
            base.Run();
            win.DrawB(this); win.DrawT(this, true);

            fs = CPU.GetAmountOfRAM();
            s = (CPU.GetEndOfKernel() + 1024) / div;
            GUI.canv.DrawString("RAM usage: " + $"{s} MB" + " / " + $"{fs} MB",
                GUI.dFont, GUI.colors.txtColor,
                wData.Position.X + 8, wData.Position.Y + 4 + win.tSize);
            GUI.canv.DrawString(CPU.GetCPUBrandString(),
                GUI.dFont, GUI.colors.txtColor, wData.Position.X + 8,
                wData.Position.Y + 4 + win.tSize + ofs);
            GUI.canv.DrawString("Uptime: " + (CPU.GetCPUUptime() / (10000 * 10000)).ToString(),
                GUI.dFont, GUI.colors.txtColor, wData.Position.X + 8,
                wData.Position.Y + 4 + win.tSize + ofs * 2);

            for (int i = 0; i < Manager.pList.Count; i++)
            {
                if (!hl.Exists(b => b.Text == Manager.pList[i].Name))
                    hl.Add(new(wData.Position.X + 8, wData.Position.Y + 4 + win.tSize + ofs * (i + 4),
                    Color.LightGray, Color.LightGray, Manager.pList[i].Name,
                    delegate () { if (Manager.IsRunning(Manager.pList[i].Name))
                            Manager.Stop(Manager.pList[i]);
                        hl.Remove(hl.Find(b => b.Text == Manager.pList[i].Name)); }, this));
            }

            foreach (Hyperlink h in hl)
            {
                if (!Manager.IsRunning(h.Text))
                {
                    hl.Remove(h);
                    continue;
                }
                h.X = wData.Position.X + 8;
                h.Y = wData.Position.Y + 4 + win.tSize
                    + ofs * (hl.IndexOf(h) + 4);
                h.Update();
                if (h.CheckHover() && MouseManager.MouseState == MouseState.Left
                    && Manager.IsRunning(h.Text) && !GUI.clicked && !clicked)
                {
                    Manager.Stop(Manager.pList.Find(p => p.Name == h.Text));
                    hl.Remove(h);
                    clicked = true;
                }
            }

            if (MouseManager.MouseState == MouseState.None
                && clicked)
                clicked = false;
        }
    }
}
