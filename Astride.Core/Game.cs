using Astride.Core.Scenes;
using System.Numerics;
using ZeroElectric.Vinculum;

namespace Astride.Core
{
    public class Game
    {
        private Scene _scene;

        public Rectangle src;
        public Rectangle dest;
        public RenderTexture renderTexture;

        public Vector2 origin;
        public Game(int width, int height)
        {
            renderTexture = Raylib.LoadRenderTexture(width, height);
            origin = new Vector2(width / 2, height / 2);
            src = new Rectangle(0, 0, width, -height);
            dest = new Rectangle(0, 0, width, height);
            _scene = new MainMenuScene();
        }

        public void Update()
        {
            _scene.Update();
        }
        public void Render()
        {
            _scene.Render();
        }

        public void UI()
        {
            _scene.UI();
        }

        public void getNextScene()
        {
            if (_scene.SetNextScene() is not null)
            {
                _scene = _scene.SetNextScene();
            }
        }


    }
}
