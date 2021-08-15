using System.Collections.Generic;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Physics.Movements;
using SFML.Graphics;

namespace FreeScape.Engine.Render.Animations
{
    public class DirectionAnimation
    {
        private readonly Dictionary<Direction, IAnimation> _animations;
        private IMovable _headingVector;
        private (IAnimation animation, Direction direction)? _currentAnimation;

        public DirectionAnimation()
        {
            _animations = new();
        }

        public Sprite? CurrentSprite => _currentAnimation?.animation.CurrentSprite;
        public void Advance()
        {
            foreach (var animation in _animations.Values)
            {
                animation.Advance();
            }

            var collapsedDirection = _headingVector?.HeadingVector?.CollapseDirection();
            if (collapsedDirection == null || collapsedDirection == Direction.None )
                return;
            
            if (_currentAnimation?.direction == collapsedDirection)
                return;
            
            _currentAnimation = (_animations[collapsedDirection.Value], collapsedDirection.Value);
        }

        public void Reset()
        {
            foreach (var animation in _animations.Values)
            {
                animation.Advance();
            }
        }

        public void LoadAnimations(List<(IAnimation animation, Direction direction)> animations, IMovable headingVector)
        {
            _headingVector = headingVector;
            foreach (var animation in animations)
            {
                _animations.Add(animation.direction, animation.animation);
            }
        }
    }
}