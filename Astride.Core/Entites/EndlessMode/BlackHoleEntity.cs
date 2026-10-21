using System.Numerics;
using ZeroElectric.Vinculum;

namespace Astride.Core.Entites.EndlessMode
{
    internal class BlackHoleEntity : Entity
    {
        public BlackHoleEntity(Vector2 gameSize, in Camera2D cam) : base()
        {
            speed = 300f;
            Vector2 startPos = Raylib.GetScreenToWorld2D(Vector2.Zero, cam);
            dest = new Rectangle(startPos.X, startPos.Y, 0, gameSize.Y * 2);

        }

        internal new void Update(in Vector2 playerDisplacement)
        {
            float change = speed - playerDisplacement.X;
            dest.Width += change * Raylib.GetFrameTime();
            dest.Y += playerDisplacement.Y * Raylib.GetFrameTime();
        }
        internal override void Render()
        {
            base.Render();
        }

        internal override void OnCollision(params Entity[] entities)
        {

        }
    }
}
