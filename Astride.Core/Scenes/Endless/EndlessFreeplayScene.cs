using Astride.Core.Entites.EndlessMode;
using Astride.Input;
using System.Numerics;
using ZeroElectric.Vinculum;

namespace Astride.Core.Scenes.Endless
{
    internal class EndlessFreeplayScene : Scene
    {

        //scene priavte
        bool _isPaused;
        Entity[] _entityList;
        IInputHandler[] _inputHandler;
        Vector2 _size = new Vector2(1100, 720);

        //player related references
        Vector2 _dis = new Vector2(0); //scene objects displacement based on player displacement
        Camera2D _cam;
        Vector2 _playerPos;
        Vector2 _playerDis;
        Rectangle _camRect;
        PlayerEndlessEntity _player = new PlayerEndlessEntity();

        public EndlessFreeplayScene()
        {
            _cam = new Camera2D
            {
                offset = new Vector2(_size.X / 2, _size.Y / 2),
                rotation = 0,
                zoom = 1
            };
            _entityList =
                [
                _player,
                //new AstroidEntity(new Vector2(700, 100), _player.CenterPos),
                //new AstroidEntity(new Vector2(700, 200), _player.CenterPos),
                //new AstroidEntity(new Vector2(700, 300), _player.CenterPos),
                //new AstroidEntity(new Vector2(700, -100), _player.CenterPos),
                //new AstroidEntity(new Vector2(700, -200), _player.CenterPos),
                //new AstroidEntity(new Vector2(700, -300), _player.CenterPos),
                new AstroidEntity(new Vector2(700, 0), _player.CenterPos),
                new BlackHoleEntity(_size, _cam)
                ];
            _inputHandler =
                [
                _player
                ];
            _camRect = new Rectangle(0 - _cam.offset.X + 32, 0 - _cam.offset.Y + 32, _size.X, _size.Y + 2000);
        }

        internal override void Update()
        {
            InputUpdate(_inputHandler);

            //bounds
            _camRect.X += _playerDis.X * Raylib.GetFrameTime();
            _camRect.Y += _playerDis.Y * Raylib.GetFrameTime();

            foreach (var e in _entityList)
            {
                if (e is PlayerEndlessEntity player) //if player do player stuff (gurenteed)
                {
                    _cam.target = player.CenterPos;
                    _playerPos = player.CenterPos;
                    _playerDis = player.Displacement;
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
                    astroid.Update(in _playerPos, in _playerDis, in _camRect);
                }
            }

            for (int i = 0; i < _entityList.Length; i++)
            {
                for (int j = i + 1; j < _entityList.Length; j++)
                {
                    if (Raylib.CheckCollisionRecs(_entityList[i].HitBox, _entityList[j].HitBox))
                    {
                        _entityList[i].OnCollision(_entityList[j]);
                        _entityList[j].OnCollision(_entityList[i]);
                    }
                }
            }

        }
        internal override void Render()
        {
            Raylib.BeginMode2D(_cam);

            foreach (var e in _entityList)
            {
                e.Render();
            }
            Raylib.DrawRectangleRec(_camRect, new Color(20, 50, 70, 60));

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
    }
}
