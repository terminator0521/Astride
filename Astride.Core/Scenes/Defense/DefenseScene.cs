using Astride.Core.Entites.Defense;
using Astride.Input;
using ZeroElectric.Vinculum;

namespace Astride.Core.Scenes.Defense
{
    internal class DefenseScene : Scene
    {
        private PlayerDefenseEntity _player;
        private IInputHandler[] _inputHandlers;
        private Entity[] _entities;

        internal DefenseScene() : base()
        {
            _player = new PlayerDefenseEntity(new(10, 10));

            _inputHandlers = 
                [
                _player
                ];

            _entities =
                [
                _player
                ];
        }

        internal override void Render()
        {

        }

        internal override Scene SetNextScene()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE))
                return new MainMenuScene();

            return null;
        }

        internal override void UI()
        {
            RayGui.GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE, 40);
            RayGui.GuiLabel(new(100, 100, 500, 40), "Defense Page");
        }

        internal override void Update()
        {

        }
    }
}
