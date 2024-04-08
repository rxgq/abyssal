namespace engine;

internal class Canvas : Form
{
    public Color CanvasColour;

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
}
