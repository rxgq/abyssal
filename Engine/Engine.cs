namespace engine;

internal abstract class Engine
{
    private readonly Vector2 ScreenSize = new(512, 512);
    private readonly Canvas Window;
    private readonly Thread GameLoopThread;
    public Color BackgroundColour = Color.White;

    public static List<Shape2D> Shapes = new();

    public Engine()
    {
        Window = new Canvas()
        { 
            Size = new(ScreenSize.X, ScreenSize.Y), 
            Text = "New Game", 
        };

        Window.Paint += Renderer;

        Window.KeyDown += Window_KeyDown;
        Window.KeyUp += Window_KeyUp;

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

    public void Window_KeyDown(object sender, KeyEventArgs e)
    {
        GetKeyDown(e);
    }

    public void Window_KeyUp(object sender, KeyEventArgs e)
    {
        GetKeyUp(e);
    }

    public static void RegisterShape(Shape2D shape) 
    {
        Shapes.Add(shape);   
    }
    public static void UnregisterShape(Shape2D shape)
    {
        Shapes.Remove(shape);
    }

    private void Renderer(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        g.Clear(BackgroundColour);

        foreach (Shape2D shape in Shapes) 
        {
            g.FillRectangle(new SolidBrush(Color.Red), shape.Position.X, shape.Position.Y, shape.Scale.X, shape.Scale.Y);
        }
    }

    public abstract void OnLoad();

    public abstract void OnUpdate();

    public abstract void OnDraw();


    public abstract void GetKeyDown(KeyEventArgs e);

    public abstract void GetKeyUp(KeyEventArgs e);

}
