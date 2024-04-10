using engine;
using Newtonsoft.Json;

internal class Player(Vector2 position, Vector2 scale, string tag) : Sprite2D(position, scale, tag)
{
    public bool Up = false;
    public bool Down = false;
    public bool Left = false;
    public bool Right = false;

    [JsonProperty]
    public int Velocity = 2;

    [JsonProperty]
    public bool ColliderFriction = false;
}