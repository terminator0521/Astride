using ZeroElectric.Vinculum;

namespace Astride.Core.Scenes.Endless
{
    internal class EndlessGameOverScene : Scene
    {
        internal EndlessGameOverScene() : base()
        {

        }

        internal override void Update()
        {
        }

        internal override void Render()
        {

        }

        internal override Scene SetNextScene()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE))
            {
                return new EndlessGameOverScene();
            }

            switch (NextScene)
            {
                case 1:
                    return new EndlessFreeplayScene();
                case -1:
                    return new MainMenuScene();
                default:
                    return null;
            }
        }

        internal override void UI()
        {
            RayGui.GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE, 70);
            RayGui.GuiLabel(new(490, 100, 300, 70), "Game Over");
            RayGui.GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE, 40);
            if (RayGui.GuiButton(new(330, 410, 300, 100), "Retry") == 1)
            {
                NextScene = 1;
            }
            if (RayGui.GuiButton(new(650, 410, 300, 100), "Quit") == 1)
            {
                NextScene = -1;
            }
        }
    }
}
