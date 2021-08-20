using SFML.Graphics;

namespace FreeScape.Engine.Render.Animations.AnimationTypes
{
    public interface IAnimation
    {
        /// <summary>
        /// The current frame of the animation
        /// </summary>
        /// <returns></returns>
        Sprite CurrentSprite { get; }

        /// <summary>
        /// Advance the state machine by the time past 
        /// </summary>
        void Advance();
        
        /// <summary>
        /// Reset the animation
        /// </summary>
        void Reset();
        
    }
}