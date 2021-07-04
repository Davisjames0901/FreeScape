using System.IO;
using System.Reflection;
using AsperandLabs.UnitStrap.Core.Extenstions;
using FreeScape.Engine;
using FreeScape.Scenes;
using Microsoft.Extensions.DependencyInjection;

namespace FreeScape
{
    public class Bootstrapper
    {
        public static IServiceCollection Bootstrap(IServiceCollection services)
        {
            var config = new GameInfo
            {
                ScreenHeight = 1080,
                ScreenWidth = 1920,
                Name = "FreeScape",
                RefreshRate = 60,
                AssetDirectory =
                    $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}{Path.DirectorySeparatorChar}Assets"
            };
            
            services.AddUnitStrapper();
            services.AddUnit<EngineUnitstrapper, GameInfo>(config);

            services.AddTransient<Menu>();
            services.AddTransient<TestScene>();

            return services;
        }
    }
}