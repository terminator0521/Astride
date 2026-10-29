using System.Numerics;
using ZeroElectric.Vinculum;

namespace Astride.Core.Entites.EndlessMode
{
    internal class AstronautEntity : Entity
    {
        private Rectangle _bounds; //enabled bounds for Astronaut
        private bool _canDestroy = false; //can be destroyed
        readonly private static float[] s_respawnTime = { 1f }; //respawn times
        private float _currentWaitTime = 1f; //wait timer
        private Vector2 _startPosOffset; //offset from player pos


        /// <summary>
        /// create an astronaut entity
        /// </summary>
        public AstronautEntity(in Vector2 startPosOffset, in Vector2 playerPos) : base(64, 64, playerPos + startPosOffset) 
        {
            Console.WriteLine(startPosOffset);
            _startPosOffset = startPosOffset;
            UpdateTexture("Assets/Textures/Astronaut.png");
        }

        internal override void OnCollision(Entity e)
        {
            if (IsEnabled) //if astroid is enabled
            {
                if (e is PlayerEndlessEntity || e is BlackHoleEntity) //disable if collided with blackhole or player
                {
                    IsEnabled = false; //disable
                    pos = new Vector2(-2000); //move offscreen
                }

                if (Raylib.CheckCollisionRecs(HitBox, _bounds) && !_canDestroy) //check if Astronaut is within active bounds
                {
                    _canDestroy = true; //enable can destory
                }
                else if (!Raylib.CheckCollisionRecs(HitBox, _bounds) && _canDestroy) //check if astroid is out of bounds and can be destroyed
                {
                    IsEnabled = false; //disable astroid
                }
            }
        }

        internal new void Update(in Vector2 playerPos, in Rectangle camRect)
        {
            _bounds = camRect; //update bounds to camera area

            if (!IsEnabled) //if astroid is disabled
            {
                if (_currentWaitTime > 0) //if still waiting
                {
                    _currentWaitTime -= Raylib.GetFrameTime(); //subtract wait time by frame time
                }
                else
                {
                    if ((float)Random.Shared.Next(64) % 2 == 0) // 50/50 chance of spawning
                    {
                        pos = playerPos + _startPosOffset; //reset pos
                        IsEnabled = true; //enable
                        _canDestroy = false; //disable can destroy
                        displacement *= speed; //change displacement by factor of speed
                    }
                    _currentWaitTime = RandomIndex(in s_respawnTime); //randomize respawn time
                }
            }

            //run parent update
            base.Update(Raylib.GetFrameTime());
        }

        internal override void Render()
        {
            //render if enabled
            if(IsEnabled)
                base.Render();
        }
    }
}
