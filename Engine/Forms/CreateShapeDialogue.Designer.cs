namespace engine;

partial class CreateShapeDialogue
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
            components.Dispose();

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        XPositionBox = new TextBox();
        YPositionBox = new TextBox();
        label1 = new Label();
        label2 = new Label();
        createShapeButton = new Button();
        label3 = new Label();
        label4 = new Label();
        YScaleBox = new TextBox();
        XScaleBox = new TextBox();
        SuspendLayout();
        // 
        // XPositionBox
        // 
        XPositionBox.Location = new Point(99, 23);
        XPositionBox.Name = "XPositionBox";
        XPositionBox.Size = new Size(125, 27);
        XPositionBox.TabIndex = 0;
        // 
        // YPositionBox
        // 
        YPositionBox.Location = new Point(99, 66);
        YPositionBox.Name = "YPositionBox";
        YPositionBox.Size = new Size(125, 27);
        YPositionBox.TabIndex = 1;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(20, 66);
        label1.Name = "label1";
        label1.Size = new Size(73, 20);
        label1.TabIndex = 2;
        label1.Text = "Y Position";
        // 
        // label2
        // 
        label2.AutoSize = true;
        label2.Location = new Point(19, 26);
        label2.Name = "label2";
        label2.Size = new Size(74, 20);
        label2.TabIndex = 3;
        label2.Text = "X Position";
        // 
        // createShapeButton
        // 
        createShapeButton.Location = new Point(378, 168);
        createShapeButton.Name = "createShapeButton";
        createShapeButton.Size = new Size(92, 29);
        createShapeButton.TabIndex = 4;
        createShapeButton.Text = "Create";
        createShapeButton.UseVisualStyleBackColor = true;
        createShapeButton.Click += createShapeButton_Click;
        // 
        // label3
        // 
        label3.AutoSize = true;
        label3.Location = new Point(282, 26);
        label3.Name = "label3";
        label3.Size = new Size(57, 20);
        label3.TabIndex = 8;
        label3.Text = "X Scale";
        // 
        // label4
        // 
        label4.AutoSize = true;
        label4.Location = new Point(283, 66);
        label4.Name = "label4";
        label4.Size = new Size(56, 20);
        label4.TabIndex = 7;
        label4.Text = "Y Scale";
        // 
        // YScaleBox
        // 
        YScaleBox.Location = new Point(345, 63);
        YScaleBox.Name = "YScaleBox";
        YScaleBox.Size = new Size(125, 27);
        YScaleBox.TabIndex = 6;
        // 
        // XScaleBox
        // 
        XScaleBox.Location = new Point(345, 23);
        XScaleBox.Name = "XScaleBox";
        XScaleBox.Size = new Size(125, 27);
        XScaleBox.TabIndex = 5;
        // 
        // CreateShapeDialogue
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(494, 209);
        Controls.Add(label3);
        Controls.Add(label4);
        Controls.Add(YScaleBox);
        Controls.Add(XScaleBox);
        Controls.Add(createShapeButton);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(YPositionBox);
        Controls.Add(XPositionBox);
        Name = "CreateShapeDialogue";
        Text = "Create New Shape";
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;

    private TextBox XPositionBox;
    private TextBox YPositionBox;
    private TextBox YScaleBox;
    private TextBox XScaleBox;

    private Button createShapeButton;
}