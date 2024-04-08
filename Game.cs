using Microsoft.VisualBasic.Devices;
using System.Numerics;

namespace engine;

internal class Game : Engine
{
    Shape2D Player;

    bool Up = false;
    bool Down = false;
    bool Left = false;
    bool Right = false;

    public override void OnLoad()
    {
        Player = new(new Vector2(10, 10), new Vector2(10, 10), "player");
    }

    public override void OnDraw()
    {

    }

    public override void OnUpdate()
    {
        // Movement
        Player.Position.Y += (Up ? -1 : Down ? 1 : 0);
        Player.Position.X += (Left ? -1 : Right ? 1 : 0);
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
            Left = false;sssssssss

        else if (e.KeyCode == Keys.D)
            Right = false;
    }
}
