using System.Numerics;
using System.Threading.Tasks;
using ZeroElectric.Vinculum;
using Astride.Core;

public partial class AstrideWeb
{
    public static async Task Main()
    {
        Raylib.InitWindow(1280, 720, "Astride");
        var game = new Game(1100, 720);
        Raylib.SetTargetFPS(Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor()));
        Raylib.SetExitKey(KeyboardKey.KEY_NULL);

        while (!Raylib.WindowShouldClose())
        {
            //render game texture
            Raylib.BeginTextureMode(game.renderTexture);
            Raylib.ClearBackground(Raylib.BLANK);
            game.Render();
            Raylib.EndTextureMode();

            //main game updates
            game.Update();
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.WHITE);
#if DEBUG
            Raylib.DrawLine((int)game.dest.X + (int)game.dest.Width, (int)game.dest.Y, (int)game.dest.X + (int)game.dest.Width, (int)game.dest.Y + (int)game.dest.Height, Raylib.BLACK);

            Raylib.DrawText("FPS: " + Raylib.GetFPS(), 10, 10, 20, Raylib.BLACK);
            Raylib.DrawText("MousePos: " + Raylib.GetMousePosition(), 10, 30, 20, Raylib.BLACK);
#endif
            //draw game render texture to screen
            Raylib.DrawTexturePro(game.renderTexture.texture, game.src, game.dest, Vector2.Zero, 0, Raylib.WHITE);

            //raygui updates
            game.UI();
            Raylib.EndDrawing();

            //reset gui
            RayGui.GuiLoadStyleDefault();

            await Task.Yield();
        }
    }
}