﻿using Cosmos.System;
using Cosmos.System.Graphics;
using Epsilon.System;
using System;
using System.Drawing;

namespace Epsilon.Interface.Components.Buttons
{
    public class ProfilePicture : Component
    {
        public Bitmap PFP { get; set; }
        public Action Action { get; set; }

        public ProfilePicture(int x, int y, Bitmap bitmap, Action action)
            : base(x, y, (int)bitmap.Width, (int)bitmap.Height)
        {
            X = x; Y = y;
            Width = (int)bitmap.Width; Height = (int)bitmap.Height;
            PFP = bitmap;
            Action = action;
        }

        public override void Update()
        {
            base.Update();

            GUI.canv.DrawImage(
                PFP, X, Y
            );

            if (CheckHover())
            {
                GUI.canv.DrawRectangle(
                    Color.DarkGray,
                    X, Y, Width, Height
                );

                GUI.crsChanged = true;
                GUI.crs = ESystem.hc;

                if (MouseManager.MouseState == MouseState.Left
                    && !GUI.clicked)
                    OnClick(0);
            }
            else GUI.crsChanged = false;
        }

        public override void OnClick(int mIndex)
        {
            base.OnClick(mIndex);
            Action.Invoke();
        }
    }
}