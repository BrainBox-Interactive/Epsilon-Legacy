using Epsilon.System.Critical.Processing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.Interface.Components.Buttons
{
    public class OKButton : Button
    {
        Process p;

        public OKButton(int x, int y, Process process) : base(x, y,
            32, 16,
            GUI.colors.btColor, GUI.colors.bthColor,
            "OK"
        ) {
            p = process;
        }

        public override void OnClick(int mIndex)
        {
            if (mIndex == 0
                && this != null)
                p.Remove();
        }
    }
}
