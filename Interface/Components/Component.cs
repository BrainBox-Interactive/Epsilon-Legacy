using Cosmos.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epsilon.Interface.Components
{
    public class Component
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Component(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public virtual void Update()
        {
            if (GUI.mx >= X
                && GUI.mx <= X + Width
                && GUI.my >= Y
                && GUI.my <= Y + Height)
                OnHover();

            if (MouseManager.MouseState == MouseState.Left
                    && !GUI.clicked
                    && CheckHover())
                OnClick(0);
            else if (MouseManager.MouseState == MouseState.Right
                    && !GUI.clicked
                    && CheckHover())
                OnClick(1);

            if (MouseManager.ScrollDelta != 0)
                OnScroll(MouseManager.ScrollDelta);

            // then, base.Update() for components
        }

        public virtual void OnHover() { }
        public virtual void OnClick(int mIndex) { }
        public virtual void OnScroll(int delta) { }

        public virtual bool CheckHover()
        {
            if (GUI.mx >= X
                && GUI.mx <= X + Width
                && GUI.my >= Y
                && GUI.my <= Y + Height)
                return true;
            return false;
        }
    }
}
