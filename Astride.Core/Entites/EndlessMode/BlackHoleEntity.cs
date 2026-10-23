using System.Numerics;
using ZeroElectric.Vinculum;

namespace Astride.Core.Entites.EndlessMode
{

    internal class BlackHoleEntity : Entity
    {
        public BlackHoleEntity(Vector2 gameSize, in Camera2D cam) : base(0, (int)gameSize.Y * 3, Raylib.GetScreenToWorld2D(Vector2.Zero, cam))
        {
            Console.WriteLine(gameSize);
            speed = 200f;
            //adjust dest and hitbox y pos manually
            pos.X -= gameSize.Y * 1.5f;
        }

        internal new void Update(in Vector2 playerDisplacement)
        {
            Console.WriteLine(pos);
            float change = speed - playerDisplacement.X;
            dest.Width += change * Raylib.GetFrameTime(); // change width over time
            dest.Y += playerDisplacement.Y * Raylib.GetFrameTime(); //maintain same Y level as player
            HitBox = dest; //sync hitbox with dest 
            HitBox = HitBox with
            {
                X = dest.X - origin.X,
                Y = dest.Y - origin.Y,
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
