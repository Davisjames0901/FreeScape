using System.Collections.Generic;

namespace FreeScape.Engine.Render.Tiled
{
    public class TiledTile
    {
        public uint Id { get; set; }
        public List<TiledProperty> Properties { get; set; }
        public TiledObjectGroup ObjectGroup { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public float ImageWidth { get; set; }
        public float ImageHeight { get; set; }
        public List<TiledAnimationFrame> Animation { get; set; }
    }
}