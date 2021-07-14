using System.Collections.Generic;

namespace FreeScape.Engine.Config.TileSet
{
    public class TileObjectGroup
    {
        public string DrawOrder { get; set; }
        public string Name { get; set; }
        public List<TileObject> Objects { get; set; }
        public int Opacity { get; set; }
        public string Type { get; set; }
        public bool Visible { get; set; }

    }
}
