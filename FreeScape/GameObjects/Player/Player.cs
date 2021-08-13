using System;
using System.Collections.Generic;
using System.Numerics;
using FreeScape.Engine.GameObjects.Entities;
using FreeScape.Engine.Managers;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Physics.Movements;
using FreeScape.Engine.Physics.Collisions.Colliders;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render.Animations;
using SFML.Graphics;

namespace FreeScape.GameObjects
{
    public class Player : IMovable, ICollidable 
    {
        private readonly UserInputMovement _movement;
        private readonly DisplayManager _displayManager;
        private readonly AnimationProvider _animationProvider;
        private CircleShape _shape;
        public int ZIndex { get; set; }
        public float Weight { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Scale { get; set; }
        public float Speed { get; set; }
        public HeadingVector HeadingVector { get; private set; }
        public List<ICollider> Colliders { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Position { get; set; }
        private IAnimation _currentAnimation;

        public Player(UserInputMovement movement, DisplayManager displayManager, AnimationProvider animationProvider)
        {
            _movement = movement;
            _displayManager = displayManager;
            _animationProvider = animationProvider;
        }

        public void Init()
        {
            //TileSetName = "CharacterSprites";
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
            _currentAnimation = _animationProvider.GetAnimation<CyclicAnimation>("idle:down");
        }

        public void Tick()
        {
            _movement.Tick();
            HeadingVector = _movement.HeadingVector;
            var attack = _movement.CurrentActionProvider.IsActionActivated("LeftClick");
            var block = _movement.CurrentActionProvider.IsActionActivated("RightClick");
            var roll = _movement.CurrentActionProvider.IsActionActivated("Roll");

            //ActionTick(HeadingVector, roll, attack, block);

            foreach(var collider in Colliders)
            {
                collider.Position = Position - Size / 2 * Scale;
            }
            //base.Tick();
        }
        
        public void Render(RenderTarget target)
        {
            _currentAnimation.Advance();
            var sprite = _currentAnimation.CurrentSprite;
            sprite.Position = Position - new Vector2(sprite.TextureRect.Width / 2, sprite.TextureRect.Height / 2) * Scale;
            sprite.Scale = Scale;
            target.Draw(sprite);
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