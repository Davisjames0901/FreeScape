using System.Collections.Generic;
using FreeScape.Engine.Config.Map;

namespace FreeScape.Engine.Config.TileSet
{
    public class TileSetTile
    {
        public uint Id { get; set; }
        public List<MapProperties> Properties { get; set; }
        public TileObjectGroup ObjectGroup { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public float ImageWidth { get; set; }
        public float ImageHeight { get; set; }
        public List<AnimationFrameInfo> Animation { get; set; }
    }
}