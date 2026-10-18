using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using ZeroElectric.Vinculum;

namespace Astride.Core.Entites
{
    internal class BlackHoleEntity : Entity
    {
        private Vector2 _disToPlayer = new Vector2();
        public BlackHoleEntity(Vector2 gameSize, in Camera2D cam) : base()
        {
            speed = 100f;
            Vector2 startPos = Raylib.GetScreenToWorld2D(Vector2.Zero, cam);
            dest = new Rectangle(startPos.X, startPos.Y, 0, gameSize.Y);
        }

        internal new void Update(in Vector2 playerDisplacement)
        {
            float change = speed - playerDisplacement.X;
            dest.Width += change * Raylib.GetFrameTime();
        }
        internal override void Render()
        {

            base.Render();
        }

        internal override void onCollision(Entity e)
        {
            
        }
    }
}
