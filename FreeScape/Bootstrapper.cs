using System.IO;
using System.Reflection;
using AsperandLabs.UnitStrap.Core.Extenstions;
using FreeScape.Engine;
using FreeScape.Engine.Config;
using FreeScape.Engine.Config.UserSettings;
using FreeScape.GameObjects;
using FreeScape.GameObjects.Player;
using FreeScape.Layers;
using FreeScape.Layers.MainMenu;
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
                Name = "FreeScape",
                AssetDirectory =
                    $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}{Path.DirectorySeparatorChar}Assets"
            };

            var sound = new SoundSettings
            {
                SfxVolume = 100.0f,
                MusicVolume = 0.0f
            };

            var graphics = new GraphicsSettings
            {
                ScreenHeight = 1080,
                ScreenWidth = 1920,
                VSyncEnabled = false,
                RefreshRate = int.MaxValue
            };
            
            services.AddUnitStrapper();
            services.AddUnit<EngineUnitstrapper, GameInfo>(config);
            services.AddSingleton(sound);
            services.AddSingleton(graphics);

            services.AddTransient<MainMenuScene>();
            services.AddTransient<TestScene>();
            services.AddTransient<Player>();
            services.AddTransient<PlayerUI>();
            services.AddTransient<TestTileMap>();
            services.AddTransient<EntityLayer>();
            services.AddTransient<MainMenuOptions>();
            services.AddTransient<MainMenuHome>();
            services.AddTransient<PlayerUI>();
            services.AddTransient<PauseMenu>();
            services.AddTransient<MainMenuBackground>();

            return services;
        }
    }
}