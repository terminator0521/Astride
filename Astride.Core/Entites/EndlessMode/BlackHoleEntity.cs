using System.Numerics;
using ZeroElectric.Vinculum;

namespace Astride.Core.Entites.EndlessMode
{

    internal class BlackHoleEntity : Entity
    {
        private Vector2 _startPos;
        public BlackHoleEntity(Vector2 gameSize, in Camera2D cam) : base()
        {
            speed = 200f;
            _startPos = Raylib.GetScreenToWorld2D(Vector2.Zero, cam);
            dest = new Rectangle(_startPos.X, _startPos.Y, 0, gameSize.Y * 2);

        }

        internal new void Update(in Vector2 playerDisplacement)
        {

            float change = speed - playerDisplacement.X;
            dest.Width += change * Raylib.GetFrameTime();
            dest.Y += playerDisplacement.Y * Raylib.GetFrameTime();

            HitBox = HitBox with
            {
                X = dest.X + origin.X,
                Y = dest.Y + origin.Y,
                Width = dest.Width,
                Height = dest.Height
            };
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
