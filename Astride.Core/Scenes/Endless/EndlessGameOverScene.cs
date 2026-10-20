using System;
using System.Collections.Generic;
using System.Text;
using ZeroElectric.Vinculum;

namespace Astride.Core.Scenes.Endless
{
    internal class EndlessGameOverScene : Scene
    {
        internal EndlessGameOverScene() : base()
        {

        }

        internal override void Update()
        {
            Console.WriteLine("hi");
        }

        internal override void Render()
        {
            
        }

        internal override Scene SetNextScene()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE))
            {
                Console.WriteLine("Reloaded Game Over Scene @: " + Raylib.GetTime());
                return new EndlessGameOverScene();
            }
            return null;
        }

        internal override void UI()
        {

        }
    }
}
