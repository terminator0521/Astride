using ZeroElectric.Vinculum;

namespace Astride.Core.Scenes
{
    internal class MainMenuScene : Scene
    {
        private int _scene;
        public override void Update()
        {

        }
        public override void Render()
        {

        }
        public override void UI()
        {
            RayGui.GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE, 64);
            RayGui.GuiLabel(new Rectangle(540, 20, 300, 64), "Astride");

            RayGui.GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE, 40);
            if (RayGui.GuiButton(new(340, 140, 600, 100), "Freeplay") == 1) { }
            RayGui.GuiButton(new(340, 270, 600, 100), "Story Mode (In Works)");
            RayGui.GuiButton(new(340, 400, 285, 100), "Options");
            if (RayGui.GuiButton(new(655, 400, 285, 100), "Quit") == 1)
            {
                Environment.Exit(0);
            }

        }
        internal override Scene SetNextScene()
        {
            return null;
        }

        //internal override void InputUpdate(params ControlListener[] inputManagers)
        //{
        //    base.InputUpdate(inputManagers);
        //}
    }
}
