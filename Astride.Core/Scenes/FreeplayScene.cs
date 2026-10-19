using Astride.Core.Entites.EndlessMode;
using Astride.Input;
using System.Numerics;
using ZeroElectric.Vinculum;

namespace Astride.Core.Scenes
{
    internal class FreeplayScene : Scene
    {
        Texture background = Raylib.LoadTexture("Assets/Backgrounds/Debug.png");
        Vector2 dis = new Vector2(0); //scene objects displacement based on player displacement
        bool isPaused;
        List<Entity> entityList;
        Vector2 size = new Vector2(1100, 720);
        Camera2D cam;

        Vector2 playerPos;

        Vector2 playerDis;

        public FreeplayScene()
        {
            cam = new Camera2D
            {
                offset = new Vector2(size.X / 2, size.Y / 2),
                rotation = 0,
                zoom = 1
            };
            entityList = new List<Entity>
            {
                new PlayerEndlessEntity(),
                new AstroidEntity(new Vector2(300, 400)),
                new BlackHoleEntity(size, cam)
            };

        }

        internal override void Update()
        {
            foreach (var e in entityList)
            {
                if (e is PlayerEndlessEntity player) //if player do player stuff (gurenteed)
                {
                    cam.target = player.CenterPos;
                    playerPos = player.CenterPos;
                    playerDis = player.Displacement;
                    InputUpdate(player);
                    player.Update(cam);
                    if (player.Health <= 0)
                        NextScene = -1;
                }
                else if (e is BlackHoleEntity blackHole)
                {
                    blackHole.Update(in playerDis);
                }
                else if (e is AstroidEntity astroid)
                {
                    astroid.Update(in playerPos, in playerDis);
                }
            }

            for (int i = 0; i < entityList.Count; i++)
            {
                for (int j = i + 1; j < entityList.Count; j++)
                {
                    if (Raylib.CheckCollisionRecs(entityList[i].dest, entityList[j].dest))
                    {
                        entityList[i].OnCollision(entityList[j]);
                        entityList[j].OnCollision(entityList[i]);
                    }
                }

            }
        }
        internal override void Render()
        {
            Raylib.BeginMode2D(cam);
            //Raylib.DrawTexture(background, 0, 0, Raylib.WHITE);

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

            switch (NextScene)
            {
                case -1:
                    return new MainMenuScene();
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
