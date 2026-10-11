using Astride.Core.Entites;
using Astride.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Astride.Core.Scenes
{
    internal class FreeplayScene : Scene
    {
        protected override int NextScene { get; protected private set; }
        bool isPaused;
        Entity player = new TestEntity(10, 10, new(300, 300), 10f);

        public FreeplayScene()
        {

        }

        internal override void Update()
        {
            player.Update();

            if(player is IInputHandler playerInput)
                InputUpdate(playerInput);
        }
        internal override void Render()
        {
            player.Render();
        }
        internal override void UI()
        {

        }

        internal override Scene SetNextScene()
        {
            return null;
        }

        internal override void InputUpdate(params IInputHandler[] inputManagers)
        {
            base.InputUpdate(inputManagers);

        }
    }
}
