using Astride.Core.Scenes.Defense;
using Astride.Core.Scenes.Endless;
using Astride.Input;
using ZeroElectric.Vinculum;
namespace Astride.Core.Scenes
{
    internal class MainMenuScene : Scene
    {
        private bool _isExtendingFreeplayOptions;
        internal override void Update()
        {
            InputUpdate(null);
        }
        internal override void Render()
        {
            //no logic
        }
        internal override void UI()
        {
            //draw main text
            RayGui.GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE, 64);
            RayGui.GuiLabel(new Rectangle(540, 20, 300, 64), "Astride");

            //draw selection buttons
            RayGui.GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE, 40);
            //freeplay options
            if (!_isExtendingFreeplayOptions)
            {
                if (RayGui.GuiButton(new(340, 270, 600, 100), "Freeplay") == 1)
                {
                    _isExtendingFreeplayOptions = true;
                }
            }
            else
            {
                if (RayGui.GuiButton(new(645, 270, 300, 100), "Freeplay") == 1)
                {
                    NextScene = 1;
                }
                if (RayGui.GuiButton(new(335, 270, 300, 100), "Defense") == 1)
                {
                    NextScene = 2;
                }
            }

            RayGui.GuiButton(new(340, 140, 600, 100), "Story Mode (In Works)");
            RayGui.GuiButton(new(340, 400, 285, 100), "Options");
            if (RayGui.GuiButton(new(655, 400, 285, 100), "Quit") == 1)
            {
                Environment.Exit(0); //exit game
            }


        }
        internal override Scene SetNextScene()
        {
            switch (NextScene)
            {
                case 1:
                    return new EndlessFreeplayScene();
                case 2:
                    return new DefenseScene();
                default:
                    return null; //dont change scene
            }
        }
        internal override void InputUpdate(IInputHandler[] inputManagers)
        {
            //disable all temp selection options
            if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_RIGHT))
            {
                _isExtendingFreeplayOptions = false;
            }

            base.InputUpdate(inputManagers);
        }
    }
}
