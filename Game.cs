using Microsoft.VisualBasic.Devices;
using System.Numerics;

namespace engine;

internal class Game : Engine
{
    Player Player;

    public override void OnLoad()
    {
        Player = new(new Vector2(400, 400), new Vector2(10, 10), "player");

        new Shape2D(new Vector2(500, 400), new Vector2(10, 10), "s1");
        new Shape2D(new Vector2(600, 600), new Vector2(10, 10), "s2");
    }

    public override void OnDraw()
    {

    }

    public override void OnUpdate()
    {
        Vector2 newPosition = new(
            Player.Position.X + (Player.Left ? -1 : Player.Right ? 1 : 0), 
            Player.Position.Y + (Player.Up ? -1 : Player.Down ? 1 : 0)
        );

        if (!Player.IsColliding(newPosition))
            Player.Position = newPosition;
    }

    public override void GetKeyDown(KeyEventArgs e)
    {
        if (e.KeyCode == Keys.W)
            Player.Up = true;

        else if (e.KeyCode == Keys.S)
            Player.Down = true;

        else if (e.KeyCode == Keys.A)
            Player.Left = true;

        else if (e.KeyCode == Keys.D)
            Player.Right = true;

        else if (e.Control && e.KeyCode == Keys.Z)
        {
            if (Shapes.Count > 0) 
                Shapes.RemoveAt(Shapes.Count - 1);
        }
    }

    public override void GetKeyUp(KeyEventArgs e)
    {
        if (e.KeyCode == Keys.W)
            Player.Up = false;

        else if (e.KeyCode == Keys.S)
            Player.Down = false;

        else if (e.KeyCode == Keys.A)
            Player.Left = false;

        else if (e.KeyCode == Keys.D)
            Player.Right = false;
    }
}
