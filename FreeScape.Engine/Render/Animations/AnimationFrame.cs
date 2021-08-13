using System.Numerics;
using SFML.Graphics;

namespace FreeScape.Engine.Render.Animations
{
    public class AnimationFrame
    {
        public Texture Texture { get; init; }
        public double Duration { get; init; }
    }
}