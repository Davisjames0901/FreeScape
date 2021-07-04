using System.IO;
using System.Reflection;
using AsperandLabs.UnitStrap.Core.Extenstions;
using FreeScape.Engine;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render;
using FreeScape.Scenes;
using Microsoft.Extensions.DependencyInjection;

namespace FreeScape
{
    public class Bootstrapper
    {
        public static IServiceCollection Bootstrap(IServiceCollection services)
        {
            services.AddUnitStrapper();
            services.AddUnit<EngineUnitstrapper>();

            services.AddSingleton(new GameInfo
            {
                ScreenHeight = 1080,
                ScreenWidth = 1920,
                Name = "FreeScape",
                RefreshRate = 60,
                AssetDirectory = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}/Assets"
            });
            services.AddTransient<Menu>();
            services.AddTransient<TestScene>();

            services.AddSingleton<TextureProvider>();
            services.AddSingleton<MapProvider>();
            services.AddSingleton<TiledMapRenderer>();

            return services;
        }
    }
}