using System.Collections.Generic;

namespace FreeScape.Engine.Render.Tiled
{
    public class TiledMap
    {
        public string BackgroundColor { get; set; }
        public int CompressionLevel { get; set; }
        public int Height { get; set; }
        public int HexSideLenght { get; set; }
        public bool Infinite { get; set; }
        public List<TiledMapLayer> Layers { get; set; }
        public int NextLayerId { get; set; }
        public int NextObjectId { get; set; }
        //public MapOrientation Orientation { get; set; }
        public List<TiledProperty> Properties { get; set; }
        public string RenderOrder { get; set; }
        public TiledMapStaggerAxis StaggerAxis { get; set; }
        public TiledMapStaggerIndex StaggerIndex { get; set; }
        public string TiledVersion { get; set; }
        public int TileHeight { get; set; }
        public List<TiledTileSet> TileSets { get; set; }
        public int TileWidth { get; set; }
        public string Type { get; set; }
        public string Version { get; set; }
        public int Width { get; set; }
    }
}