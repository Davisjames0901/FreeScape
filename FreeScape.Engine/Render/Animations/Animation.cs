using FreeScape.Engine.Config.TileSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeScape.Engine.Render
{
    public class Animation
    {
        public float ImageHeight { get; set; }
        public float ImageWidth { get; set; }
        public uint Id { get; set; }
        public List<AnimationFrame> AnimationFrames { get; set; }
        public string Type { get; set; }

    }
}
