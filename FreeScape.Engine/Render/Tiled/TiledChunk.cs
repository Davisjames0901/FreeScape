using System.Collections.Generic;

namespace FreeScape.Engine.Render.Tiled
{
    public class TiledChunk
    {
        public List<uint> Data { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}