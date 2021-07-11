using System.Collections.Generic;
using SFML.Graphics;

namespace FreeScape.Engine.Config.TileSet
{
    public class CachedTileSet
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public Texture Sheet { get; set; }
        public Dictionary<uint, CachedTileSetTile> Tiles { get; set; }
    }
}