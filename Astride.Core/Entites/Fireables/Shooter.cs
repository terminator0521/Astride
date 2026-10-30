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
        public Shooter(int size, int projectileCount) : base(size, size)
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
            
        }
        internal override void OnCollision(Entity e)
        {
            
        }

        internal virtual void Shoot(Vector2 aimDirection, Vector2 position)
        {

        }

        public virtual void InputUpdate()
        {
            Console.WriteLine("not working");
        }
    }
}
