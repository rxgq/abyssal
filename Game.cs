namespace engine;

internal class Game : Engine
{
    public static Player? Player;
    public static bool Play = false;

    public override void OnLoad()
    {

    }

    public override void OnDraw()
    {

    }

    public override void OnUpdate()
    {
        if (Player is null) return;

        Vector2 newPosition = new(
            Player.PlayPosition.X + (Player.Left ? -Player.Velocity : Player.Right ? Player.Velocity : 0),
            Player.PlayPosition.Y + (Player.Up ? -Player.Velocity : Player.Down ? Player.Velocity : 0)
        );

        if (!Player.IsColliding(newPosition))
            Player.PlayPosition = newPosition;

        else if (Player.ColliderFriction)
        {
            int newX = Player.PlayPosition.X;
            int newY = Player.PlayPosition.Y;

            if (Player.Left || Player.Right)
            {
                Vector2 tempPositionX = new(Player.Left ? newX - Player.Velocity + 1: newX + Player.Velocity - 1, Player.PlayPosition.Y);

                if (!Player.IsColliding(tempPositionX))
                    newX = tempPositionX.X;
            }

            if (Player.Up || Player.Down)
            {
                Vector2 tempPositionY = new(Player.PlayPosition.X, Player.Up ? newY - Player.Velocity + 1 : newY + Player.Velocity - 1);

                if (!Player.IsColliding(tempPositionY))
                    newY = tempPositionY.Y;
            }

            Player.PlayPosition = new Vector2(newX, newY);
        }
    }

    public override void GetKeyDown(KeyEventArgs e)
    {
        if (e.Control && e.KeyCode == Keys.Z)
        {
            if (Play) return;

            if (Shapes.Count > 0)
                UnregisterShape(Shapes[Shapes.Count - 1]);
        }

        if (!Play) return;

        if (e.KeyCode == Keys.W)
            Player.Up = true;

        else if (e.KeyCode == Keys.S)
            Player.Down = true;

        else if (e.KeyCode == Keys.A)
            Player.Left = true;

        else if (e.KeyCode == Keys.D)
            Player.Right = true;
    }

    public override void GetKeyUp(KeyEventArgs e)
    {
        if (!Play) return;

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
