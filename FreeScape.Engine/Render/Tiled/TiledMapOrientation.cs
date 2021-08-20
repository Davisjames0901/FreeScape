using System.Text.Json.Serialization;

namespace FreeScape.Engine.Render.Tiled
{
    public enum TiledMapOrientation
    {
        [JsonPropertyName("othogonal")]
        Othogonal,
        [JsonPropertyName("isometric")]
        Isometric,
        [JsonPropertyName("staggered")]
        Staggered,
        [JsonPropertyName("hexagonal")]
        Hexagonal
    }
}