using Raylib_cs;
using raygui_cs;
using System.Numerics;
using Astride.Core.Scenes;

namespace Astride.Core
{
    public class Game
    {
        private Scene scene;

        public Rectangle src;
        public Rectangle dest;
        public RenderTexture2D renderTexture;

        public Vector2 origin;
        public Game(int width, int height)
        {
            renderTexture = Raylib.LoadRenderTexture(width, height);
            origin = new Vector2(width / 2, height / 2);
            src = new Rectangle(0, 0, width, -height);
            dest = new Rectangle(0, 0, width, height);
            scene = new MainMenuScene();
        }

        public void Update()
        {
            scene.Update();
        }
        public void Render()
        {
            scene.Render();
        }

        public void UI()
        {
            scene.UI();
        }

        public void getNextScene()
        {
            if (scene.SetNextScene() is not null)
            {
                scene = scene.SetNextScene();
            }
        }


    }
}
