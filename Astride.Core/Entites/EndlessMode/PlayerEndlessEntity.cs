using Astride.Input;
using System.Numerics;
using ZeroElectric.Vinculum;

namespace Astride.Core.Entites.EndlessMode
{


    internal class PlayerEndlessEntity : Entity, IInputHandler
    {
        private Camera2D _cam;

        //player speeds
        private float _speedA = 75f;

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

        internal PlayerEndlessEntity() : base(64, 64, new(0, 0))
        {
            speed = 250f;
            Health = 1;
        }

        public void InputUpdate()
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
            {
                displacement += Vector2.Negate(Vector2.Normalize(Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), _cam) - CenterPos) * _speedA);
            }
        }

        internal override void Render()
        {
            base.Render();
            Raylib.DrawCircleV(pos, 10, Raylib.BLUE);
        }

        internal new void Update(in Camera2D cam)
        {
            Console.WriteLine(pos);
            this._cam = cam;

            if (displacement.Length() > speed)
                displacement = RayMath.Vector2Normalize(displacement) * speed;

            base.Update(Raylib.GetFrameTime());
        }

        internal override void OnCollision(params Entity[] entities)
        {

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
