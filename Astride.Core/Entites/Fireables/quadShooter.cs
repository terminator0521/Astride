using Astride.Input;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using ZeroElectric.Vinculum;

namespace Astride.Core.Entites.Fireables
{
    internal class QuadShooter : Shooter, IInputHandler
    {
        public QuadShooter(int size, int projectileCount) : base(size, projectileCount)
        {
        }

        internal override void Update(float delta, Rectangle bounds, Vector2 aimDirection, Vector2 position)
        {
            base.Update(delta, bounds, aimDirection, position);
        }

        public override void InputUpdate()
        {
            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
            {
                Shoot(aimDirection, pos);
            }
        }

        internal override void Shoot(Vector2 aimDirection, Vector2 position)
        {
            foreach (var p in projectiles)
            {
                if (!p.IsEnabled)
                {
                    p.Shoot(aimDirection, pos);
                }
            }
        }
    }
}
