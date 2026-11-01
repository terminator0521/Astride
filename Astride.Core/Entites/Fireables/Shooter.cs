using Astride.Input;
using System.Numerics;
using ZeroElectric.Vinculum;

namespace Astride.Core.Entites.Fireables
{

    /// <summary>
    /// This class inherits "Entity" but not "IInputHandler"
    /// </summary>
    internal class Shooter : Entity, IInputHandler
    {
        protected Vector2 aimDirection;
        protected ProjectileEntity[] projectiles;
        internal Shooter(int size, int projectileCount) : base(size, size)
        {
            //construct projectiles
            projectiles = new ProjectileEntity[projectileCount];
            for (int i = 0; i < projectileCount; i++)
            {
                projectiles[i] = new ProjectileEntity(size);
            }
        }

        internal virtual void Update(float delta, Rectangle bounds, Vector2 aimDirection, Vector2 position)
        {
            foreach (var projectile in projectiles)
            {
                projectile.Update(delta, bounds);
            }
        }
        internal override void OnCollision(Entity e)
        {

        }

        internal virtual void Shoot(Vector2 aimDirection, Vector2 position)
        {
            foreach (var p in projectiles)
            {
                if (!p.IsEnabled)
                {
                    p.Shoot(aimDirection, position);
                }
            }
        }

        public virtual void InputUpdate()
        {
            Console.WriteLine("not working");
        }

        internal ProjectileEntity[] GetProjectiles()
        {
            return projectiles;
        }
    }
}
