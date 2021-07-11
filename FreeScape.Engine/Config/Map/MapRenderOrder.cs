using System.Text.Json.Serialization;

namespace FreeScape.Engine.Config.Map
{
    public enum MapRenderOrder
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