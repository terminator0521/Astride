using System.Numerics;
using System.Security.Cryptography;
using ZeroElectric.Vinculum;

namespace Astride.Core.Entites.EndlessMode
{
    internal class AstroidEntity : Entity
    {
        internal bool IsEnabled { get; private set; } = false;
        private Vector2 _startPos;
        private bool _canDestroy = false;
        private Vector2 _target;
        readonly private static float[] s_respawnTime = { 1f, 2f, 3f };
        readonly private static float[] s_speeds = { 200f, 250f, 150f };
        private float _currentWaitTime = 0;
        private Rectangle _gameBounds;

        internal AstroidEntity(in Vector2 startPos) : base(24, 24, new Vector2(600, 600))
        {
            _startPos = startPos;
            pos = startPos;
            _currentWaitTime = 0;
            speed = RandomIndex(in s_speeds);
        }

        internal new void Update(in Vector2 playerPos, in Vector2 dis, in Rectangle bounds)
        {
            if (IsEnabled)
            {
                if (Raylib.CheckCollisionRecs(dest, bounds) && !_canDestroy)
                {
                    _canDestroy = true;
                }
                else if (!Raylib.CheckCollisionRecs(dest, bounds) && _canDestroy)
                {
                    IsEnabled = false;
                }

                _target -= dis * Raylib.GetFrameTime(); //target of for direction

                displacement = RayMath.Vector2Normalize((_target / Raylib.GetFrameTime()) - displacement) * speed * Raylib.GetFrameTime(); // update displacement based on target
                displacement -= (dis * Raylib.GetFrameTime()); //adjust displacement by player speed

                //predict player's future pos
                //displacement += displacement * Raylib.GetFrameTime();

                base.Update(1f);
            }
            else
            {
                if (_currentWaitTime > 0)
                {
                    _currentWaitTime -= Raylib.GetFrameTime();
                }
                else
                {
                    pos = playerPos + _startPos; //return to start pos
                    //(work on adjusting based on player vel)
                    displacement = playerPos - _startPos; //head towards players last pos 
                    _target = (playerPos - pos); //set target
                    _currentWaitTime = RandomIndex(s_respawnTime); //randomize respawn time
                    speed = RandomIndex(in s_speeds); //choose speed
                    IsEnabled = true; //enable
                    _canDestroy = false;
                }
            }

        }

        internal override void Render()
        {
            if (IsEnabled)
            {
                base.Render();
            }
            Raylib.DrawCircleV(_target, 10, Raylib.RED);
            Console.WriteLine(_target);
        }

        internal override void OnCollision(params Entity[] entities)
        {
            if (IsEnabled)
            {
                foreach (var e in entities)
                {
                    if (e is PlayerEndlessEntity)
                    {
                        IsEnabled = false;
                    }
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
            return array[RandomNumberGenerator.GetInt32(array.Length)];
        }

    }
}
