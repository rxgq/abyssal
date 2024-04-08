using Microsoft.VisualBasic.Devices;
using System.Numerics;

namespace engine;

internal class Game : Engine
{
    Sprite2D Player;

    bool Up = false;
    bool Down = false;
    bool Left = false;
    bool Right = false;

    public override void OnLoad()
    {
        Player = new(new Vector2(10, 10), new Vector2(10, 10), "player");

        Shape2D sprite = new(new Vector2(40, 40), new Vector2(10, 10), "s1");
        Shape2D sprite2 = new(new Vector2(60, 60), new Vector2(10, 10), "s2");
    }

    public override void OnDraw()
    {

    }

    public override void OnUpdate()
    {
        Vector2 newPosition = new(Player.Position.X + (Left ? -1 : Right ? 1 : 0), Player.Position.Y + (Up ? -1 : Down ? 1 : 0));

        if (!Player.IsColliding(newPosition))
            Player.Position = newPosition;
    }

    public override void GetKeyDown(KeyEventArgs e)
    {
        if (e.KeyCode == Keys.W)
            Up = true;

        else if (e.KeyCode == Keys.S)
            Down = true;

        else if (e.KeyCode == Keys.A)
            Left = true;

        else if (e.KeyCode == Keys.D)
            Right = true;
    }

    public override void GetKeyUp(KeyEventArgs e)
    {
        if (e.KeyCode == Keys.W)
            Up = false;

        else if (e.KeyCode == Keys.S)
            Down = false;

        else if (e.KeyCode == Keys.A)
            Left = false;

        else if (e.KeyCode == Keys.D)
            Right = false;
    }
}
