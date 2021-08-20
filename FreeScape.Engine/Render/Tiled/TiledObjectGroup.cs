using System.Collections.Generic;

namespace FreeScape.Engine.Render.Tiled
{
    public class TiledObjectGroup
    {
        public string DrawOrder { get; set; }
        public string Name { get; set; }
        public List<TiledObject> Objects { get; set; }
        public int Opacity { get; set; }
        public string Type { get; set; }
        public bool Visible { get; set; }

    }
}
