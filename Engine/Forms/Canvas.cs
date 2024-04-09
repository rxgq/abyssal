using System.Windows.Forms;

namespace engine;

internal class Canvas : Form
{
    public Color CanvasColour;

    private readonly int GridSize = 10;
    private readonly Color GridColor = Color.LightGray;

    private Panel panel1;
    private Button createShapeButton;
    private ListBox spriteListBox;

    private void InitializeComponent()
    {
        panel1 = new Panel();
        createShapeButton = new Button();
        spriteListBox = new ListBox();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // panel1
        // 
        panel1.BackColor = Color.FromArgb(50, 50, 50);
        panel1.Controls.Add(spriteListBox);
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
        createShapeButton.TabStop = false;
        createShapeButton.Text = "Create Shape";
        createShapeButton.UseVisualStyleBackColor = true;
        createShapeButton.Click += createShapeButton_Click;
        // 
        // spriteListBox
        // 
        spriteListBox.FormattingEnabled = true;
        spriteListBox.ItemHeight = 20;
        spriteListBox.Location = new Point(12, 58);
        spriteListBox.Name = "spriteListBox";
        spriteListBox.Size = new Size(148, 204);
        spriteListBox.TabIndex = 1;
        spriteListBox.TabStop = false;
        Controls.Add(spriteListBox);
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

        Focus();
    }

    public void InitialiseListBoxItems() 
    {
        spriteListBox.Items.Clear();

        foreach (var shape in Engine.Shapes)
            spriteListBox.Items.Add(shape.Tag);

        spriteListBox.Refresh();
    }

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
