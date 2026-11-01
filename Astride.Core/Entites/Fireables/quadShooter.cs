using Astride.Input;

namespace Astride.Core.Entites.Fireables
{
    internal class QuadShooter : Shooter, IInputHandler
    {
        public QuadShooter(int size, int projectileCount) : base(size, projectileCount)
        {
            projectiles = new ProjectileEntity[projectileCount];

            for (int i = 0; i < projectileCount; i++)
            {
                projectiles[i] = new ProjectileEntity(size);
            }
        }
    }
}
