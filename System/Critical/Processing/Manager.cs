using Epsilon.Interface;
using Epsilon.Interface.System.Shell.Screen;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.System.Critical.Processing
{
    public static class Manager
    {
        public static List<Process> pList
            = new List<Process>();
        public static Process toUpdate;

        public static List<Process> spList
            = new List<Process>();
        static List<Process> importantPList
            = new List<Process>();
        public static void Update()
        {
            spList = new List<Process>();
            foreach (Process p in pList)
                if ((p != GUI.cProc || p != toUpdate)
                    && !p.Special) p.Run();
                else if ((p != GUI.cProc || p != toUpdate)
                    && p.Special) spList.Add(p);
            if (GUI.cProc != null && pList.Contains(GUI.cProc))
            {
                toUpdate = GUI.cProc;
                GUI.cProc.Run();
            }
            else if (toUpdate != null
                && pList.Contains(toUpdate))
                toUpdate.Run();
            else toUpdate = null;
            foreach (Process sp in spList)
                sp.Run();
            foreach (Process p in importantPList)
                p.Run();
        }

        public static void Start(Process p)
        {
            if (p.Name == "Control Bar"
                || p.Name == "Top Bar")
                importantPList.Add(p);
            else pList.Add(p);
            toUpdate = p;
            p.Start();
        }

        public static void Stop(Process p)
            => pList.Remove(p);

        public static bool IsRunning(string pName)
        {
            foreach (Process p in pList)
                if (p.Name.ToLower() == pName.ToLower())
                    return true;
            return false;
        }

        public static bool IsFrontTU(Process p)
        {
            if (toUpdate == p
                || toUpdate == null) return false;
            if (GUI.mx >= toUpdate.wData.Position.X
                && GUI.mx <= toUpdate.wData.Position.X + toUpdate.wData.Position.Width
                && GUI.my >= toUpdate.wData.Position.Y
                && GUI.my <= toUpdate.wData.Position.Y + toUpdate.wData.Position.Height)
                return true;
            return false;
        }
    }
}
