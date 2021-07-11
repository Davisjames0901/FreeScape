using System;
using System.Numerics;
using FreeScape.Engine.Managers;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Utilities;
using SFML.Graphics;

namespace FreeScape.GameObjects
{
    public class Player: IMovable
    {
        private readonly ActionProvider _actionProvider;
        private readonly DisplayManager _displayManager;
        private readonly Movement _movement;
        public int ZIndex { get; set; }
        public float Weight { get; set; }
        public Vector2 Size { get; set; }
        public float Speed { get; set; }
        public Vector2 HeadingVector { get; private set; }

        private Vector2 _velocity = new (0, 0);
        private Vector2 _position = new (0, 0);

        public Vector2 Velocity { get { return _velocity; } set { _velocity = value; } }
        public Vector2 Position { get { return _position; } set { _position = value; } }

        
        public Player(ActionProvider actionProvider, SoundProvider soundProvider, DisplayManager displayManager, Movement movement)
        {
            _actionProvider = actionProvider;
            _displayManager = displayManager;
            _movement = movement;
            
            actionProvider.SubscribeOnPressed(a =>
            {
                if(a == "Punch")
                    soundProvider.PlaySound("punch");
            });
            //_position = new Vector2(600, 600);
        }

        public void Init()
        {
            ZIndex = 999;
            Velocity = new Vector2(0, 0);
            Speed = 5.0f;
            Size = new Vector2(3.0f, 3.0f);
            _position = new Vector2(500, 500);
            _displayManager.CurrentPerspective.View.Center = Position;
        }

        public void Tick()
        {
            HeadingVector = Maths.GetHeadingVectorFromMovement(
                _actionProvider.IsActionActivated("MoveUp"),
                _actionProvider.IsActionActivated("MoveDown"), 
                _actionProvider.IsActionActivated("MoveLeft"),
                _actionProvider.IsActionActivated("MoveRight"));
            Console.WriteLine(HeadingVector);
        }

        public void Render(RenderTarget target)
        {
            var player = new CircleShape(Size.X);
            player.FillColor = Color.Red;
            player.Position = new Vector2(_position.X - (Size.X), _position.Y - (Size.Y));
                
            target.Draw(player);
        }
    }
}