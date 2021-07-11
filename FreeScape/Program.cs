using FreeScape.Engine;
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
            
        }
    }
}