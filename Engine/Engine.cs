namespace engine;

internal abstract class Engine
{
    private readonly Vector2 ScreenSize = new(1080, 768);
    private readonly Canvas Window;
    public readonly Color BackgroundColour = Color.White;

    private readonly Thread GameLoopThread;

    public static List<Shape2D> Shapes = new();
    public static List<Sprite2D> Sprites = new();

    public Engine()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

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

    private void Renderer(object sender, PaintEventArgs e)
    {
        Graphics g = e.Graphics;
        //g.Clear(BackgroundColour);

        foreach (Shape2D shape in Shapes) 
            g.FillRectangle(new SolidBrush(Color.Red), shape.Position.X, shape.Position.Y, shape.Scale.X, shape.Scale.Y);

        foreach (Sprite2D sprite in Sprites)
            g.FillRectangle(new SolidBrush(Color.Blue), sprite.Position.X, sprite.Position.Y, sprite.Scale.X, sprite.Scale.Y);
    }

    public static void RegisterShape(Shape2D shape)
    {
        Shapes.Add(shape);
        UpdateListBoxItems();
    }

    public static void UnregisterShape(Shape2D shape)
    {
        Shapes.Remove(shape);
        UpdateListBoxItems();
    }

    public static void UpdateListBoxItems()
    {
        if (Application.OpenForms.Count > 0)
        {
            Form canvasForm = Application.OpenForms[0];
            if (canvasForm is Canvas canvas)
            {
                canvas.UpdateListBoxItems();
            }
        }
    }

    public static void RegisterSprite(Sprite2D sprite) => Sprites.Add(sprite);
    public static void UnregisterSprite(Sprite2D sprite) => Sprites.Remove(sprite);

    public void Window_KeyDown(object sender, KeyEventArgs e) => GetKeyDown(e); 
    public void Window_KeyUp(object sender, KeyEventArgs e) => GetKeyUp(e);
 
    public abstract void OnLoad();
    public abstract void OnUpdate();
    public abstract void OnDraw();
    public abstract void GetKeyDown(KeyEventArgs e);
    public abstract void GetKeyUp(KeyEventArgs e);
}
