using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Reflection;
using System.Text;
using ZeroElectric.Vinculum;

namespace Astride.Core.Entites.Fireables
{

    internal class ProjectileEntity : Entity
    {
        private Rectangle _bounds;
        private float _radius; //radius of the projectile

        /// <summary>
        /// Create a projectile
        /// </summary>
        /// <param name="radius">Radius of the projectile</param>
        public ProjectileEntity(float radius) : base((int)radius * 2, (int)radius * 2)
        {
            _radius = radius; //set radius
            speed = 700f; //set speed
        }

        internal override void OnCollision(Entity e)
        {
            if (IsEnabled)
            {
                
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
