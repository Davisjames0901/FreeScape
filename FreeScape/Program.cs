using System;
using System.Numerics;
using FreeScape.Engine;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Physics.Colliders;
using FreeScape.Scenes;
using Microsoft.Extensions.DependencyInjection;

namespace FreeScape
{
    class Program
    {
        static void Main()
        {
            var collection = Bootstrapper.Bootstrap(new ServiceCollection());
            using var provider = collection.BuildServiceProvider();
            var game = provider.GetRequiredService<Game>();

            var c = new RectangleCollider(new Vector2(10, 10), Vector2.Zero);
            var test = c.Collides(Vector2.One);
            //Console.WriteLine(line1.Intersection(line2));

            game.Start<MainMenuScene>();
            
        }
    }
}