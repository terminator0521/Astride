using Astride.Input;
using System.Numerics;
using System.Reflection.Metadata;
using ZeroElectric.Vinculum;

namespace Astride.Core.Entites
{


    internal class PlayerEntity : Entity, IInputHandler
    {
        private Camera2D _cam;
        private float _maxSpeed = 600f;

        internal PlayerEntity() : base(64, 64)
        {
            speed = 25f;
        }

        public void InputUpdate()
        {
            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
            {
                displacement += Vector2.Normalize(Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), _cam) - CenterPos) * -speed;
            }
        }

        internal override void Render()
        {
            base.Render();
            Raylib.DrawLineEx(Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), _cam), CenterPos, 10, Raylib.RED);
        }

        internal void Update(in Camera2D cam)
        {
            //update camera
            this._cam = cam;

            //limit speed
            if (displacement.Length() > _maxSpeed)
                displacement = Vector2.Normalize(displacement) * _maxSpeed;


            //base updates
            base.Update();
        }

        internal override void OnCollision(params Entity[] entities)
        {
            foreach (Entity e in entities)
            {
                
            }
        }
    }
}
