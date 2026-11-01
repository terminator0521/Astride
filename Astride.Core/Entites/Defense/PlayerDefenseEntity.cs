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
        private Shooter[] _gun = {new QuadShooter(24, 5)};

        private int _selectedGun = 0;

        /// <summary>
        /// 0 for 
        /// </summary>
        private int _selectedFireMode = 0;

        internal PlayerDefenseEntity(Vector2 startPos) : base(40, 40, startPos)
        {
        }

        internal new void Update(float delta, Camera2D camera)
        {
            Console.WriteLine(pos);
            this._cam = camera;

            _gun[_selectedGun].Update(delta, new Rectangle(0, 0, 1100, 720), RayMath.Vector2Normalize(Raylib.GetMousePosition() - pos), pos);

            if(displacement.Length() > 600f) 
            {
                displacement = RayMath.Vector2Normalize(displacement) * 600f;
            }

            base.Update(delta);
        }
        public void InputUpdate()
        {
            if(Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_RIGHT))
            {
                displacement += -RayMath.Vector2Normalize(Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), _cam) - pos) * speed;
                displacement += -RayMath.Vector2Normalize(Raylib.GetScreenToWorld2D(Raylib.GetMousePosition(), _cam) - pos) * _weaponVelIncrease[_selectedGun];

            }
        }

        internal override void OnCollision(Entity e)
        {

        }
    }
}
