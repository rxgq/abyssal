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

    private bool RemoveMode = false;
    private string SelectedShapeItem = "";

    private Panel panel1;
    private Label shapeCount;
    private Button playButton;
    private CheckBox colliderFrictionCheckbox;
    private Button removeModeButton;
    private ComboBox shapeDropdown;
    private Button saveButton;
    private ListBox spriteListBox;

    private void InitializeComponent()
    {
        panel1 = new Panel();
        shapeDropdown = new ComboBox();
        removeModeButton = new Button();
        colliderFrictionCheckbox = new CheckBox();
        playButton = new Button();
        shapeCount = new Label();
        spriteListBox = new ListBox();
        saveButton = new Button();
        panel1.SuspendLayout();
        SuspendLayout();
        // 
        // panel1
        // 
        panel1.BackColor = Color.FromArgb(50, 50, 50);
        panel1.Controls.Add(saveButton);
        panel1.Controls.Add(shapeDropdown);
        panel1.Controls.Add(removeModeButton);
        panel1.Controls.Add(colliderFrictionCheckbox);
        panel1.Controls.Add(playButton);
        panel1.Controls.Add(shapeCount);
        panel1.Controls.Add(spriteListBox);
        panel1.Location = new Point(0, 0);
        panel1.Name = "panel1";
        panel1.Size = new Size(265, 722);
        panel1.TabIndex = 0;
        // 
        // shapeDropdown
        // 
        shapeDropdown.FormattingEnabled = true;
        shapeDropdown.Items.AddRange(new object[] { "Player", "Shape" });
        shapeDropdown.Location = new Point(12, 347);
        shapeDropdown.Name = "shapeDropdown";
        shapeDropdown.Size = new Size(151, 28);
        shapeDropdown.TabIndex = 1;
        shapeDropdown.TabStop = false;
        shapeDropdown.SelectedIndexChanged += shapeDropdown_SelectedIndexChanged;
        // 
        // removeModeButton
        // 
        removeModeButton.Location = new Point(12, 301);
        removeModeButton.Name = "removeModeButton";
        removeModeButton.Size = new Size(105, 29);
        removeModeButton.TabIndex = 1;
        removeModeButton.Text = "Remove: OFF";
        removeModeButton.UseVisualStyleBackColor = true;
        removeModeButton.Click += Remove_Click;
        // 
        // colliderFrictionCheckbox
        // 
        colliderFrictionCheckbox.AutoSize = true;
        colliderFrictionCheckbox.ForeColor = SystemColors.ControlLightLight;
        colliderFrictionCheckbox.Location = new Point(12, 685);
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
        shapeCount.Location = new Point(12, 58);
        shapeCount.Name = "shapeCount";
        shapeCount.Size = new Size(17, 20);
        shapeCount.TabIndex = 2;
        shapeCount.Text = "0";
        // 
        // spriteListBox
        // 
        spriteListBox.FormattingEnabled = true;
        spriteListBox.ItemHeight = 20;
        spriteListBox.Location = new Point(12, 81);
        spriteListBox.Name = "spriteListBox";
        spriteListBox.Size = new Size(147, 204);
        spriteListBox.TabIndex = 1;
        spriteListBox.TabStop = false;
        // 
        // saveButton
        // 
        saveButton.Location = new Point(155, 680);
        saveButton.Name = "saveButton";
        saveButton.Size = new Size(94, 29);
        saveButton.TabIndex = 5;
        saveButton.Text = "Save";
        saveButton.UseVisualStyleBackColor = true;
        saveButton.Click += saveButton_Click;
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
        spriteListBox.MouseDown += SpriteListBox_MouseDown;

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
        shapeCount.Text = "Objects: " + spriteListBox.Items.Count.ToString();
    }

    private void Canvas_MouseClick(object sender, MouseEventArgs e)
    {
        if (Game.Play) return;

        Point point = e.Location;

        Vector2 scale = new(10, 10);

        int X = (point.X / GridSize) * GridSize;
        int Y = (point.Y / GridSize) * GridSize;

        Vector2 position = new(X, Y);

        Shape2D? potentialShapeOverlap = Engine.Shapes.FirstOrDefault(shape =>
            shape.Position.X == position.X && shape.Position.Y == position.Y);

        Sprite2D? potentialSpriteOverlap = Engine.Sprites.FirstOrDefault(shape =>
            shape.Position.X == position.X && shape.Position.Y == position.Y);

        if (RemoveMode)
        {
            if (potentialShapeOverlap is not null)
                Engine.UnregisterShape(potentialShapeOverlap);

            if (potentialSpriteOverlap is not null)
                Engine.UnregisterSprite(potentialSpriteOverlap);
        }

        else if (potentialShapeOverlap is null)
        {
            if (SelectedShapeItem == "Player")
            {
                if (Game.Player is null)
                    Game.Player = new Player(position, scale, GetNextID("player"));
            }


            else if (SelectedShapeItem == "Shape")
                new Shape2D(position, scale, GetNextID("shape"));
        }
    }

    public string GetNextID(String defaultTag)
    {
        int largestShapeTag = -1;
        foreach (var shape in Engine.Shapes)
        {
            int shapeID;
            if (int.TryParse(shape.Tag.Substring(6), out shapeID))
            {
                if (shapeID > largestShapeTag)
                    largestShapeTag = shapeID;
            }
        }

        largestShapeTag++;

        return $"{defaultTag} {largestShapeTag}";
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

    public void playButton_Click(object sender, EventArgs e)
    {
        if (Game.Player is null) return;

        Game.Play = !Game.Play;
        Game.Player.Up = false;
        Game.Player.Down = false;
        Game.Player.Left = false;
        Game.Player.Right = false;

        playButton.Text = Game.Play ? "Playing..." : "Play";

        Game.Player.PlayPosition.X = Game.Player.Position.X;
        Game.Player.PlayPosition.Y = Game.Player.Position.Y;

        ToggleUIVisibility(Game.Play);

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

    private void Remove_Click(object sender, EventArgs e)
    {
        RemoveMode = !RemoveMode;
        removeModeButton.BackColor = RemoveMode ? Color.Gray : Color.White;
        removeModeButton.ForeColor = RemoveMode ? Color.White : Color.Black;
        removeModeButton.Text = RemoveMode ? "Remove: ON" : "Remove: OFF";

        removeModeButton.FlatAppearance.BorderColor = RemoveMode ? Color.Gray : Color.White;
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (keyData == Keys.Escape && Game.Play)
        {
            playButton_Click(null, EventArgs.Empty);
            return true;
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }

    public void ToggleUIVisibility(bool visible)
    {
        panel1.Visible = !visible;
        playButton.Visible = !visible;
        colliderFrictionCheckbox.Visible = !visible;
        removeModeButton.Visible = !visible;
        shapeCount.Visible = !visible;
        spriteListBox.Visible = !visible;
    }

    private void shapeDropdown_SelectedIndexChanged(object sender, EventArgs e)
    {
        ComboBox comboBox = (ComboBox)sender;
        SelectedShapeItem = comboBox.SelectedItem.ToString()!;
    }

    public void SpriteListBox_MouseDown(object sender, EventArgs e)
    {

    }

    private void saveButton_Click(object sender, EventArgs e)
    {
        FolderBrowserDialog folderBrowserDialog = new()
        {
            InitialDirectory = @"C:\"
        };

        DialogResult result = folderBrowserDialog.ShowDialog();

        if (result == DialogResult.OK)
        {
            string filePath = folderBrowserDialog.SelectedPath;
            Engine.SaveAsJson(filePath);
        }
    }
}
