using Microsoft.VisualBasic.Devices;

namespace engine;

internal class Game : Engine
{
    Shape2D Player;

    public override void OnLoad()
    {
        Player = new(new Vector2(10, 10), new Vector2(10, 10), "player");
    }

    public override void OnDraw()
    {

    }

    public override void OnUpdate()
    {
    }

    public override void GetKeyDown(KeyEventArgs e)
    {
        switch (e.KeyCode) 
        { 
            case Keys.W:
            case Keys.Up:
                Player.Position.Y -= 10;
                break;

            case Keys.A:
            case Keys.Left:
                Player.Position.X -= 10;
                break;

            case Keys.S:
            case Keys.Down:
                Player.Position.Y += 10;
                break;

            case Keys.D:
            case Keys.Right:
                Player.Position.X += 10;
                break;
        }
    }

    public override void GetKeyUp(KeyEventArgs e)
    {

    }
}
