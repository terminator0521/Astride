using ZeroElectric.Vinculum;

namespace Astride.Core.Scenes.Defense
{
    internal class DefenseScene : Scene
    {
        internal override void Render()
        {

        }

        internal override Scene SetNextScene()
        {
            if (Raylib.IsKeyPressed(KeyboardKey.KEY_BACKSPACE))
                return new MainMenuScene();

            return null;
        }

        internal override void UI()
        {
            RayGui.GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE, 40);
            RayGui.GuiLabel(new(100, 100, 500, 40), "Defense Page");
        }

        internal override void Update()
        {

        }
    }
}
