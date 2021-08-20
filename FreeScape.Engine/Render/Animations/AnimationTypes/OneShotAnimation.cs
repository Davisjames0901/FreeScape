using System.Collections.Generic;
using FreeScape.Engine.Core;
using SFML.Graphics;

namespace FreeScape.Engine.Render.Animations.AnimationTypes
{
    public class OneShotAnimation : ISingleAnimation
    {
        private readonly FrameTimeProvider _timeProvider;
        private readonly Queue<AnimationFrame> _animationQueue;
        private List<AnimationFrame> _animationCache;
        private double _frameTimeRemaining;
        private AnimationFrame _currentFrame;

        public Sprite CurrentSprite => _currentFrame?.Sprite;

        public OneShotAnimation(FrameTimeProvider timeProvider)
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
                _currentFrame = newFrame;
                _frameTimeRemaining = newFrame.Duration;
            }
            else
            {
                _currentFrame = null;
            }
        }

        public void Reset()
        {
            _animationQueue.Clear();
            foreach (var frame in _animationCache)
            {
                _animationQueue.Enqueue(frame);
            }

            _currentFrame = _animationQueue.Dequeue();
        }

        void ISingleAnimation.LoadFrames(List<AnimationFrame> frames)
        {
            _animationCache = frames;
            Reset();
        }
    }
}