using System.Collections.Generic;

namespace FreeScape.Engine.Render.Tiled
{
    public class TiledTileSet
    {
        public string Image { get; set; }
        public int FirstGid { get; set; }
        public int Columns { get; set; }
        public int ImageHeight { get; set; }
        public int ImageWidth { get; set; }
        public int Margin { get; set; }
        public string Name { get; set; }
        public int Spacing { get; set; }
        public int TileCount { get; set; }
        public string TiledVersion { get; set; }
        public int TileHeight { get; set; }
        public int TileWidth { get; set; }
        public string Type { get; set; }
        public List<TiledTile> Tiles { get; set; }
    }
}