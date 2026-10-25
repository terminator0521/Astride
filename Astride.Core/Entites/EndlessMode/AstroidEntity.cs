using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using ZeroElectric.Vinculum;

namespace Astride.Core.Entites.EndlessMode
{
    internal class AstroidEntity : Entity
    {
        internal bool IsEnabled { get; private set; } = false; //if astroid is enabled
        private Vector2 _startPos; //start position 
        private bool _canDestroy = false; //can be destroyed
        private Vector2 _target; //astroid target spot
        readonly private static float[] s_respawnTime = { 1f, 2f, 3f }; //respawn times
        readonly private static float[] s_speeds = { 100f, 200f, 150f }; //travelling speeds
        private float _currentWaitTime = 0; //wait timer

        /// <summary>
        /// Creates an astroid
        /// </summary>
        /// <param name="startPos">Start position offset based on playerPos</param>
        /// <param name="playerPos">Position of player</param>
        internal AstroidEntity(in Vector2 startPos, in Vector2 playerPos) : base(24, 24, startPos + playerPos)
        {
            _startPos = startPos; //set start pos
            pos = _startPos; //set current pos
            _currentWaitTime = RandomIndex(s_respawnTime); //set random respawn time
            speed = RandomIndex(in s_speeds); //set random speed
        }

        /// <summary>
        /// Run astroid logic updates
        /// </summary>
        /// <param name="playerPos">Player position</param>
        /// <param name="dis">Player displacement</param>
        /// <param name="bounds">Active bounds</param>
        internal new void Update(in Vector2 playerPos, in Vector2 dis, in Rectangle bounds)
        {
            Console.WriteLine(playerPos);
            if (IsEnabled) //if astroid is enabled
            {
                if (Raylib.CheckCollisionRecs(dest, bounds) && !_canDestroy) //check if astroid is within active bounds
                {
                    _canDestroy = true; //enable can destory
                }
                else if (!Raylib.CheckCollisionRecs(dest, bounds) && _canDestroy) //check if astroid is out of bounds and can be destroyed
                {
                    IsEnabled = false; //disable astroid
                }

                _target -= dis * Raylib.GetFrameTime(); //target of for direction

                displacement = RayMath.Vector2Normalize((_target / Raylib.GetFrameTime()) - displacement) * speed * Raylib.GetFrameTime(); // update displacement based on target
                displacement -= dis * Raylib.GetFrameTime(); //adjust displacement by player speed


                base.Update(1f); //run parent updates
            }
            else //if astroid is disabled
            {
                if (_currentWaitTime > 0) //if still waiting
                {
                    _currentWaitTime -= Raylib.GetFrameTime(); //subtract wait time by frame time
                }
                else
                {
                    pos = playerPos + _startPos; //return to start pos
                    pos.Y -= (dis.Y * Raylib.GetFrameTime()); //adjust starting y pos based on player velocity
                    //(work on adjusting based on player vel)
                    displacement = playerPos - _startPos; //head towards players last pos 
                    _target = (playerPos - pos); //set target
                    _currentWaitTime = RandomIndex(s_respawnTime); //randomize respawn time
                    speed = RandomIndex(in s_speeds); //choose speed
                    IsEnabled = true; //enable
                    _canDestroy = false; //disable can destroy
                }
            }

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
            return array[RandomNumberGenerator.GetInt32(array.Length)]; //return random index in given array
        }
    }
}
