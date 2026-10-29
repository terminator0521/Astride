using Astride.Input;
using System.Numerics;
using ZeroElectric.Vinculum;

namespace Astride.Core.Entites.EndlessMode
{
    internal class PlayerEndlessEntity : Entity, IInputHandler
    {
        //player score
        internal int Score { get; private set; }

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

        //player projectile
        internal ProjectileEntity proj;

        /// <summary>
        /// Create a player
        /// </summary>
        internal PlayerEndlessEntity() : base(64, 64, new(0, 0))
        {
            speed = 250f; //set player max speed
            Health = 3; //set player health
            UpdateTexture("Assets/Textures/Player.png"); //init player texture and texture parsing rect
            proj = new ProjectileEntity(24f); //init projectile

        }

        public void InputUpdate()
        {
            //on LMB clicked & projectile is disabled
            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT) && !proj.IsEnabled)
            {
                //shoot projectile
                proj.Shoot(Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), _cam) - pos, pos);

                //change player velocity with speed towards opposite direction of mouse
                displacement += Vector2.Negate(Vector2.Normalize(Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), _cam) - CenterPos) * _speedA);
            }

        }

        internal override void Render()
        {

            base.Render(); //render texture
            Raylib.DrawCircleV(Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), _cam), 5, Raylib.RED); //draw red dot on player mouse pos
        }

        internal new void Update(in Camera2D cam)
        {
            //add 5 to score if project hits an astroid
            Score += proj.HitAstroid ? 5 : 0;

            this._cam = cam; //update player instance camera
            angle = RayMath.Vector2LineAngle(pos, Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), cam)) * -RayMath.RAD2DEG - 90; //set angle

            //limit speed to max speed
            if (displacement.Length() > speed)
                displacement = RayMath.Vector2Normalize(displacement) * speed;

            //update projectile

            base.Update(Raylib.GetFrameTime()); //run parent updates at deltatime
        }

        internal override void OnCollision(Entity e)
        {
            if (e is AstroidEntity) //remove health by one if collided with enabled astroid
            {
                Health--;
            }
            else if (e is BlackHoleEntity) //instant dealth when collided with blackhole
            {
                Health = 0;
            }
            else if (e is AstronautEntity)
            {
                Score += 10;
            }
        }
    }
}
