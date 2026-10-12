using Astride.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Astride.Core.Entites
{
    internal class PlayerEntity : Entity, IInputHandler
    {
        public PlayerEntity() : base(64, 64)
        {
            speed = 300f;

        }

        public void InputUpdate()
        {
            throw new NotImplementedException();
        }
        
        internal override void Render()
        {
            base.Render();
        }

        internal override void Update()
        {
            base.Update();
        }
    }
}
