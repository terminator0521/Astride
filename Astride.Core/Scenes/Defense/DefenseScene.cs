using Astride.Core.Entites.Defense;
using Astride.Core.Entites.Fireables;
using Astride.Input;
using System.Numerics;
using ZeroElectric.Vinculum;

namespace Astride.Core.Scenes.Defense
{
    internal class DefenseScene : Scene
    {
        private PlayerDefenseEntity _player;
        private IInputHandler[] _inputHandlers;
        private List<Entity> _entities = new List<Entity>();
        private Vector2 _playerPosition = new Vector2(0);
        private Camera2D _cam = new Camera2D();
        private Vector2 _size = new Vector2(1100, 720); //size of render area
        private Texture _background = Raylib.LoadTexture("Assets/Backgrounds/debug.png");
        private Rectangle _camRect;

        internal DefenseScene() : base()
        {
            _player = new PlayerDefenseEntity(new(100, 100));

            _inputHandlers =
                [
                _player,
                ];

            _entities =
                [
                _player,
                ];

            _cam.offset = _size / 2;
            _cam.zoom = 1f;
            _cam.rotation = 0f;

            _camRect = new Rectangle(0 - _cam.offset.X + 32, 0 - _cam.offset.Y + 32, _size.X, _size.Y);

            //add projectiles
            foreach (var projectile in _player._gun[0].GetProjectiles())
            {
                _entities.Add(projectile);
            }

            Console.WriteLine(_entities.Count);
        }

        internal override void InputUpdate(IInputHandler[] inputManagers)
        {
            base.InputUpdate(inputManagers);
        }

        internal override void Render()
        {
            Raylib.BeginMode2D(_cam);

            Raylib.DrawTexturePro(_background, new Rectangle(0, 0, _background.width, _background.height), new Rectangle(0, 0, 2000, 2000), new(0f, 0f), 0f, Raylib.WHITE);

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
            _cam.target = _playerPosition;

            foreach (var entity in _entities)
            {
                if (entity is PlayerDefenseEntity player)
                {
                    player.Update(Raylib.GetFrameTime(), _cam);
                    _playerPosition = player.pos;
                }
                else if (entity is ProjectileEntity p)
                {
                    p.Update(Raylib.GetFrameTime(), _camRect);
                }
            }
        }
    }
}
