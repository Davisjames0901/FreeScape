using System.Collections.Generic;

namespace FreeScape.Engine.Config.Map
{
    public class MapInfo
    {
        public string BackgroundColor { get; set; }
        public int CompressionLevel { get; set; }
        public int Height { get; set; }
        public int HexSideLengh { get; set; }
        public bool Infinite { get; set; }
        public List<MapLayer> Layers { get; set; }
        public int NextLayerId { get; set; }
        public int NextObjectId { get; set; }
        //public MapOrientation Orientation { get; set; }
        public List<MapProperties> Properties { get; set; }
        public string RenderOrder { get; set; }
        public MapStaggerAxis StaggerAxis { get; set; }
        public MapStaggerIndex StaggerIndex { get; set; }
        public string TiledVersion { get; set; }
        public int TileHeight { get; set; }
        public List<TileSet> TileSets { get; set; }
        public int TileWidth { get; set; }
        public string Type { get; set; }
        public string Version { get; set; }
        public int Width { get; set; }
    }
}