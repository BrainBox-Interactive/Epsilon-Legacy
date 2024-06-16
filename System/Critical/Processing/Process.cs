using Cosmos.System.Graphics;
using Epsilon.System.Resources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.System.Critical.Processing
{
    public class Process
    {
        public virtual void Run() { }
        public virtual void Start() { }
        public virtual void Remove()
            => Manager.pList.Remove(this);

        public string Name;
        public bool Special;
        public WindowData wData = new();
    }

    public class WindowData
    {
        public Rectangle Position 
            = new Rectangle {
                X = 0,
                Y = 0,
                Width = 100,
                Height = 100
        }; public Bitmap Icon = new(Files.RawDefaultIcon);
        public bool Moveable = true;
    }
}
