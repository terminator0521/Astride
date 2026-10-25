using ZeroElectric.Vinculum;
using Astride.Core.Scenes.Endless;
namespace Astride.Core.Scenes
{
    internal class MainMenuScene : Scene
    {
        internal override void Update()
        {
            //no logic
        }
        internal override void Render()
        {
            //no logic
        }
        internal override void UI()
        {
            //draw main text
            RayGui.GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE, 64);
            RayGui.GuiLabel(new Rectangle(540, 20, 300, 64), "Astride");

            //draw selection buttons
            RayGui.GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE, 40);
            if (RayGui.GuiButton(new(340, 270, 600, 100), "Freeplay") == 1) NextScene = 1;
            RayGui.GuiButton(new(340, 140, 600, 100), "Story Mode (In Works)");
            RayGui.GuiButton(new(340, 400, 285, 100), "Options");
            if (RayGui.GuiButton(new(655, 400, 285, 100), "Quit") == 1)
            {
                Environment.Exit(0); //exit game
            }

        }
        internal override Scene SetNextScene()
        {
            if (NextScene == 1)
            {
                return new EndlessFreeplayScene(); //start endless freeplay game
            }
            return null; //dont change scene
        }
    }
}
