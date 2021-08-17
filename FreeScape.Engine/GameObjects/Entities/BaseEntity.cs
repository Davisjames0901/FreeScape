using System;
using System.Collections.Generic;
using System.Numerics;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Physics.Movements;
using FreeScape.Engine.Physics.Collisions.Colliders;
using FreeScape.Engine.Render.Animations;
using SFML.Graphics;

namespace FreeScape.GameObjects
{
    public abstract class BaseEntity : IMovable, ICollidable
    {
        public abstract Vector2 Size { get; }
        public abstract Vector2 Scale { get; }
        public abstract float Speed { get; }
        public HeadingVector HeadingVector { get; set; }
        public List<ICollider> Colliders { get; }
        public Vector2 Position { get; set; }
        public IMovement Movement { get; set; }
        protected IAnimation CurrentAnimation { get; private set; }
        
        private List<(Func<bool> selector, IAnimation animation)> _animations;

        public BaseEntity()
        {
            Colliders = new List<ICollider>();
        }

        public virtual void Init()
        {
            HeadingVector = Movement.HeadingVector;
            _animations = new List<(Func<bool>, IAnimation)>();
            _animations.AddRange(RegisterMovementAnimations());
        }

        public virtual void Tick()
        {
            Movement.Tick();
            foreach(var collider in Colliders)
            {
                collider.Position = Position - Size / 2 * Scale;
            }
        }
        
        public virtual void Render(RenderTarget target)
        {
            SetCurrentAnimation();
            CurrentAnimation.Advance();
            var sprite = CurrentAnimation.CurrentSprite;
            if (sprite == null)
                return;
            sprite.Position = Position - new Vector2(sprite.TextureRect.Width / 2, sprite.TextureRect.Height / 2) * Scale;
            sprite.Scale = Scale;
            target.Draw(sprite);
        }

        private void SetCurrentAnimation()
        {
            foreach (var animation in _animations)
            {
                if (animation.selector())
                    CurrentAnimation = animation.animation;
            }
        }

        public abstract IEnumerable<(Func<bool>, IAnimation)> RegisterMovementAnimations();
        public abstract IEnumerable<(Func<bool>, IAnimation)> RegisterActionAnimations();
        public abstract void CollisionEnter(ICollidable collidable);
    }
}