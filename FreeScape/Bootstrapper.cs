using System.IO;
using System.Reflection;
using AsperandLabs.UnitStrap.Core.Extenstions;
using FreeScape.Engine;
using FreeScape.Engine.Config;
using FreeScape.Layers;
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
                RefreshRate = 144,
                SfxVolume = 100.0f,
                MusicVolume = 50.0f,
                AssetDirectory =
                    $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}{Path.DirectorySeparatorChar}Assets"
            };
            
            services.AddUnitStrapper();
            services.AddUnit<EngineUnitstrapper, GameInfo>(config);

            services.AddTransient<Menu>();
            services.AddTransient<TestScene>();
            services.AddTransient<Player>();
            services.AddTransient<TestTileMap>();
            services.AddTransient<EntityLayer>();

            return services;
        }
    }
}