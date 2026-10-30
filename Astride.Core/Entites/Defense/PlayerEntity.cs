using Astride.Input;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using ZeroElectric.Vinculum;

namespace Astride.Core.Entites.Defense
{
    internal class PlayerDefenseEntity : Entity, IInputHandler
    {
        internal int Score { get; private set; }

        private Camera2D _cam;
        private float _boosterVelIncrease = 25f;
        private float[] _weaponVelIncrease = [75f, 100f, 150f];
        
        /// <summary>
        /// 0 for 
        /// </summary>
        private int _selectedFireMode = 0;

        internal PlayerDefenseEntity() : base()
        {

        }

        public void InputUpdate()
        {

        }

        internal override void OnCollision(Entity e)
        {

        }
    }
}
