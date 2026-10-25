using Astride.Core;
using System.Numerics;
using ZeroElectric.Vinculum;

Raylib.InitWindow(1280, 720, "Astride"); //create game window
var game = new Game(1100, 720); //create game
Raylib.SetTargetFPS(Raylib.GetMonitorRefreshRate(Raylib.GetCurrentMonitor())); //set fps based on monitor refresh rate

//while window is still running
while (!Raylib.WindowShouldClose())
{
    //update game render texture
    Raylib.BeginTextureMode(game.renderTexture);
    Raylib.ClearBackground(Raylib.BLANK); //reset background
    game.Render(); //render game
    Raylib.EndTextureMode();

    //main game updates
    game.Update();
    Raylib.BeginDrawing(); //begin main gpu buffer
    Raylib.ClearBackground(Raylib.WHITE);
    //draw game render texture to screen
    Raylib.DrawTexturePro(game.renderTexture.texture, game.src, game.dest, Vector2.Zero, 0, Raylib.WHITE); //render game renderer

    //raygui updates
    game.UI();

#if DEBUG
    Raylib.DrawLine((int)game.dest.X + (int)game.dest.Width, (int)game.dest.Y, (int)game.dest.X + (int)game.dest.Width, (int)game.dest.Y + (int)game.dest.Height, Raylib.BLACK); //draw line that divides main game renderer and UI reserved space
    Raylib.DrawText("FPS: " + Raylib.GetFPS(), 10, 10, 20, Raylib.BLACK); //write fps to screen
    Raylib.DrawText("MousePos: " + Raylib.GetMousePosition(), 10, 30, 20, Raylib.BLACK); //write mouse pos to screen
#endif
    Raylib.EndDrawing(); //end main gpu buffer

    //reset gui
    RayGui.GuiLoadStyleDefault();
}

//stop audio
Raylib.CloseAudioDevice();

//stop window
Raylib.CloseWindow();