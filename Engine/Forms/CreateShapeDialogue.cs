namespace engine;

public partial class CreateShapeDialogue : Form
{
    public CreateShapeDialogue()
    {
        InitializeComponent();
    }

    private void createShapeButton_Click(object sender, EventArgs e)
    {
        int XPosition = int.Parse(XPositionBox.Text);
        int YPosition = int.Parse(YPositionBox.Text);

        int XScale = int.Parse(XScaleBox.Text);
        int YScale = int.Parse(YScaleBox.Text);

        Vector2 position = new(XPosition, YPosition);
        Vector2 scale = new(XScale, YScale);

        Shape2D shape = new(position, scale, "test");
    }
}
