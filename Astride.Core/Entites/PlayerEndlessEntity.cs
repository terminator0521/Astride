using Astride.Input;
using System.Numerics;
using ZeroElectric.Vinculum;

namespace Astride.Core.Entites
{


    internal class PlayerEndlessEntity : Entity, IInputHandler
    {
        private Camera2D cam;
        private float maxSpeed = 600f;



        internal PlayerEndlessEntity() : base(64, 64)
        {
            speed = 25f;
        }

        public void InputUpdate()
        {
            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
            {
                displacement += Vector2.Normalize(Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), cam) - CenterPos) * speed;
                Console.WriteLine(displacement.Length());
            }
        }

        internal override void Render()
        {
            base.Render();
            Raylib.DrawLineEx(Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), cam), CenterPos, 10, Raylib.RED);
        }

        internal new void Update(in Camera2D cam)
        {
            //limit speed
            if (displacement.Length() > maxSpeed)
                displacement = Vector2.Normalize(displacement) * maxSpeed;

            base.Update();
        }
    }
}
