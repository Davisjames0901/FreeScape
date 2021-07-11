using System.Text.Json;
using System.Text.Json.Serialization;
using AsperandLabs.UnitStrap.Core.Abstracts;
using FreeScape.Engine.Config;
using FreeScape.Engine.Config.Action;
using FreeScape.Engine.Managers;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Physics;
using Microsoft.Extensions.DependencyInjection;

namespace FreeScape.Engine
{
    public class EngineUnitstrapper : BaseUnitStrapper<GameInfo>
    {
        public override string Namespace => GetType().Assembly.ToString();
        protected override IServiceCollection RegisterInternalDependencies(IServiceCollection services, GameInfo info)
        {
            services.AddSingleton<Game>();
            
            services.AddSingleton(info);

            services.AddSingleton<SceneManager>();
            services.AddSingleton<DisplayManager>();
            
            services.AddSingleton<TextureProvider>();
            services.AddSingleton<MapProvider>();
            services.AddSingleton<SfmlActionResolver>();
            services.AddSingleton<LayerProvider>();
            services.AddSingleton<ServiceScopeProvider>();
            services.AddSingleton<GameManager>();
            services.AddSingleton<FrameTimeProvider>();
            services.AddSingleton<Movement>();
            services.AddSingleton<TileSetProvider>();
            
            services.AddSingleton(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            services.AddSingleton<GameObjectProvider>();
            services.AddSingleton<UIObjectProvider>();

            services.AddScoped<ActionProvider>();
            services.AddScoped<GameObjectProvider>();
            services.AddSingleton<SoundProvider>();
            
            return services;
        }
    }
}