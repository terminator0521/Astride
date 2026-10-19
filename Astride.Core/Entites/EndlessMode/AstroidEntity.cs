using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using ZeroElectric.Vinculum;

namespace Astride.Core.Entites.EndlessMode
{
    internal class AstroidEntity : Entity
    {
        public bool IsEnabled { get; private set; } = false;
        private Vector2 _startPos;
        private static float[] s_respawnTime = { 1f, 2f, 3f };
        private float _currentWaitTime = 0;



        internal AstroidEntity(Vector2 startPos) : base(24, 24)
        {
            _startPos = startPos;
            _currentWaitTime = 0;
            speed = 200f;
        }

        internal new void Update(in Vector2 playerPos, in Vector2 dis)
        {
            Console.WriteLine(pos);
            if (IsEnabled)
            {
                displacement = Vector2.Normalize(displacement + -dis) * speed; //adjust displacement with speed and player relative displacement
                base.Update();
            }
            else
            {
                if (_currentWaitTime > 0)
                {
                    _currentWaitTime -= Raylib.GetFrameTime();
                }
                else
                {
                    pos = _startPos; //return to start pos
                    displacement = playerPos - _startPos; //head towards players last pos
                    _currentWaitTime = s_respawnTime[RandomNumberGenerator.GetInt32(3)]; //randomize respawn time
                    IsEnabled = true; //enable
                }
            }

        }

        internal override void Render()
        {
            if (IsEnabled)
            {
                base.Render();
            }
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

    }
}
