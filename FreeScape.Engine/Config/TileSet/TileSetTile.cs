using System.Collections.Generic;
using FreeScape.Engine.Config.Map;

namespace FreeScape.Engine.Config
{
    public class TileSetTile
    {
        public uint Id { get; set; }
        public List<MapProperties> Properties { get; set; }
    }
}