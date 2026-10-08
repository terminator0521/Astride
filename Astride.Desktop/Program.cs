using Astride.Core;
using raygui_cs;
using Raylib_cs;
using System.Numerics;

Raylib.InitWindow(1280, 720, "Astride");
var game = new Game(720, 720);
Raylib.SetTargetFPS(Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor()));

while (!Raylib.WindowShouldClose())
{
    //render game texture
    Raylib.BeginTextureMode(game.renderTexture);
    Raylib.ClearBackground(Color.Blank);
    game.Render();
    Raylib.EndTextureMode();

    //main game updates
    game.Update();
    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.White);
#if DEBUG
    Raylib.DrawLine((int)game.dest.X + (int)game.dest.Width, (int)game.dest.Y, (int)game.dest.X + (int)game.dest.Width, (int)game.dest.Y + (int)game.dest.Height, Color.Black);
#endif
    Raylib.DrawTexturePro(game.renderTexture.Texture, game.src, game.dest, Vector2.Zero, 0, Color.White);
    Raylib.EndDrawing();

    //reset gui
    Raygui.GuiLoadStyleDefault();
}