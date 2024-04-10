using engine;
using Newtonsoft.Json;
using System.Runtime.Serialization;

internal class Player(Vector2 position, Vector2 scale, string tag) : Sprite2D(position, scale, tag)
{
    public bool Up { get; set; } = false;
    public bool Down { get; set; } = false;
    public bool Left { get; set; } = false;
    public bool Right { get; set; } = false;

    public int Velocity { get; set; } = 2;

    public bool ColliderFriction { get; set; } = false;
}