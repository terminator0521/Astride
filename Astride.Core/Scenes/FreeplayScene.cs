using Astride.Core.Entites;
using Astride.Input;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using ZeroElectric.Vinculum;

namespace Astride.Core.Scenes
{
    internal class FreeplayScene : Scene
    {
        Texture background = Raylib.LoadTexture("Assets/Backgrounds/Debug.png");
        protected override int NextScene { get; protected private set; }
        bool isPaused;
        Entity player = new TestEntity();
        Color[] colors = new Color[50 * 50];

        Camera2D cam;
        public FreeplayScene()
        {
            cam = new Camera2D
            {
                target = player.pos,
                offset = new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2),
                rotation = 0,
                zoom = 1
            };
        }

        internal override void Update()
        {
            player.Update();

            if(player is IInputHandler playerInput)
                InputUpdate(playerInput);
        }
        internal override void Render()
        {

            Raylib.BeginMode2D(cam);
            Raylib.DrawTexture(background, 0, 0, Raylib.WHITE);
            player.Render();
            cam.target = player.pos;
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

        private Color GenerateRandomColor()
        {
            return new Color(Raylib.GetRandomValue(0, 3) * 100, Raylib.GetRandomValue(0, 3) * 100, Raylib.GetRandomValue(0, 3) * 100, Raylib.GetRandomValue(0, 3) * 100);
        }
    }
}
