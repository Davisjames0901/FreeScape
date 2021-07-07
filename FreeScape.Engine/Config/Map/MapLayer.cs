using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;

namespace FreeScape.Engine.Config.Map
{
    public class MapLayer
    {
        public List<Chunk> Chunks { get; set; }
        public string Compression { get; set; }
        public List<uint> Data { get; set; }
        public string DrawOrder { get; set; }
        public string Encoding { get; set; }
        public int Height { get; set; }
        public int Id { get; set; }
        public string Image { get; set; }
        public List<MapLayer> Layers { get; set; }
        public string Name { get; set; }
        public List<MapObject> Objects { get; set; }
        public double OffsetX { get; set; }
        public double OffsetY { get; set; }
        public double Opacity { get; set; }
        public double ParallaxX { get; set; }
        public List<MapProperties> Properties { get; set; }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public string TintColor { get; set; }
        public string TransparentColor { get; set; }
        public string Type { get; set; }
        public bool Visible { get; set; }
        public int Width { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}