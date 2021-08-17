using System.Collections.Generic;
using SFML.Graphics;

namespace FreeScape.Engine.Render.Animations
{
    public class OneShotAnimation : ISingleAnimation
    {

        public Sprite CurrentSprite { get; private set; }

        public void Advance()
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }

        void ISingleAnimation.LoadFrames(List<AnimationFrame> frames)
        {
            throw new System.NotImplementedException();
        }
    }
}