using Astride.Input;
using raygui_cs;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Astride.Core.Scenes
{
    internal class MainMenuScene : Scene
    {
        public override void Update()
        {
            
        }
        public override void Render()
        {

        }
        public override void UI()
        {
            Raygui.GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE, 32);
            Raygui.GuiDrawText("Main Menu", new Rectangle(490, 10, 300, 32), 1, Color.Black);
        }
        internal override Scene SetNextScene()
        {
            return null;
        }

        //internal override void InputUpdate(params ControlListener[] inputManagers)
        //{
        //    base.InputUpdate(inputManagers);
        //}
    }
}
