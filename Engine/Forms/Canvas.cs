using System.Drawing;
using System.Windows.Forms;

namespace engine;

internal class Canvas : Form
{
    public Color CanvasColour;

    private readonly int GridSize = 10;
    private readonly Color GridColor = Color.LightGray;
    private Vector2 HoverPosition;
    private bool ShowHoverBox;

    private Panel panel1;
    private Label shapeCount;
    private Button playButton;
    private ListBox spriteListBox;

    private void InitializeComponent()
    {
        panel1 = new Panel();
        shapeCount = new Label();
        spriteListBox = new ListBox();
        playButton = new Button();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // panel1
        // 
        panel1.BackColor = Color.FromArgb(50, 50, 50);
        panel1.Controls.Add(playButton);
        panel1.Controls.Add(shapeCount);
        panel1.Controls.Add(spriteListBox);
        panel1.Location = new Point(0, 0);
        panel1.Name = "panel1";
        panel1.Size = new Size(265, 722);
        panel1.TabIndex = 0;
        // 
        // shapeCount
        // 
        shapeCount.AutoSize = true;
        shapeCount.ForeColor = Color.White;
        shapeCount.Location = new Point(12, 60);
        shapeCount.Name = "shapeCount";
        shapeCount.Size = new Size(0, 20);
        shapeCount.TabIndex = 2;
        // 
        // spriteListBox
        // 
        spriteListBox.FormattingEnabled = true;
        spriteListBox.ItemHeight = 20;
        spriteListBox.Location = new Point(12, 83);
        spriteListBox.Name = "spriteListBox";
        spriteListBox.Size = new Size(111, 204);
        spriteListBox.TabIndex = 1;
        spriteListBox.TabStop = false;
        // 
        // playButton
        // 
        playButton.Location = new Point(12, 12);
        playButton.Name = "playButton";
        playButton.Size = new Size(94, 29);
        playButton.TabIndex = 3;
        playButton.Text = "Play";
        playButton.UseVisualStyleBackColor = true;
        playButton.Click += playButton_Click;
        playButton.TabStop = false;
        // 
        // Canvas
        // 
        ClientSize = new Size(1062, 721);
        Controls.Add(panel1);
        Name = "Canvas";
        panel1.ResumeLayout(false);
        panel1.PerformLayout();
        ResumeLayout(false);
    }

    public Canvas()
    {
        InitializeComponent();

        DoubleBuffered = true;
        MouseClick += Canvas_MouseClick;
        MouseMove += Canvas_MouseMove;

        Focus();
    }

    public void UpdateListBoxItems()
    {
        spriteListBox.Items.Clear();

        foreach (var shape in Engine.Shapes)
            spriteListBox.Items.Add($"{shape.GetType().Name}: {shape.Tag}");

        foreach (var sprite in Engine.Sprites)
            spriteListBox.Items.Add($"{sprite.GetType().Name}: {sprite.Tag}");

        spriteListBox.Refresh();
        shapeCount.Text = "Sprites: " + spriteListBox.Items.Count.ToString();
    }

    private void Canvas_MouseClick(object sender, MouseEventArgs e)
    {
        if (Game.Play) return; 

        Point point = e.Location;

        Vector2 scale = new(10, 10);

        int X = (point.X / GridSize) * GridSize;
        int Y = (point.Y / GridSize) * GridSize;

        Vector2 position = new(X, Y);

        new Shape2D(position, scale, "test");
    }

    private void Canvas_MouseMove(object sender, MouseEventArgs e) 
    {
        if (Game.Play) return;
        

        int X = (e.X / GridSize) * GridSize;
        int Y = (e.Y / GridSize) * GridSize;
        
        HoverPosition = new(X, Y);      

        ShowHoverBox = true;
        Refresh();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        using Pen gridPen = new(GridColor);

        for (int x = 0; x < ClientSize.Width; x += GridSize)
            e.Graphics.DrawLine(gridPen, x, 0, x, ClientSize.Height);

        for (int y = 0; y < ClientSize.Height; y += GridSize)
            e.Graphics.DrawLine(gridPen, 0, y, ClientSize.Width, y);

        if (ShowHoverBox && HoverPosition != null)
        {
            using Brush hoverBrush = new SolidBrush(Color.FromArgb(100, Color.Gray));
            e.Graphics.FillRectangle(hoverBrush, HoverPosition.X, HoverPosition.Y, GridSize, GridSize);
        }
    }

    private void playButton_Click(object sender, EventArgs e)
    {
        Game.Play = !Game.Play;
        Game.Player.Up = false;
        Game.Player.Down = false;
        Game.Player.Left = false;
        Game.Player.Right = false;

        playButton.Text = Game.Play ? "Playing..." : "Play";

        Game.Player.PlayPosition.X = Game.Player.Position.X;
        Game.Player.PlayPosition.Y = Game.Player.Position.Y;

        Focus();
        ActiveControl = null;
    }
}
