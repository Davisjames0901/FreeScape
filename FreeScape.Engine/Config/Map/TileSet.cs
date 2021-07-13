using FreeScape.Engine.Config.TileSet;
using System.Collections.Generic;

namespace FreeScape.Engine.Config.Map
{
    public class TileSet
    {
        public string Name { get; set; }
        public int Columns { get; set; }
        public int FirstGid { get; set; }
        public string Image { get; set; }
        public int ImageHeight { get; set; }
        public int ImageWidth { get; set; }
        public int Margin { get; set; }
        public string Spacing { get; set; }
        public string TileCount { get; set; }
        public string TileHeight { get; set; }
        public string TileWidth { get; set; }
        public List<TileSetTile> Tiles { get; set; }
    }
}