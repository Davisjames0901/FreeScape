using System.Collections.Generic;
using FreeScape.Engine.Config.Map;
using SFML.Graphics;

namespace FreeScape.Engine.Config
{
    public class CachedTileSetTile
    {
        public uint Id { get; set; }
        public IntRect TextureLocation { get; set; }
        public List<MapProperties> Properties { get; set; }
    }
}