using System;
using System.Collections.Generic;
using System.Numerics;
using FreeScape.Engine.GameObjects.Entities;
using FreeScape.Engine.Managers;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Physics.Movements;
using FreeScape.Engine.Physics.Collisions;
using FreeScape.Engine.Physics.Collisions.Colliders;
using FreeScape.Engine.Providers;
using SFML.Graphics;

namespace FreeScape.GameObjects
{
    public class Player : PlayerActions, IMovable, ICollidable 
    {
        private readonly UserInputMovement _movement;
        private readonly DisplayManager _displayManager;
        private CircleShape _shape;
        public int ZIndex { get; set; }
        public float Weight { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Scale { get; set; }
        private float speed = 0;
        public float Speed { 
            get { return speed * PlayerActionSpeedModifier; }
            set { speed = value; } }
        public HeadingVector HeadingVector { get; private set; }
        public List<ICollider> Colliders { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Position { get; set; }

        public Player(UserInputMovement movement, ActionProvider actionProvider, SoundProvider soundProvider, DisplayManager displayManager, FrameTimeProvider frameTimeProvider, AnimationProvider animationProvider, MapProvider mapProvider) : base(soundProvider, frameTimeProvider, animationProvider, mapProvider)
        {
            _movement = movement;
            _displayManager = displayManager;
        }

        public override void Init()
        {
            TileSetName = "CharacterSprites";
            ZIndex = 999;
            Velocity = new Vector2(0, 0);
            Speed = 0.1f;
            Scale = new Vector2(2.0f, 2.0f);
            Size = new Vector2(4.0f, 4.0f);
            Position = new Vector2(300, 500);
            _displayManager.CurrentPerspective.WorldView.Center= Position;
            _shape = new CircleShape(Size.X);
            _shape.FillColor = Color.Red;
            var bodyCollider = new CircleCollider(Position, Position / Size * Scale, Size.X * Scale.X);
            bodyCollider.ColliderType = ColliderType.Solid;
            Colliders = new List<ICollider>();
            Colliders.Add(bodyCollider);
            GetAnimations();

            base.Init();
        }

        private void GetAnimations()
        {
            foreach(var animation in _animationProvider.GetMovementAnimation("idle"))
            {
                Animations.Add(animation.Type, animation);
            }
            foreach (var animation in _animationProvider.GetMovementAnimation("walk"))
            {
                Animations.Add(animation.Type, animation);
            }
            foreach (var animation in _animationProvider.GetMovementAnimation("attack"))
            {
                Animations.Add(animation.Type, animation);
            }

            foreach (var animation in _animationProvider.GetMovementAnimation("roll"))
            {
                Animations.Add(animation.Type, animation);
            }
            foreach (var animation in _animationProvider.GetMovementAnimation("block"))
            {
                Animations.Add(animation.Type, animation);
            }
        }
        
        new public void Tick()
        {
            _movement.Tick();
            HeadingVector = _movement.HeadingVector;
            var attack = _movement.CurrentActionProvider.IsActionActivated("LeftClick");
            var block = _movement.CurrentActionProvider.IsActionActivated("RightClick");
            var roll = _movement.CurrentActionProvider.IsActionActivated("Roll");

            ActionTick(HeadingVector, roll, attack, block);

            foreach(var collider in Colliders)
            {
                collider.Position = Position - Size / 2 * Scale;
            }
            base.Tick();
        }
        
        public void Render(RenderTarget target)
        {
            AnimationSprite.Position = Position - new Vector2(AnimationSprite.TextureRect.Width / 2, AnimationSprite.TextureRect.Height / 2) * Scale;
            AnimationSprite.Scale = Scale;
            target.Draw(AnimationSprite);
        }

        public void CollisionEnter(ICollidable collidable)
        {
            if (collidable is Tile tile)
                Console.WriteLine("player is colliding with a " + tile._tileInfo.Type);
            if (collidable is MapGameObject mgo)
                Console.WriteLine("player is colliding with a " + mgo._tileInfo.Type);
        }
    }
}