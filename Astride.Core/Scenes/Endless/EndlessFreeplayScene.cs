using Astride.Core.Entites.EndlessMode;
using Astride.Input;
using System.Numerics;
using ZeroElectric.Vinculum;

namespace Astride.Core.Scenes.Endless
{
    internal class EndlessFreeplayScene : Scene
    {
        //background
        Texture _background = Raylib.LoadTexture("Assets/Backgrounds/Debug.png");

        Vector2 _bg1Pos;
        Vector2 _bg2Pos;

        //Vector2

        //scene priavte
        bool _isPaused;
        private Entity[] _entityList;
        private IInputHandler[] _inputHandler;
        private Vector2 _size = new Vector2(1100, 720);

        //player related references
        private Vector2 _dis = new Vector2(0); //scene objects displacement based on player displacement
        private Camera2D _cam;
        private Vector2 _playerPos;
        private Vector2 _playerDis;
        private Rectangle _camRect;
        private PlayerEndlessEntity _player = new PlayerEndlessEntity();
        public EndlessFreeplayScene()
        {
            //init camera
            _cam = new Camera2D
            {
                offset = new Vector2(_size.X / 2, _size.Y / 2),
                rotation = 0,
                zoom = 1
            };

            //background positions
            _bg1Pos = -_cam.offset;
            _bg2Pos = _bg1Pos + new Vector2(_background.width, 0);

            _entityList =
                [
                _player,
                //new AstroidEntity(new Vector2(700, 100), _player.CenterPos),
                //new AstroidEntity(new Vector2(700, 200), _player.CenterPos),
                //new AstroidEntity(new Vector2(700, 300), _player.CenterPos),
                //new AstroidEntity(new Vector2(700, -100), _player.CenterPos),
                //new AstroidEntity(new Vector2(700, -200), _player.CenterPos),
                //new AstroidEntity(new Vector2(700, -300), _player.CenterPos),
                //new AstroidEntity(new Vector2(700, 0), _player.CenterPos),
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

            if (!_isPaused)
            {


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

                //move background
                _bg1Pos.X -= _playerDis.X * Raylib.GetFrameTime();
                _bg2Pos.X -= _playerDis.X * Raylib.GetFrameTime();

                _bg1Pos.Y += _playerDis.Y * Raylib.GetFrameTime();
                _bg2Pos.Y += _playerDis.Y * Raylib.GetFrameTime();



                if (_bg1Pos.X < _cam.target.X - _cam.offset.X - _background.width)
                {
                    _bg1Pos.X = _bg2Pos.X + _background.width;
                    Console.WriteLine("1");
                }
                else if (_bg2Pos.X < _cam.target.X - _cam.offset.X - _background.width)
                {
                    Console.WriteLine("2");
                    _bg2Pos.X = _bg1Pos.X + _background.width;
                }
            }
        }
        internal override void Render()
        {
            Raylib.BeginMode2D(_cam);

            //draw backgrounds (WORK ON)
            Raylib.DrawTextureV(_background, _bg1Pos, Raylib.WHITE);
            Raylib.DrawTextureV(_background, _bg2Pos, Raylib.WHITE);

            foreach (var e in _entityList)
            {
                e.Render();
            }

            Raylib.EndMode2D();
        }
        internal override void UI()
        {
            if (_isPaused)
            {
                Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), new Color(0, 0, 0, 100)); //gray out background

                RayGui.GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE, 70);
                RayGui.GuiLabel(new(490, 100, 300, 70), "Paused");
                RayGui.GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE, 40);
                if (RayGui.GuiButton(new(330, 410, 300, 100), "Continue") == 1)
                {
                    _isPaused = false;
                }
                if (RayGui.GuiButton(new(650, 410, 300, 100), "Quit") == 1)
                {
                    NextScene = -1;
                }
            }
        }

        internal override Scene SetNextScene()
        {
            switch (NextScene)
            {
                case -1:
                    return new MainMenuScene();
                case 1:
                    return new EndlessGameOverScene();
                default:
                    return null;
            }
        }

        internal override void InputUpdate(IInputHandler[] inputManagers)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_P)) //toggle pausing
                _isPaused = !_isPaused;

            base.InputUpdate(inputManagers);
        }
    }
}
