using System.Collections.Generic;

namespace FreeScape.Engine.Render.Animations
{
    /// <summary>
    /// This is an interface that defines how the frames are loaded for the animation.
    /// </summary>
    public interface ISingleAnimation : IAnimation
    {
        
        /// <summary>
        /// Load in the frames for the animation
        /// </summary>
        /// <param name="frames"></param>
        internal void LoadFrames(List<AnimationFrame> frames);
    }
}