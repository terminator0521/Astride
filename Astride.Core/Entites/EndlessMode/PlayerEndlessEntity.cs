using Astride.Input;
using System.Numerics;
using ZeroElectric.Vinculum;

namespace Astride.Core.Entites.EndlessMode
{


    internal class PlayerEndlessEntity : Entity, IInputHandler
    {
        private Camera2D _cam;

        //max player speed
        private float _maxSpeed = 500f;

        //player lives
        internal int Health { get; private set; }


        //player displacement getter for scene
        internal Vector2 Displacement
        {
            get
            {
                return displacement;
            }
        }

        internal PlayerEndlessEntity() : base(64, 64)
        {
            speed = 100f;
            Health = 1;
        }

        public void InputUpdate()
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
            {
                displacement += Vector2.Negate(Vector2.Normalize(Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), _cam) - CenterPos) * speed);
            }
        }

        internal override void Render()
        {
            base.Render();
            Raylib.DrawLineEx(Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), _cam), CenterPos, 10, Raylib.RED);
        }

        internal new void Update(in Camera2D cam)
        {
            Console.WriteLine(displacement);
            this._cam = cam;

            if (displacement.Length() > _maxSpeed)
                displacement = RayMath.Vector2Normalize(displacement) * _maxSpeed;

            base.Update(Raylib.GetFrameTime());
        }

        internal override void OnCollision(params Entity[] entities)
        {
            Console.WriteLine("Player @: " + pos);

            foreach (Entity e in entities)
            {
                if (e is AstroidEntity)
                {
                    Health--;
                }
                else if (e is BlackHoleEntity)
                {
                    Health = 0;
                }
            }
        }
    }
}
