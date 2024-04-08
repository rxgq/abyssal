using System.Numerics;

namespace engine;

internal class Shape2D
{
    public Vector2 Position { get; set; }

    public Vector2 Scale { get; set; }

    public string Tag { get; set; }

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
