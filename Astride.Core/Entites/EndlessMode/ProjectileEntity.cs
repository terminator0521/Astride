using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Reflection;
using System.Text;
using ZeroElectric.Vinculum;

namespace Astride.Core.Entites.EndlessMode
{

    internal class ProjectileEntity : Entity
    {
        //true if hit astroid before disabled
        private bool _hitAstroid = false;
        
        //get setter for if hit astroid
        internal bool HitAstroid
        {
            get
            {
                if (_hitAstroid)
                {
                    _hitAstroid = false;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private Rectangle _bounds;
        private float _radius; //radius of the projectile

        /// <summary>
        /// Create a projectile
        /// </summary>
        /// <param name="radius">Radius of the projectile</param>
        public ProjectileEntity(float radius) : base(48, 48)
        {
            _radius = radius; //set radius
            speed = 700f; //set speed
        }

        internal override void OnCollision(Entity e)
        {
            if (IsEnabled)
            {
                //disable if hit blackhole or astroid
                if (e is BlackHoleEntity)
                {
                    //disable projectile
                    IsEnabled = false;
                }
                if (e is AstroidEntity) //if hit astroid
                {
                    //enable signal hit astroid
                    _hitAstroid = true;
                    
                    //disable projectile
                    IsEnabled = false;
                }
            }
        }

        internal new void Update(float delta, Rectangle bounds)
        {
            _bounds = bounds; //update bounds

            if (IsEnabled) //update parent logic if enabled
            {
                base.Update(delta);
                if (!Raylib.CheckCollisionRecs(bounds, HitBox))
                {
                    IsEnabled = false;
                }
            }
        }

        internal void Shoot(Vector2 direction, Vector2 startPos)
        {
            direction = RayMath.Vector2Normalize(direction); //normalize direction vector
            pos = startPos; //set start position
            displacement = direction * speed; //set velocity
            IsEnabled = true;
        }

        internal override void Render()
        {
            if (IsEnabled)//draw circle as projectile if enabled
            {
                Raylib.DrawCircleV(pos, _radius, Raylib.YELLOW);
            }
        }
    }
}
