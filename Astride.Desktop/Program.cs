using Astride.Core;
using System.Numerics;
using ZeroElectric.Vinculum;

Raylib.InitWindow(1280, 720, "Astride");
var game = new Game(1100, 720);
Raylib.SetTargetFPS(Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor()));
//Raylib.SetExitKey(KeyboardKey.KEY_NULL);

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
    //draw game render texture to screen
    Raylib.DrawTexturePro(game.renderTexture.texture, game.src, game.dest, Vector2.Zero, 0, Raylib.WHITE);

    //raygui updates
    game.UI();

#if DEBUG
    Raylib.DrawLine((int)game.dest.X + (int)game.dest.Width, (int)game.dest.Y, (int)game.dest.X + (int)game.dest.Width, (int)game.dest.Y + (int)game.dest.Height, Raylib.BLACK);
    Raylib.DrawCircle(1100 / 2, 720 / 2, 10, Raylib.RED);
    Raylib.DrawCircle(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2, 10, Raylib.BLUE);
    Raylib.DrawText("FPS: " + Raylib.GetFPS(), 10, 10, 20, Raylib.BLACK);
    Raylib.DrawText("MousePos: " + Raylib.GetMousePosition(), 10, 30, 20, Raylib.BLACK);
#endif
    Raylib.EndDrawing();

    //reset gui
    RayGui.GuiLoadStyleDefault();
}