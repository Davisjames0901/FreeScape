using System;
using System.Collections.Generic;
using System.Numerics;
using FreeScape.Engine.Core.GameObjects.Entities;
using FreeScape.Engine.Input.Controllers;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Physics.Collisions;
using FreeScape.Engine.Physics.Collisions.Colliders;
using FreeScape.Engine.Render.Animations;
using FreeScape.Engine.Render.Animations.AnimationTypes;

namespace FreeScape.GameObjects.Player
{
    public class Player : BaseEntity
    {
        private readonly AnimationProvider _animationProvider;
        private readonly UserInputController _input;
        public override Vector2 Size => new (4.0f, 4.0f);
        public override Vector2 Scale => new (2.0f, 2.0f);
        public override float Speed => _speed;
        private float _speed;
        
        public Player(UserInputController controller, AnimationProvider animationProvider)
        {
            _animationProvider = animationProvider;
            _input = controller;
            Controller = controller;
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
            yield return (() => _input.CurrentActionProvider.IsActionActivated("LeftClick"), 
                _animationProvider.GetDirectionAnimation<OneShotAnimation>("attack", this));
        }

        public override void CollisionEnter(ICollidable collidable)
        {
            
        }
    }
}