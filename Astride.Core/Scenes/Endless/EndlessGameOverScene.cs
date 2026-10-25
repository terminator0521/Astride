using ZeroElectric.Vinculum;

namespace Astride.Core.Scenes.Endless
{
    internal class EndlessGameOverScene : Scene
    {
        private int _time;
        private int _finalScore; //final score

        /// <summary>
        /// Create game over scene
        /// </summary>
        internal EndlessGameOverScene(int score, int time) : base()
        {
            _finalScore = score; //set final score
            _time = time; //set final time

        }

        internal override void Update()
        {
            //no logic
        }

        internal override void Render()
        {
            //no logic
        }

        internal override Scene SetNextScene()
        {
            switch (NextScene)
            {
                case 1:
                    return new EndlessFreeplayScene(); //start new endless freeplay mode
                case -1:
                    return new MainMenuScene(); //go back to main menu
                default:
                    return null; //don't change scene
            }
        }

        internal override void UI()
        {
            //print gameover text
            RayGui.GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE, 70);
            RayGui.GuiLabel(new(490, 100, 300, 70), "Game Over");

            //draw final score and time
            RayGui.GuiLabel(new(200, 250, 340, 70), $"Score: {_finalScore}");
            RayGui.GuiLabel(new(800, 250, 340, 70), $"Time: {_time}s");

            //draw retry and quit buttons
            RayGui.GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE, 40);
            if (RayGui.GuiButton(new(330, 410, 300, 100), "Retry") == 1)
            {
                NextScene = 1; //set next scene to endless freeplay mode
            }
            if (RayGui.GuiButton(new(650, 410, 300, 100), "Quit") == 1)
            {
                NextScene = -1; //set next scene to main menu
            }
        }
    }
}
