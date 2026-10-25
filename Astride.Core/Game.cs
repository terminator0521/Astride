using Astride.Core.Scenes;
using System.Numerics;
using ZeroElectric.Vinculum;

namespace Astride.Core
{
    public class Game
    {
        //scene instance
        private Scene _scene;

        public Rectangle src; //parsing rectangle for render texture
        public Rectangle dest; //actual rendering rectangle for rendering game
        public RenderTexture renderTexture; //render texture for rendering game

        public Vector2 origin; //middle point of render texture

        /// <summary>
        /// create new game
        /// </summary>
        /// <param name="width">Width of game screen</param>
        /// <param name="height">Height of game screen</param>
        public Game(int width, int height)
        {
            renderTexture = Raylib.LoadRenderTexture(width, height); //init render texture
            origin = new Vector2(width / 2, height / 2); //set middle point 
            src = new Rectangle(0, 0, width, -height); //set parsing rectangle
            dest = new Rectangle(0, 0, width, height); //set posting rectangle
            _scene = new MainMenuScene(); //set scene to main menu
        }

        public void Update()
        {
            //update scene logic
            _scene.Update();

            //check for scene checks
            var nextScene = _scene.SetNextScene(); //get new scene

            if (nextScene is not null) //update scene if it doesnt return null
            {
                _scene = nextScene;
            }
        }
        public void Render()
        {
            _scene.Render(); //render scene game elements
        }

        public void UI()
        {
            _scene.UI(); //render scene UI elements
        }
    }
}
