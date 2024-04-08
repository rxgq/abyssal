namespace engine;

public partial class CreateShapeDialogue : Form
{
    public CreateShapeDialogue()
    {
        InitializeComponent();
    }

    private void createShapeButton_Click(object sender, EventArgs e)
    {
        Vector2 position = new(450, 450);
        Vector2 scale = new(450, 450);

        Shape2D shape = new(position, scale, "test");
    }
}
