using System.Collections.Generic;
using SFML.Graphics;

namespace FreeScape.Engine.Config.TileSet
{
    public class CachedTileSet
    {
        public int Id { get; set; }
        public Texture Sheet { get; set; }
        public Dictionary<uint, CachedTileSetTile> Tiles { get; set; }
        public string Name { get; set; }
        public int Columns { get; set; }
        public int FirstGid { get; set; }
        public string Image { get; set; }
        public int ImageHeight { get; set; }
        public int ImageWidth { get; set; }
        public int Margin { get; set; }
        public int Spacing { get; set; }
        public int TileCount { get; set; }
        public int TileHeight { get; set; }
        public int TileWidth { get; set; }
    }
}