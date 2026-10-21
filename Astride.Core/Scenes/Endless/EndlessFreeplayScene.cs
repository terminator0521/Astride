using Astride.Core.Entites.EndlessMode;
using Astride.Input;
using System.Numerics;
using ZeroElectric.Vinculum;

namespace Astride.Core.Scenes.Endless
{
    internal class EndlessFreeplayScene : Scene
    {
        Texture _background = Raylib.LoadTexture("Assets/Backgrounds/Debug.png");

        Vector2 _d1 = new Vector2(-1000, 0);
        Vector2 _d2 = new Vector2(1000, 0);

        Vector2 _dis = new Vector2(0); //scene objects displacement based on player displacement
        bool _isPaused;
        List<Entity> _entityList;
        Vector2 _size = new Vector2(1100, 720);
        Camera2D _cam;

        Vector2 _playerPos;

        Vector2 _playerDis;

        public EndlessFreeplayScene()
        {
            _cam = new Camera2D
            {
                offset = new Vector2(_size.X / 2, _size.Y / 2),
                rotation = 0,
                zoom = 1
            };
            _entityList = new List<Entity>
            {
                new PlayerEndlessEntity(),
                new AstroidEntity(new Vector2(300, 400)),
                //new BlackHoleEntity(_size, _cam)
            };

        }

        internal override void Update()
        {
            foreach (var e in _entityList)
            {
                if (e is PlayerEndlessEntity player) //if player do player stuff (gurenteed)
                {
                    _cam.target = player.CenterPos;
                    _playerPos = player.CenterPos;
                    _playerDis = player.Displacement;
                    InputUpdate(player);
                    player.Update(_cam);
                    if (player.Health <= 0)
                        NextScene = 1;
                }
                else if (e is BlackHoleEntity blackHole)
                {
                    blackHole.Update(in _playerDis);
                }
                else if (e is AstroidEntity astroid)
                {
                    astroid.Update(in _playerPos, in _playerDis);
                }
            }

            for (int i = 0; i < _entityList.Count; i++)
            {
                for (int j = i + 1; j < _entityList.Count; j++)
                {
                    if (Raylib.CheckCollisionRecs(_entityList[i].dest, _entityList[j].dest))
                    {
                        _entityList[i].OnCollision(_entityList[j]);
                        _entityList[j].OnCollision(_entityList[i]);
                    }
                }

            }

            //background
            _d1.X += -_playerDis.X * Raylib.GetFrameTime();
            _d1.Y += -_playerDis.Y * Raylib.GetFrameTime();
            _d2.X += -_playerDis.X * Raylib.GetFrameTime();
            _d2.Y += -_playerDis.Y * Raylib.GetFrameTime();

            if (_d1.X < -3000)
            {
                _d1.X = 1000;
            }
            else if (_d2.X < -3000)
            {
                _d2.X = 1000;
            }

        }
        internal override void Render()
        {
            Raylib.BeginMode2D(_cam);

            Raylib.DrawTextureV(_background, _d1, Raylib.WHITE);
            Raylib.DrawTextureV(_background, _d2, Raylib.WHITE);

            foreach (var e in _entityList)
            {
                e.Render();
            }

            Raylib.EndMode2D();
        }
        internal override void UI()
        {

        }

        internal override Scene SetNextScene()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE))
                return new MainMenuScene();

            switch (NextScene)
            {
                case 1:
                    return new EndlessGameOverScene();
                default:
                    return null;
            }
        }

        internal override void InputUpdate(params IInputHandler[] inputManagers)
        {
            foreach (var inputManager in inputManagers)
                base.InputUpdate(inputManagers);
        }
    }
}
