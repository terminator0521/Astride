using Astride.Core;
using System.Numerics;
using ZeroElectric.Vinculum;

Raylib.InitWindow(1280, 720, "Astride");
var game = new Game(720, 720);
Raylib.SetTargetFPS(Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor()));

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
    //Raylib.DrawLine((int)game.dest.X + (int)game.dest.Width, (int)game.dest.Y, (int)game.dest.X + (int)game.dest.Width, (int)game.dest.Y + (int)game.dest.Height, Color.Black);

    Raylib.DrawText("FPS: " + Raylib.GetFPS(), 10, 10, 30, Raylib.BLACK);
    Raylib.DrawText("MousePos: " + Raylib.GetMousePosition(), 10, 50, 30, Raylib.BLACK);
#endif
    //draw game render texture to screen
    Raylib.DrawTexturePro(game.renderTexture.texture, game.src, game.dest, Vector2.Zero, 0, Raylib.WHITE);

    //raygui updates
    game.UI();
    Raylib.EndDrawing();

    //reset gui
    RayGui.GuiLoadStyleDefault();
}