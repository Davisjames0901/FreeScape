using System;
using System.Collections.Generic;
using FreeScape.Engine.Physics.Movement;
using SFML.Graphics;

namespace FreeScape.Engine.Render.Animations.AnimationTypes
{
    public class DirectionAnimation: ISwitchedGroupAnimation<Direction>
    {
        private Dictionary<Direction, IAnimation> _animations;
        private (IAnimation animation, Direction direction) _currentAnimation;
        private Func<Direction> _selectDirection;
        
        public Sprite CurrentSprite => _currentAnimation.animation.CurrentSprite;

        public void Advance()
        {
            foreach (var animation in _animations.Values)
            {
                animation.Advance();
            }

            var collapsedDirection = _selectDirection();
            if (collapsedDirection == Direction.None || _currentAnimation.direction == collapsedDirection)
                return;
            
            _currentAnimation = (_animations[collapsedDirection], collapsedDirection);
        }

        public void Reset()
        {
            _currentAnimation = (_animations[Direction.Down], Direction.Down);
            foreach (var item in _animations)
            {
                item.Value.Reset();
            }
        }

        void ISwitchedGroupAnimation<Direction>.LoadAnimations(List<(IAnimation animation, Direction groupingKey)> animations, Func<Direction> selector)
        {
            _animations = new();
            _selectDirection = selector;
            foreach (var animation in animations)
            {
                _animations.Add(animation.groupingKey, animation.animation);
            }

            Reset();
        }
    }
}