using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            game.Start<MainMenuScene>();

                //(new Vector2((float)(radius * Math.Sin(k)), (float)(radius * Math.Cos(k))) + center);

        }
    }
}