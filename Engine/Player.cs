using engine;

internal class Player(Vector2 position, Vector2 scale, string tag) : Sprite2D(position, scale, tag)
{
    public bool Up = false;
    public bool Down = false;
    public bool Left = false;
    public bool Right = false;

    public int Velocity = 2;
}