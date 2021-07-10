using System.Text.Json.Serialization;

namespace FreeScape.Engine.Config.Map
{
    public enum MapOrientation
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