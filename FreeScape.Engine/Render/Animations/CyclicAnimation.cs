using System.Collections.Generic;
using FreeScape.Engine.Providers;
using SFML.Graphics;

namespace FreeScape.Engine.Render.Animations
{
    public class CyclicAnimation : IAnimation
    {
        private readonly FrameTimeProvider _timeProvider;
        private readonly Queue<(Sprite Sprite, double Duration)> _animationQueue;
        private readonly List<(Sprite Sprite, double Duration)> _animationCache;
        private double _frameTimeRemaining;
        
        public Sprite CurrentSprite { get; private set; }

        public CyclicAnimation(FrameTimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
            _animationCache = new();
            _animationQueue = new();
        }
        
        public void Advance()
        {
            if (_frameTimeRemaining > 0)
                _frameTimeRemaining -= _timeProvider.DeltaTimeMilliSeconds;
            else if (_animationQueue.TryDequeue(out var newFrame))
            {
                CurrentSprite = newFrame.Sprite;
                _frameTimeRemaining = newFrame.Duration;
                _animationQueue.Enqueue(newFrame);
            }
        }

        public void Reset()
        {
            _animationQueue.Clear();
            foreach (var frame in _animationCache)
            {
                _animationQueue.Enqueue(frame);
            }
        }

        void IAnimation.LoadFrames(List<AnimationFrame> frames)
        {
            foreach (var frame in frames)
            {
                _animationCache.Add((new Sprite(frame.Texture), frame.Duration));
            }
            Reset();
        }
    }
}