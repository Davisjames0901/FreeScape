using System;
using System.Numerics;
using FreeScape.Engine;
using FreeScape.Engine.Physics;
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
            var line1 = new Line(new Vector2(1, 1), new Vector2(-1, -1));
            var line2 = new Line(new Vector2(-1, 1), new Vector2(1, -1));
            Console.WriteLine(line1.Intersection(line2));

            game.Start<MainMenuScene>();
            
        }
    }
}