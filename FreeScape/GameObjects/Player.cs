using System;
using System.Numerics;
using FreeScape.Engine.Managers;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Physics.Colliders;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render;
using FreeScape.Engine.Utilities;
using SFML.Graphics;

namespace FreeScape.GameObjects
{
    public class Player: Animated, IMovable, ICollidable
    {
        private readonly ActionProvider _actionProvider;
        private readonly DisplayManager _displayManager;
        public readonly AnimationProvider _animationProvider;
        private CircleShape _shape;
        public int ZIndex { get; set; }
        public float Weight { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Scale { get; set; }
        public float Speed { get; set; }
        public Vector2 HeadingVector { get; private set; }
        private CircleCollider _collider;
        public ICollider Collider => _collider;
        public Vector2 Velocity { get; set; }
        public Vector2 Position { get; set; }
        public string AnimationDirection { get; set; }

        private bool _performingAction = false;
        private string _action = "none";

        
        public Player(ActionProvider actionProvider, SoundProvider soundProvider, DisplayManager displayManager, FrameTimeProvider frameTimeProvider, AnimationProvider animationProvider, MapProvider mapProvider) : base(animationProvider, mapProvider)
        {
            _actionProvider = actionProvider;
            _displayManager = displayManager;
            _animationProvider = animationProvider;


            actionProvider.SubscribeOnPressed(a =>
            {
                if(a == "Punch")
                    soundProvider.PlaySound("punch");
            });
        }

        public override void Init()
        {
            TileSetName = "CharacterSprites";
            ZIndex = 999;
            Velocity = new Vector2(0, 0);
            Speed = 5.0f;
            Scale = new Vector2(2.0f, 2.0f);
            Size = new Vector2(4.0f, 4.0f);
            Position = new Vector2(300, 500);
            _displayManager.CurrentPerspective.View.Center = Position;
            _shape = new CircleShape(Size.X);
            _shape.FillColor = Color.Red;
            _collider = new CircleCollider(Position, Position / Size * Scale, Size.X * Scale.X);
            GetAnimations();
            AnimationDirection = "down";

            AnimationName = "idle:down";
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
        }

        public void Tick()
        {
            var up = _actionProvider.IsActionActivated("MoveUp");
            var down = _actionProvider.IsActionActivated("MoveDown");
            var left = _actionProvider.IsActionActivated("MoveLeft");
            var right = _actionProvider.IsActionActivated("MoveRight");
            var attack = _actionProvider.IsActionActivated("LeftClick");
            var animationName = "";
            if (!_performingAction)
            {
                if ((up || down || left || right) && !_performingAction)
                {
                    animationName = "walk";
                }
                else animationName = "idle";


                if (left)
                {
                    AnimationDirection = "left";
                }
                if (right)
                {
                    AnimationDirection = "right";
                }
                if (left && right)
                {
                    AnimationDirection = "down";
                }
                if (up)
                {
                    AnimationDirection = "up";
                }
                if (down)
                {
                    AnimationDirection = "down";
                }
                if (attack)
                {
                    PlayerAttack();
                }
                if(!_performingAction)
                    AnimationName = animationName + ":" + AnimationDirection;
                PlayerMove(up, down, left, right);
            }
            else
            {
                PlayerAction();
            }
            base.Tick();
        }
        private void PlayerAction()
        {
            switch (_action)
            {
                case "attack":
                    PlayerAttack();
                    break;
                default:
                    break;
            }
        }
        public void PlayerAttack()
        {
            AnimationName = "attack:" + AnimationDirection;
            if (AnimationTimeRemaining > 0)
            {
            _performingAction = true;
            _action = "attack";
            }
            else
            {
                AnimationName = "idle:" + AnimationDirection;

                _performingAction = false;
            }
        }
        private void PlayerMove(bool up, bool down, bool left, bool right)
        {
            if (!_performingAction)
            {
                HeadingVector = Maths.GetHeadingVectorFromMovement(up, down, left, right);
            }
            else HeadingVector = Vector2.Zero;


            _collider.Position = Position - Size / 2 * Scale;
        }

        public void Render(RenderTarget target)
        {
            AnimationSprite.Position = Position - new Vector2(AnimationSprite.TextureRect.Width / 2, AnimationSprite.TextureRect.Height / 2) * Scale;
            AnimationSprite.Scale = Scale;
            target.Draw(AnimationSprite);
        }

    }
}