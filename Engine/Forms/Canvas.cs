namespace engine;

internal class Canvas : Form
{
    public Color CanvasColour;

    private int GridSize = 10;
    private Color GridColor = Color.LightGray;
    private void InitializeComponent()
    {
        panel1 = new Panel();
        createShapeButton = new Button();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // panel1
        // 
        panel1.BackColor = Color.FromArgb(50, 50, 50);
        panel1.Controls.Add(createShapeButton);
        panel1.Location = new Point(0, 0);
        panel1.Name = "panel1";
        panel1.Size = new Size(265, 722);
        panel1.TabIndex = 0;
        // 
        // createShapeButton
        // 
        createShapeButton.Location = new Point(12, 12);
        createShapeButton.Name = "createShapeButton";
        createShapeButton.Size = new Size(111, 29);
        createShapeButton.TabIndex = 0;
        createShapeButton.Text = "Create Shape";
        createShapeButton.UseVisualStyleBackColor = true;
        createShapeButton.Click += createShapeButton_Click;
        createShapeButton.TabStop = false;
        // 
        // Canvas
        // 
        ClientSize = new Size(1062, 721);
        Controls.Add(panel1);
        Name = "Canvas";
        panel1.ResumeLayout(false);
        ResumeLayout(false);
    }

    public Canvas()
    {
        InitializeComponent();
        DoubleBuffered = true;
        MouseClick += Canvas_MouseClick;
    }

    private Panel panel1;
    private Button createShapeButton;

    private void createShapeButton_Click(object sender, EventArgs e)
    {
        using (CreateShapeDialogue createShapeDialogue = new())
        {
            DialogResult result = createShapeDialogue.ShowDialog(this);
        }

        Focus();
        ActiveControl = null;
    }

    private void Canvas_MouseClick(object sender, MouseEventArgs e)
    {
        Point point = e.Location;

        Vector2 scale = new(10, 10);

        int X = (point.X / GridSize) * GridSize;
        int Y = (point.Y / GridSize) * GridSize;

        Vector2 position = new(X, Y);

        new Shape2D(position, scale, "test");
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        using Pen gridPen = new(GridColor);

        for (int x = 0; x < ClientSize.Width; x += GridSize)
            e.Graphics.DrawLine(gridPen, x, 0, x, ClientSize.Height);

        for (int y = 0; y < ClientSize.Height; y += GridSize)
            e.Graphics.DrawLine(gridPen, 0, y, ClientSize.Width, y);
    }
}
