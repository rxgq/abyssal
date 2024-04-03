namespace engine;

internal class Engine
{
    private readonly Vector2 ScreenSize = new(512, 512);
    private readonly Canvas Window;

    public Engine()
    {
        Window = new Canvas 
        { 
            Size = new(ScreenSize.X, ScreenSize.Y), 
            Text = "New Game", 
        };

        Application.Run(Window);
    }
}
