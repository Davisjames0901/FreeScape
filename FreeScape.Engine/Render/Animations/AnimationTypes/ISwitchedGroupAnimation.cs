using System;
using System.Collections.Generic;

namespace FreeScape.Engine.Render.Animations.AnimationTypes
{
    /// <summary>
    /// This is an interface that defines how the frames are loaded for the animation. in this case it is a group of animations that can be switched between
    /// </summary>
    public interface ISwitchedGroupAnimation<T> : IAnimation
    {
        /// <summary>
        /// Loads the animations with the given grouping and passes a selector to determine the animation to display at any given tick
        /// </summary>
        /// <param name="animations"></param>
        /// <param name="selector">A func that determines the animation to display</param>
        internal void LoadAnimations(List<(IAnimation animation, T groupingKey)> animations, Func<T> selector);
    }
}