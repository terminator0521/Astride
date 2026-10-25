using Astride.Core.Entites.EndlessMode;
using Astride.Input;
using System.Numerics;
using ZeroElectric.Vinculum;

namespace Astride.Core.Scenes.Endless
{
    internal class EndlessFreeplayScene : Scene
    {
        //load background
        Texture _background = Raylib.LoadTexture("Assets/Backgrounds/Debug.png");

        //background positions
        Vector2 _bg1Pos;
        Vector2 _bg2Pos;

        //scene priavte
        bool _isPaused; //pause menu active bool
        private Entity[] _entityList; //array of entity references
        private IInputHandler[] _inputHandler; //array of inputhandler references (objects that handle inputs)
        private Vector2 _size = new Vector2(1100, 720); //size of render area

        //player related references
        private Camera2D _cam; //scene camera
        private Vector2 _playerPos; //player position
        private Vector2 _playerDis; //player velocity
        private Rectangle _camRect; //scene camera's rect
        private PlayerEndlessEntity _player = new PlayerEndlessEntity(); //create new player
        
        /// <summary>
        /// Creates an endless freeplay scene
        /// </summary>
        public EndlessFreeplayScene()
        {
            //init camera
            _cam = new Camera2D
            {
                offset = new Vector2(_size.X / 2, _size.Y / 2), //set offset
                rotation = 0, //set rotation 
                zoom = 1
            };

            //set background positions
            _bg1Pos = -_cam.offset; 
            _bg2Pos = _bg1Pos + new Vector2(_background.width, 0);

            _entityList =
                [
                //add player
                _player,

                //add astroids
                new AstroidEntity(new Vector2(700, 100), _player.CenterPos),
                new AstroidEntity(new Vector2(700, 200), _player.CenterPos),
                new AstroidEntity(new Vector2(700, 300), _player.CenterPos),
                new AstroidEntity(new Vector2(700, -100), _player.CenterPos),
                new AstroidEntity(new Vector2(700, -200), _player.CenterPos),
                new AstroidEntity(new Vector2(700, -300), _player.CenterPos),
                new AstroidEntity(new Vector2(700, 0), _player.CenterPos),

                //add blackhole
                new BlackHoleEntity(_size, _cam)
                ];
            
            //array of input handler
            _inputHandler =
                [
                //add player
                _player
                ];
            
            //init camera rectangle
            _camRect = new Rectangle(0 - _cam.offset.X + 32, 0 - _cam.offset.Y + 32, _size.X, _size.Y + 2000);
        }

        internal override void Update()
        {
            //update objects that are input handlers
            InputUpdate(_inputHandler);

            //check if game is paused
            if (!_isPaused)
            {
                // update camera rect's position based on player displacement
                _camRect.X += _playerDis.X * Raylib.GetFrameTime();
                _camRect.Y += _playerDis.Y * Raylib.GetFrameTime();

                foreach (var e in _entityList)
                {
                    //update each entity depending on their type
                    if (e is PlayerEndlessEntity player) //if player do player stuff (gurenteed)
                    {
                        _cam.target = player.CenterPos; //update scene camera target
                        _playerPos = player.CenterPos; //get player position
                        _playerDis = player.Displacement; //get player's velocity or displacement over time
                        
                        //update player logic and pass in scene camera
                        player.Update(_cam);

                        //change scene if player's health is at 0
                        if (player.Health <= 0)
                            NextScene = 1;
                    }
                    else if (e is BlackHoleEntity blackHole)
                    {
                        //update blackhole
                        blackHole.Update(in _playerDis);
                    }
                    else if (e is AstroidEntity astroid)
                    {
                        //update astroids
                        astroid.Update(in _playerPos, in _playerDis, in _camRect);
                    }
                }

                //check collisions
                for (int i = 0; i < _entityList.Length; i++)
                {
                    for (int j = i + 1; j < _entityList.Length; j++)
                    {
                        if (Raylib.CheckCollisionRecs(_entityList[i].HitBox, _entityList[j].HitBox))
                        {
                            //run collision logic
                            _entityList[i].OnCollision(_entityList[j]);
                            _entityList[j].OnCollision(_entityList[i]);
                        }
                    }
                }

                //move backgrounds based on player displacement
                _bg1Pos -= _playerDis * Raylib.GetFrameTime();
                _bg2Pos -= _playerDis * Raylib.GetFrameTime();


                //reset background positions to ahead of background infront when offscreen
                if (_bg1Pos.X < _cam.target.X - _cam.offset.X - _background.width)
                {
                    _bg1Pos.X = _bg2Pos.X + _background.width;
                }
                else if (_bg2Pos.X < _cam.target.X - _cam.offset.X - _background.width)
                {
                    _bg2Pos.X = _bg1Pos.X + _background.width;
                }
            }
        }
        internal override void Render()
        {
            Raylib.BeginMode2D(_cam); //begin camera mode

            //draw backgrounds (WORK ON)
            Raylib.DrawTextureV(_background, _bg1Pos, Raylib.WHITE);
            Raylib.DrawTextureV(_background, _bg2Pos, Raylib.WHITE);

            //render each entity
            foreach (var e in _entityList)
            {
                e.Render();
            }

            Raylib.EndMode2D(); //end camera mode
        }
        internal override void UI()
        {
            //place ui elements on screen if game is paused
            if (_isPaused)
            {
                Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), new Color(0, 0, 0, 100)); //gray out background

                //draw paused text
                RayGui.GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE, 70);
                RayGui.GuiLabel(new(490, 100, 300, 70), "Paused");

                //draw continue and quit buttons
                RayGui.GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE, 40);
                if (RayGui.GuiButton(new(330, 410, 300, 100), "Continue") == 1)
                {
                    _isPaused = false; //unpause game
                }
                if (RayGui.GuiButton(new(650, 410, 300, 100), "Quit") == 1)
                {
                    NextScene = -1; //set next scene
                }
            }
        }

        internal override Scene SetNextScene()
        {
            switch (NextScene)
            {
                case -1:
                    return new MainMenuScene(); //return to main menu
                case 1:
                    return new EndlessGameOverScene(); //go to game over screen
                default:
                    return null; //don't change scenes
            }
        }

        internal override void InputUpdate(IInputHandler[] inputManagers)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_P)) //toggle pausing
                _isPaused = !_isPaused;

            //update input managed objects (inputHhandler's)
            base.InputUpdate(inputManagers);
        }
    }
}
