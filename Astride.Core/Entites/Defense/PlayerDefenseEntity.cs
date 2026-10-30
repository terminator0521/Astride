using Astride.Core.Entites.Fireables;
using Astride.Input;
using System.Numerics;
using ZeroElectric.Vinculum;

namespace Astride.Core.Entites.Defense
{
    internal class PlayerDefenseEntity : Entity, IInputHandler
    {
        internal int Score { get; private set; }

        private Camera2D _cam;
        private float _boosterVelIncrease = 25f;
        private float[] _weaponVelIncrease = [75f, 100f, 150f];
        private Shooter gun = new QuadShooter(48, 4);

        /// <summary>
        /// 0 for 
        /// </summary>
        private int _selectedFireMode = 0;

        internal PlayerDefenseEntity(Vector2 startPos) : base(40, 40, startPos)
        {
        }

        internal override void Update(float delta)
        {
            gun.Update(delta, new Rectangle(0, 0, 1100, 720), RayMath.Vector2Normalize(Raylib.GetMousePosition() - pos), pos);
            base.Update(delta);
        }
        public void InputUpdate()
        {
            //displacement = RayMath.Vector2Normalize()
        }

        internal override void OnCollision(Entity e)
        {

        }
    }
}
