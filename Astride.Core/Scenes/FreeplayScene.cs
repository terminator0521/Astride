using Astride.Core.Entites;
using Astride.Input;
using System.Numerics;
using ZeroElectric.Vinculum;

namespace Astride.Core.Scenes
{
    internal class FreeplayScene : Scene
    {
        Texture background = Raylib.LoadTexture("Assets/Backgrounds/Debug.png");
        Vector2 dis = new Vector2(0); //scene objects displacement based on player displacement
        protected override int NextScene { get; protected private set; }
        bool isPaused;
        List<Entity> entityList = new List<Entity>(1);
        Vector2 size = new Vector2(1100, 720);
        Camera2D cam;

        public FreeplayScene()
        {
            cam = new Camera2D
            {
                offset = new Vector2(size.X / 2, size.Y / 2),
                rotation = 0,
                zoom = 1
            };
            entityList.Add(new PlayerEndlessEntity());
        }

        internal override void Update()
        {
            foreach (var e in entityList)
            {
                if (e is PlayerEndlessEntity player) //if player do player stuff (gurenteed)
                {
                    cam.target = player.CenterPos;
                    InputUpdate(player);
                    player.Update(cam);
                    //dis = player.Displacement;
                }
                else
                {
                    e.Update();
                }
            }
        }
        internal override void Render()
        {
            Raylib.BeginMode2D(cam);
            Raylib.DrawTexture(background, 0, 0, Raylib.WHITE);

            foreach (var e in entityList)
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
            return null;
        }

        internal override void InputUpdate(params IInputHandler[] inputManagers)
        {
            base.InputUpdate(inputManagers);
        }
    }
}
