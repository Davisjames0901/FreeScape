using System.Collections.Generic;
using FreeScape.Engine.Config.Map;
using SFML.Graphics;

namespace FreeScape.Engine.Config.TileSet
{
    public class CachedTileSetTile
    {
        public IntRect TextureLocation { get; set; }
        public uint Id { get; set; }
        public List<MapProperties> Properties { get; set; }
        public TileObjectGroup ObjectGroup { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}; Location: {TextureLocation}";
        }
    }
}