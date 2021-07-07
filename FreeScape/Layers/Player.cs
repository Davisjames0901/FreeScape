using FreeScape.Engine.GameObjects;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render.Layers;
using FreeScape.Engine.Physics;
using SFML.Graphics;
using SFML.System;

namespace FreeScape.Layers
{
    public class Player : IMovable
    {
        private readonly ActionProvider _actionProvider;
        public int ZIndex { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Weight { get; set; }
        public float Size { get; set; }
        public bool Collidable { get; set; } = false;
        public float Speed { get; set; }

        private Vector2f _velocity = new Vector2f(0, 0);
        private Vector2f _position = new Vector2f(0, 0);

        public Vector2f Velocity { get { return _velocity; } set { _velocity = value; } }
        public Vector2f Position { get { return _position; } set { _position = value; } }

        
        public Player(ActionProvider actionProvider, SoundProvider soundProvider)
        {
            
            _actionProvider = actionProvider;
            ZIndex = 999;
            Velocity = new Vector2f(0, 0);
            Speed = 5.0f;
            Size = 3.0f;
            actionProvider.SubscribeOnPressed(a =>
            {
                if(a == "Punch")
                    soundProvider.PlaySound("punch");
            });
        }

        public void Tick()
        {
            SetVelocity();
        }

        public void ActionPressed()
        {

        }
        public void ActionReleased()
        {

        }

        private void SetVelocity()
        {

            bool left = _actionProvider.IsActionActivated("MoveLeft");
            bool right = _actionProvider.IsActionActivated("MoveRight");
            bool up = _actionProvider.IsActionActivated("MoveUp");
            bool down = _actionProvider.IsActionActivated("MoveDown");

            float finalSpeed = Speed;
            
            if ((up || down) && (left || right))
            {
                finalSpeed = Speed / 1.5f;
            }

            Vector2f vel = new Vector2f(0, 0);

            if (left)
            {
                _velocity.X = -finalSpeed;
            }
            else if (right)
            {
                _velocity.X = finalSpeed;
            }
            else
            {
                _velocity.X = 0.0f;
            }
            
            if (up)
            {
                _velocity.Y = -finalSpeed;
            }
            else if (down)
            {
                _velocity.Y = finalSpeed;
            }
            else
            {
                _velocity.Y = 0.0f;
            }
        }

        public void Render(RenderTarget target)
        {
            var player = new CircleShape(Size);
            player.FillColor = Color.Red;
            player.Position = new Vector2f(_position.X - (Size), _position.Y - (Size));
            target.Draw(player);
        }
    }
}