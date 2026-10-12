using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using ZeroElectric.Vinculum;
using ZeroElectric.Vinculum.Extensions;
using Astride.Input;

namespace Astride.Core.Entites
{
    internal class TestEntity : Entity, IInputHandler
    {
        internal TestEntity() : base(64, 64)
        {
            speed = 200f;
            pos = new Vector2(400, 300);
        }

        internal override void Update()
        {
            base.Update();
        }

        internal override void Render()
        {
            base.Render();
        }

        public void InputUpdate()
        {
            displacement = Vector2.Zero;

            displacement.X += (Raylib.IsKeyDown(KeyboardKey.KEY_A) ? -1 : 0) + (Raylib.IsKeyDown(KeyboardKey.KEY_D) ? 1: 0);
            displacement.Y += (Raylib.IsKeyDown(KeyboardKey.KEY_S) ? 1 : 0) + (Raylib.IsKeyDown(KeyboardKey.KEY_W) ? -1 : 0);

            displacement = RayMath.Vector2Normalize(displacement) * speed;
        }
    }
}
