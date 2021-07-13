using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeScape.Engine.Config.TileSet
{
    public class TileObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Rotation { get; set; }
        public string Type { get; set; }
        public bool Visible { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }
}
