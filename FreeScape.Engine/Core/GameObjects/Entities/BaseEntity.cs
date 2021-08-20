using System;
using System.Collections.Generic;
using System.Numerics;
using FreeScape.Engine.Input.Controllers;
using FreeScape.Engine.Physics.Collisions;
using FreeScape.Engine.Physics.Collisions.Colliders;
using FreeScape.Engine.Physics.Movement;
using FreeScape.Engine.Render.Animations.AnimationTypes;
using SFML.Graphics;

namespace FreeScape.Engine.Core.GameObjects.Entities
{
    public abstract class BaseEntity : IMovable, ICollidable
    {
        public abstract Vector2 Size { get; }
        public abstract Vector2 Scale { get; }
        public abstract float Speed { get; }
        public HeadingVector HeadingVector { get; set; }
        public List<ICollider> Colliders { get; }
        public Vector2 Position { get; set; }
        public IController Controller { get; set; }
        protected IAnimation CurrentAnimation { get; private set; }
        
        private List<(Func<bool> selector, IAnimation animation)> _animations;
        private List<(Func<bool> selector, IAnimation animation)> _actionAnimations;
        private bool _isActioning;

        public BaseEntity()
        {
            Colliders = new List<ICollider>();
        }

        public virtual void Init()
        {
            HeadingVector = Controller.HeadingVector;
            _animations = new();
            _animations.AddRange(RegisterMovementAnimations());
            _actionAnimations = new();
            _actionAnimations.AddRange(RegisterActionAnimations());
        }

        public virtual void Tick()
        {
            Controller.Tick();
            foreach(var collider in Colliders)
            {
                collider.Position = Position - Size / 2 * Scale;
            }
        }
        
        public virtual void Render(RenderTarget target)
        {
            SetCurrentAnimation();
            var sprite = CurrentAnimation.CurrentSprite;
            if (sprite == null)
                return;
            sprite.Position = Position - new Vector2(sprite.TextureRect.Width / 2, sprite.TextureRect.Height / 2) * Scale;
            sprite.Scale = Scale;
            target.Draw(sprite);
        }

        private void SetCurrentAnimation()
        {
            CurrentAnimation?.Advance();
            if (CurrentAnimation?.CurrentSprite == null)
                _isActioning = false;

            if (_isActioning)
                return;
            
            foreach (var animation in _actionAnimations)
            {
                if (animation.selector())
                {
                    _isActioning = true;
                    CurrentAnimation = animation.animation;
                    CurrentAnimation.Reset();
                    return;
                }
            }
            foreach (var animation in _animations)
            {
                if (animation.selector())
                {
                    CurrentAnimation = animation.animation;
                    return;
                }
                
            }
        }

        public abstract IEnumerable<(Func<bool>, IAnimation)> RegisterMovementAnimations();
        public abstract IEnumerable<(Func<bool>, IAnimation)> RegisterActionAnimations();
        public abstract void CollisionEnter(ICollidable collidable);
    }
}