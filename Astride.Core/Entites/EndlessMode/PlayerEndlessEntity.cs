using Astride.Input;
using System.Numerics;
using ZeroElectric.Vinculum;

namespace Astride.Core.Entites.EndlessMode
{


    internal class PlayerEndlessEntity : Entity, IInputHandler
    {
        
        //scene camera reference
        private Camera2D _cam;

        //player speeds
        private float _speedA = 75f; //set player velocity increase per click

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

        /// <summary>
        /// Create a player
        /// </summary>
        internal PlayerEndlessEntity() : base(64, 64, new(0, 0))
        {
            speed = 250f; //set player max speed
            Health = 1; //set player health
            UpdateTexture("Assets/Textures/Player.png"); //init player texture and texture parsing rect
        }

        public void InputUpdate()
        {
            //on LMB clicked
            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
            {
                //change player velocity with speed towards opposite direction of mouse
                displacement += Vector2.Negate(Vector2.Normalize(Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), _cam) - CenterPos) * _speedA); 
            }
        }

        internal override void Render()
        {
            base.Render(); //render texture
        }

        internal new void Update(in Camera2D cam)
        {
            this._cam = cam; //update player instance camera

            //limit speed to max speed
            if (displacement.Length() > speed)
                displacement = RayMath.Vector2Normalize(displacement) * speed;

            base.Update(Raylib.GetFrameTime()); //run parent updates at deltatime
        }

        internal override void OnCollision(Entity e)
        {
            if (e is AstroidEntity) //remove health by one if collided with astroid
            {
                Health--;
            }
            else if (e is BlackHoleEntity) //instant dealth when collided with blackhole
            {
                Health = 0;
            }
        }
    }
}
