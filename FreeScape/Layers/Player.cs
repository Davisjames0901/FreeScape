using System;
using FreeScape.Engine.GameObjects;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace FreeScape.Layers
{
    public class Player : ILayer, IGameObject
    {
        private readonly ActionProvider _actionProvider;
        public int ZIndex { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Player(ActionProvider actionProvider)
        {
            _actionProvider = actionProvider;
            ZIndex = 999;
        }

        public void Tick()
        {
            if (_actionProvider.IsActionActivated("MoveUp"))
            {
                Y--;
            }

            if (_actionProvider.IsActionActivated("MoveDown"))
            {
                Y++;
            }

            if (_actionProvider.IsActionActivated("MoveLeft"))
            {
                X--;
            }

            if (_actionProvider.IsActionActivated("MoveRight"))
            {
                X++;
            }
        }

        public void Render(RenderTarget target)
        {
            var player = new CircleShape(5);
            player.FillColor = Color.Red;
            player.Position = new Vector2f(X, Y);
            target.Draw(player);
        }
    }
}