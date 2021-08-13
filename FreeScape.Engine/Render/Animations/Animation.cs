using System.Collections.Generic;
using FreeScape.Engine.Config.TileSet;

namespace FreeScape.Engine.Render.Animations
{
    public class Animation
    {
        public float ImageHeight { get; set; }
        public float ImageWidth { get; set; }
        public uint Id { get; set; }
        public List<AnimationFrameInfo> AnimationFrames { get; set; }
        public string Type { get; set; }

    }
}
