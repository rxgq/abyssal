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
    private bool IsMouseDown = false;

    private Panel panel1;
    private Label shapeCount;
    private Button playButton;
    private CheckBox colliderFrictionCheckbox;
    private ListBox spriteListBox;

    private void InitializeComponent()
    {
        panel1 = new Panel();
        colliderFrictionCheckbox = new CheckBox();
        playButton = new Button();
        shapeCount = new Label();
        spriteListBox = new ListBox();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // panel1
        // 
        panel1.BackColor = Color.FromArgb(50, 50, 50);
        panel1.Controls.Add(colliderFrictionCheckbox);
        panel1.Controls.Add(playButton);
        panel1.Controls.Add(shapeCount);
        panel1.Controls.Add(spriteListBox);
        panel1.Location = new Point(0, 0);
        panel1.Name = "panel1";
        panel1.Size = new Size(265, 722);
        panel1.TabIndex = 0;
        // 
        // colliderFrictionCheckbox
        // 
        colliderFrictionCheckbox.AutoSize = true;
        colliderFrictionCheckbox.ForeColor = SystemColors.ControlLightLight;
        colliderFrictionCheckbox.Location = new Point(12, 307);
        colliderFrictionCheckbox.Name = "colliderFrictionCheckbox";
        colliderFrictionCheckbox.Size = new Size(124, 24);
        colliderFrictionCheckbox.TabIndex = 4;
        colliderFrictionCheckbox.TabStop = false;
        colliderFrictionCheckbox.Text = "Player Friction";
        colliderFrictionCheckbox.UseVisualStyleBackColor = true;
        colliderFrictionCheckbox.CheckedChanged += colliderFrictionCheckbox_CheckedChanged;
        // 
        // playButton
        // 
        playButton.Location = new Point(12, 12);
        playButton.Name = "playButton";
        playButton.Size = new Size(94, 29);
        playButton.TabIndex = 3;
        playButton.TabStop = false;
        playButton.Text = "Play";
        playButton.UseVisualStyleBackColor = true;
        playButton.Click += playButton_Click;
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
        spriteListBox.Location = new Point(12, 82);
        spriteListBox.Name = "spriteListBox";
        spriteListBox.Size = new Size(111, 204);
        spriteListBox.TabIndex = 1;
        spriteListBox.TabStop = false;
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
        MouseDown += Canvas_MouseDown;
        MouseUp += Canvas_MouseUp;

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


        Shape2D? shapeToRemove = Engine.Shapes.FirstOrDefault(shape =>
            shape.Position.X == position.X && shape.Position.Y == position.Y);

        if (shapeToRemove is not null)
            Engine.UnregisterShape(shapeToRemove);

        else
            new Shape2D(position, scale, "shape");
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

    private void Canvas_MouseDown(object sender, MouseEventArgs e)
    {
        IsMouseDown = true;
    }

    private void Canvas_MouseUp(object sender, MouseEventArgs e)
    {
        IsMouseDown = false;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        Color gridColor = Game.Play ? Color.FromArgb(50, Color.White) : GridColor;
        using Pen gridPen = new(gridColor);

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

    private void colliderFrictionCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox checkBox = (CheckBox)sender;
        Game.Player.ColliderFriction = checkBox.Checked;

        Focus();
        ActiveControl = null;
    }
}
