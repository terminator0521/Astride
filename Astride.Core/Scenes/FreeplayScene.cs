using Astride.Core.Entites;
using Astride.Input;
using System.Numerics;
using ZeroElectric.Vinculum;

namespace Astride.Core.Scenes
{
    internal class FreeplayScene : Scene
    {
        Texture background = Raylib.LoadTexture("Assets/Backgrounds/Debug.png");
        protected override int NextScene { get; protected private set; }
        bool isPaused;
        PlayerEntity player;
        List<Entity> entityList = new List<Entity>();

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
            player = new PlayerEntity();
        }

        internal override void Update()
        {
            InputUpdate(player);
            cam.target = player.CenterPos;
            player.Update(ref cam);
        }
        internal override void Render()
        {

            Raylib.BeginMode2D(cam);
            Raylib.DrawTexture(background, 0, 0, Raylib.WHITE);
            player.Render();
            Raylib.EndMode2D();
        }
        internal override void UI()
        {

        }

        internal override Scene SetNextScene()
        {
            return null;
        }

        internal override void InputUpdate(params IInputHandler[] inputManagers)
        {
            base.InputUpdate(inputManagers);
        }
    }
}
