using System.Text.Json.Serialization;

namespace FreeScape.Engine.Render.Tiled
{
    public enum TiledMapRenderOrder
    {
        [JsonPropertyName("right-down")]
        RightDown,
        [JsonPropertyName("right-up")]
        RightUp,
        [JsonPropertyName("left-down")]
        LeftDown,
        [JsonPropertyName("left-up")]
        LeftUp
    }
}