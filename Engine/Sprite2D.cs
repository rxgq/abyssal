namespace engine;

internal class Sprite2D
{
    public Vector2 Position { get; set; }
    public Vector2 Scale { get; set; }
    public string Tag { get; set; }

    public Sprite2D(Vector2 position, Vector2 scale, string tag)
    {
        Position = position;
        Scale = scale;
        Tag = tag;

        Engine.RegisterSprite(this);
    }

    public void DestroySelf()
    {
        Engine.UnregisterSprite(this);
    }

    public bool IsColliding(Vector2 newPosition)
    {
        foreach (Shape2D shape in Engine.Shapes)
        {
            if (IsCollidingWith(shape.Position, shape.Scale, newPosition))
                return true;
        }

        foreach (Sprite2D sprite in Engine.Sprites)
        {
            if (sprite != this && IsCollidingWith(sprite.Position, sprite.Scale, newPosition))
                return true;
        }

        return false;
    }

    private bool IsCollidingWith(Vector2 shapePosition, Vector2 shapeScale, Vector2 newPosition)
    {
        if (newPosition.X < shapePosition.X + shapeScale.X &&
            newPosition.X + Scale.X > shapePosition.X &&
            newPosition.Y < shapePosition.Y + shapeScale.Y &&
            newPosition.Y + Scale.Y > shapePosition.Y)
        {
            return true;
        }

        return false;
    }
}
