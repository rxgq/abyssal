namespace engine;

internal class Shape2D
{
    public Vector2 Position { get; }
    public Vector2 Scale { get; }
    public string Tag { get; }

    public Shape2D(Vector2 position, Vector2 scale, string tag)
    {
        Position = position;
        Scale = scale;
        Tag = tag;

        Engine.RegisterShape(this);
    }

    public void DestroySelf() 
    {
        Engine.UnregisterShape(this);
    }
}
