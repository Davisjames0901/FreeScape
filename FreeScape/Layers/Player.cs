using System;
using FreeScape.Engine;
using FreeScape.Engine.Event;
using FreeScape.Engine.Render;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace FreeScape.Layers
{
    public class Player : ILayer, IGameObject, IKeyListener
    {
        public int ZIndex { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Player()
        {
            ZIndex = 999;
        }

        public void Tick()
        {
        }

        public void Render(RenderTarget target)
        {
            var player = new CircleShape(5);
            player.FillColor = Color.Red;
            player.Position = new Vector2f(X, Y);
            target.Draw(player);
        }

        public void KeyPressed(object sender, KeyEventArgs args)
        {
            switch (args.Code)
            {
                case Keyboard.Key.Comma:
                    Y--;
                    break;
                case Keyboard.Key.O:
                    Y++;
                    break;
                case Keyboard.Key.A :
                    X--;
                    break;
                case Keyboard.Key.E :
                    X++;
                    break;
            }
        }

        public void KeyReleased(object sender, KeyEventArgs args)
        {
            Console.WriteLine(args);
        }
    }
}