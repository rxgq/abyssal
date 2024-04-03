namespace engine;

internal class Vector2(int x, int y)
{
    public int X { get; set; } = x;
    public int Y { get; set; } = y;

    public static Vector2 Zero()
    {
        return new Vector2(0, 1);
    }
}