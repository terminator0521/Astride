using ZeroElectric.Vinculum;

namespace Astride.Core.Scenes.Endless
{
    internal class EndlessGameOverScene : Scene
    {
        private int _finalScore;

        /// <summary>
        /// Create game over scene
        /// </summary>
        internal EndlessGameOverScene(int score) : base()
        {
            _finalScore = score;
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

            //draw final score
            RayGui.GuiLabel(new(410, 200, 340, 70), $"Score: {_finalScore}");

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
