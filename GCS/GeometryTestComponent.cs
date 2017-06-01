﻿using System.Linq;
using System.Collections.Generic;
using Grid.Framework;
using Grid.Framework.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GCS
{
    class GeometryTestComponent : Renderable
    {
        public override void Draw(SpriteBatch sb)
        {
            var v1 = new Circle(new Dot(30, 30), 200);
            var v2 = new Segment(new Dot(100,200), new Dot(200, 230));
            v1.Draw(sb);
            v2.Draw(sb);
            Vector2 v3 = (Camera.Current as Camera2D).GetRay(Mouse.GetState().Position.ToVector2());

            GUI.DrawPoint(sb, Geometry.GetNearest(v1, v3), 10f, Color.Blue);
            GUI.DrawPoint(sb, Geometry.GetNearest(v2, v3), 10f, Color.Red);
        }
    }
}
