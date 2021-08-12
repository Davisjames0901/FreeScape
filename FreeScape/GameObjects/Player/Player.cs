using System;
using System.Numerics;
using FreeScape.Engine.Managers;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Physics.Collisions.Colliders;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render;
using FreeScape.Engine.Utilities;
using SFML.Graphics;

namespace FreeScape.GameObjects
{
    public class Player : PlayerController, IMovable, ICollidable 
    {
        private readonly ActionProvider _actionProvider;
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
        public Vector2 HeadingVector { get; private set; }
        private CircleCollider _collider;
        public ICollider Collider => _collider;
        public Vector2 Velocity { get; set; }
        public Vector2 Position { get; set; }

        public ColliderType ColliderType { get; set; }

        public Player(ColliderProvider colliderProvider, ActionProvider actionProvider, SoundProvider soundProvider, DisplayManager displayManager, FrameTimeProvider frameTimeProvider, AnimationProvider animationProvider, MapProvider mapProvider) : base(actionProvider, soundProvider, frameTimeProvider, animationProvider, mapProvider)
        {
            _actionProvider = actionProvider;
            _displayManager = displayManager;
            ColliderType = ColliderType.Solid;
            actionProvider.SubscribeOnPressed(a =>
            {
            });
        }

        public override void Init()
        {
            TileSetName = "CharacterSprites";
            ZIndex = 999;
            Velocity = new Vector2(0, 0);
            Speed = 10.0f;
            Scale = new Vector2(2.0f, 2.0f);
            Size = new Vector2(4.0f, 4.0f);
            Position = new Vector2(300, 500);
            _displayManager.CurrentPerspective.WorldView.Center= Position;
            _shape = new CircleShape(Size.X);
            _shape.FillColor = Color.Red;
            _collider = new CircleCollider(Position, Position / Size * Scale, Size.X * Scale.X);
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
            var up = _actionProvider.IsActionActivated("MoveUp");
            var down = _actionProvider.IsActionActivated("MoveDown");
            var left = _actionProvider.IsActionActivated("MoveLeft");
            var right = _actionProvider.IsActionActivated("MoveRight");
            var attack = _actionProvider.IsActionActivated("LeftClick");
            var block = _actionProvider.IsActionActivated("RightClick");
            var roll = _actionProvider.IsActionActivated("Roll");

            HeadingVector = Maths.GetHeadingVectorFromMovement(up, down, left, right);

            ControllerTick(HeadingVector, roll, attack, block);

            PlayerMove(up, down, left, right);
            base.Tick();
        }
        private void PlayerMove(bool up, bool down, bool left, bool right)
        {
            _collider.Position = Position - Size / 2 * Scale;
        }

        public void Render(RenderTarget target)
        {
            AnimationSprite.Position = Position - new Vector2(AnimationSprite.TextureRect.Width / 2, AnimationSprite.TextureRect.Height / 2) * Scale;
            AnimationSprite.Scale = Scale;
            target.Draw(AnimationSprite);
        }

        public void CollisionEnter(ICollidable collidable)
        {
            //set player to previous position if collidable is solid
        }
    }
}