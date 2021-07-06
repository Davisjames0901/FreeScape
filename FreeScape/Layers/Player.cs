using System;
using FreeScape.Engine.GameObjects;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render.Layers;
using SFML.Graphics;
using SFML.System;

namespace FreeScape.Layers
{
    public class Player : ILayer, IGameObject
    {
        private readonly ActionProvider _actionProvider;
        public int ZIndex { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Size { get; set; }
        public Vector2f Velocity;
        public float Speed { get; set; }
        

        public Player(ActionProvider actionProvider)
        {
            _actionProvider = actionProvider;
            ZIndex = 999;
            Velocity = new Vector2f(0, 0);
            Speed = 1.0f;
            Size = 3.0f;
        }

        public void Tick()
        {
            Movement();
        }

        public void ActionPressed()
        {

        }
        public void ActionReleased()
        {

        }

        private void Movement()
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
                Velocity.X = -finalSpeed;
            }
            else if (right)
            {
                Velocity.X = finalSpeed;
            }
            else
            {
                Velocity.X = 0.0f;
            }
            
            if (up)
            {
                Velocity.Y = -finalSpeed;
            }
            else if (down)
            {
                Velocity.Y = finalSpeed;
            }
            else
            {
                Velocity.Y = 0.0f;
            }

            X += Velocity.X;
            Y += Velocity.Y;
        }

        public void Render(RenderTarget target)
        {
            var player = new CircleShape(Size);
            player.FillColor = Color.Red;
            player.OutlineColor = Color.White;
            player.OutlineThickness = 1f;
            player.Position = new Vector2f(X - (Size/2), Y - (Size/2));
            target.Draw(player);
        }
    }
}