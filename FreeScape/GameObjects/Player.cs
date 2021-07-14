using System;
using System.Numerics;
using FreeScape.Engine.Managers;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Physics.Colliders;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Utilities;
using SFML.Graphics;

namespace FreeScape.GameObjects
{
    public class Player: IMovable, ICollidable
    {
        private readonly ActionProvider _actionProvider;
        private readonly DisplayManager _displayManager;
        private CircleShape _shape;
        public int ZIndex { get; set; }
        public float Weight { get; set; }
        public Vector2 Size { get; set; }
        public float Speed { get; set; }
        public Vector2 HeadingVector { get; private set; }
        private CircleCollider _collider;
        public ICollider Collider => _collider;
        public Vector2 Velocity { get; set; }
        public Vector2 Position { get; set; }

        
        public Player(ActionProvider actionProvider, SoundProvider soundProvider, DisplayManager displayManager)
        {
            _actionProvider = actionProvider;
            _displayManager = displayManager;
            
            actionProvider.SubscribeOnPressed(a =>
            {
                if(a == "Punch")
                    soundProvider.PlaySound("punch");
            });
        }

        public void Init()
        {
            ZIndex = 999;
            Velocity = new Vector2(0, 0);
            Speed = 5.0f;
            Size = new Vector2(3.0f, 3.0f);
            Position = new Vector2(300, 500);
            _displayManager.CurrentPerspective.View.Center = Position;
            _shape = new CircleShape(Size.X);
            _shape.FillColor = Color.Red;
            _collider = new CircleCollider(Position, Position / Size, Size.X);
        }

        public void Tick()
        {
            HeadingVector = Maths.GetHeadingVectorFromMovement(
                _actionProvider.IsActionActivated("MoveUp"),
                _actionProvider.IsActionActivated("MoveDown"), 
                _actionProvider.IsActionActivated("MoveLeft"),
                _actionProvider.IsActionActivated("MoveRight"));
            _collider.Position = Position - Size / 2;
        }

        public void Render(RenderTarget target)
        {
            _shape.Position = Position - Size;
            target.Draw(_shape);
        }

    }
}