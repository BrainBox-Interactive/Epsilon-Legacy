using Epsilon.Interface;
using Epsilon.Interface.System.Shell.Screen;
using System;
using System.Collections.Generic;
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
        }

        public static void Start(Process p)
        {
            pList.Add(p);
            p.Start();
        }

        public static void Stop(Process p)
            => pList.Remove(p);
    }
}
