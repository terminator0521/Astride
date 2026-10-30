using Astride.Core.Entites.Defense;
using Astride.Input;
using System.Numerics;
using ZeroElectric.Vinculum;

namespace Astride.Core.Scenes.Defense
{
    internal class DefenseScene : Scene
    {
        private PlayerDefenseEntity _player;
        private IInputHandler[] _inputHandlers;
        private Entity[] _entities;
        private Vector2 _playerPosition = new Vector2(0);
        private Camera2D _camera = new Camera2D();
        private Vector2 _size = new Vector2(1100, 720); //size of render area

        internal DefenseScene() : base()
        {
            _player = new PlayerDefenseEntity(new(100, 100));

            _inputHandlers =
                [
                _player
                ];

            _entities =
                [
                _player
                ];

            _camera.offset = _size / 2;
            _camera.zoom = 1.5f;
            _camera.rotation = 0f;
        }

        internal override void InputUpdate(IInputHandler[] inputManagers)
        {
            base.InputUpdate(inputManagers);
        }

        internal override void Render()
        {
            Raylib.BeginMode2D(_camera);

            foreach (var entity in _entities)
            {
                entity.Render();
            }

            Raylib.EndMode2D();
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
            InputUpdate(_inputHandlers);
            _camera.target = _playerPosition;

            foreach (var entity in _entities)
            {
                if (entity is PlayerDefenseEntity player)
                {
                    player.Update(Raylib.GetFrameTime());
                    _playerPosition = player.pos;
                }
            }
        }
    }
}
