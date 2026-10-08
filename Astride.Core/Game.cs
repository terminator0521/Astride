using Raylib_cs;
using raygui_cs;
using System.Numerics;

namespace Astride.Core
{
    public class Game
    {
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
        }

        public void Update()
        {
            Raygui.GuiSetStyle((int)GuiControl.DEFAULT, (int)GuiDefaultProperty.TEXT_SIZE, 32);
            Raygui.GuiDrawText("Test", new Rectangle(10, 10, 100, 30), 30, Color.Black);
        }
        public void Render()
        {

        }
    }
}
