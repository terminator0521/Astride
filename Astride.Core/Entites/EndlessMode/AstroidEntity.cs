using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using ZeroElectric.Vinculum;

namespace Astride.Core.Entites.EndlessMode
{
    internal class AstroidEntity : Entity
    {
        internal bool IsEnabled { get; private set; } = false; //if astroid is enabled
        private Vector2 _offset; //start position offset from player
        private Vector2 _disAdj; //displacement adjustment;
        private bool _canDestroy = false; //can be destroyed
        readonly private static float[] s_respawnTime = {2f}; //respawn times
        readonly private static float[] s_speeds = { 200f, 175f, 150f}; //travelling speeds

        //random adjustments to astroid displacement
        readonly private static Vector2[] s_disAdjustments =
        {
            new Vector2(-1f, 0f),
            new Vector2(-1f, -0.1f),
            new Vector2(-1f, 0.1f),
            new Vector2(-1f, -0.2f),
            new Vector2(-1f, 0.2f),
            new Vector2(-1f, -0.3f),
            new Vector2(-1f, 0.3f),
        }; 

        private float _currentWaitTime = 0; //wait timer

        /// <summary>
        /// Creates an astroid
        /// </summary>
        /// <param name="offset">Start position offset based on playerPos</param>
        /// <param name="playerPos">Position of player</param>
        internal AstroidEntity(in Vector2 offset, in Vector2 playerPos) : base(24, 24, offset + playerPos)
        {
            _offset = offset; //set start pos offset
            _currentWaitTime = RandomIndex(s_respawnTime); //set random respawn time
            speed = RandomIndex(in s_speeds); //set random speed
        }

        /// <summary>
        /// Run astroid logic updates
        /// </summary>
        /// <param name="playerPos">Player position</param>
        /// <param name="bounds">Active bounds</param>
        internal new void Update(in Vector2 playerPos, in Vector2 playerDis, in Rectangle bounds)
        {
            if (IsEnabled) //if astroid is enabled
            {
                if (Raylib.CheckCollisionRecs(HitBox, bounds) && !_canDestroy) //check if astroid is within active bounds
                {
                    _canDestroy = true; //enable can destory
                }
                else if (!Raylib.CheckCollisionRecs(HitBox, bounds) && _canDestroy) //check if astroid is out of bounds and can be destroyed
                {
                    IsEnabled = false; //disable astroid
                }
            }
            else //if astroid is disabled
            {
                if (_currentWaitTime > 0) //if still waiting
                {
                    _currentWaitTime -= Raylib.GetFrameTime(); //subtract wait time by frame time
                }
                else
                {
                    pos = playerPos + _offset;
                    _currentWaitTime = RandomIndex(in s_respawnTime); //randomize respawn time
                    speed = RandomIndex(in s_speeds); //choose speed
                    IsEnabled = true; //enable
                    _canDestroy = false; //disable can destroy

                    displacement = -RayMath.Vector2Normalize(_offset); //get direction to player 
                    displacement += RayMath.Vector2Normalize(RandomIndex(s_disAdjustments)); //adjust with random direction
                    Console.WriteLine(displacement);
                    displacement *= speed; //change displacement by factor of speed
                    Console.WriteLine(displacement);

                }
            }

            base.Update(Raylib.GetFrameTime()); //update parent logic
        }

        internal override void Render()
        {
            if (IsEnabled) // if enabled
            {
                Raylib.DrawRectangleRec(dest, Raylib.SKYBLUE);
            }
        }

        internal override void OnCollision(Entity e)
        {
            if (IsEnabled) //if enabled
            {
                if (e is PlayerEndlessEntity)
                {
                    IsEnabled = false; //disable on collided with player
                }
            }
        }

        /// <summary>
        /// Returns a random index number of an array
        /// </summary>
        /// <typeparam name="A">Array type</typeparam>
        /// <param name="array">Array refernce</param>
        /// <returns></returns>
        private A RandomIndex<A>(in A[] array)
        {
            return array[Random.Shared.Next(array.Length)]; //return random index in given array
        }
    }
}
