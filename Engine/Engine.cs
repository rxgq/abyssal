namespace engine;

internal abstract class Engine
{
    private readonly Vector2 ScreenSize = new(512, 512);
    private readonly Canvas Window;
    private readonly Thread GameLoopThread;
    public Color BackgroundColour = Color.White;

    public Engine()
    {
        Window = new Canvas()
        { 
            Size = new(ScreenSize.X, ScreenSize.Y), 
            Text = "New Game", 
        };

        Window.Paint += Renderer;

        GameLoopThread = new Thread(GameLoop);
        GameLoopThread.Start();

        Application.Run(Window);
    }

    void GameLoop()
    {
        OnLoad();

        while (GameLoopThread.IsAlive) 
        {
            try
            {

                OnDraw();
                Window.BeginInvoke((MethodInvoker)delegate { Window.Refresh(); });
                OnUpdate();

                Thread.Sleep(1);
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.ToString());
            }

        }
    }

    private void Renderer(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.Clear(BackgroundColour);
    }

    public abstract void OnLoad();

    public abstract void OnUpdate();

    public abstract void OnDraw();

}
