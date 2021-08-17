using System;
using System.Collections.Generic;
using System.Numerics;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Physics.Collisions.Colliders;
using FreeScape.Engine.Physics.Movements;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render.Animations;

namespace FreeScape.GameObjects.Player
{
    public class Player : BaseEntity
    {
        private readonly AnimationProvider _animationProvider;
        public int ZIndex => 999;
        public override Vector2 Size => new (4.0f, 4.0f);
        public override Vector2 Scale => new (2.0f, 2.0f);
        public override float Speed => _speed;
        private float _speed;
        
        public Player(UserInputMovement movement, AnimationProvider animationProvider)
        {
            _animationProvider = animationProvider;
            Movement = movement;
        }

        public override void Init()
        {
            base.Init();
            _speed = 0.1f;
            Position = new Vector2(300, 500);
            var bodyCollider = new CircleCollider(Position, Position / Size * Scale, Size.X * Scale.X);
            bodyCollider.ColliderType = ColliderType.Solid;
            Colliders.Add(bodyCollider);
        }

        public override IEnumerable<(Func<bool>, IAnimation)> RegisterMovementAnimations()
        {
            yield return (() => HeadingVector.Vector == Vector2.Zero, _animationProvider.GetDirectionAnimation<CyclicAnimation>("idle", this));
            yield return (() => HeadingVector.Vector != Vector2.Zero, _animationProvider.GetDirectionAnimation<CyclicAnimation>("walk", this));
        }

        public override IEnumerable<(Func<bool>, IAnimation)> RegisterActionAnimations()
        {
            return default;
        }

        public override void CollisionEnter(ICollidable collidable)
        {
            
        }
    }
}