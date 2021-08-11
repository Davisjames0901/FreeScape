using FreeScape.Engine.Config.TileSet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeScape.Engine.Render
{
    public interface IAnimated
    {
        Dictionary<string, Animation> Animations { get; set; }
        string TileSetName { get; set; }
        int FrameCounter{ get; set; }
        Stopwatch Timer { get; set; }
        AnimationFrame CurrentFrame { get; set; }
    }
}
