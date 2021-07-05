using AsperandLabs.UnitStrap.Core.Abstracts;
using FreeScape.Engine.Actions;
using FreeScape.Engine.Managers;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render;
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
            
            services.AddScoped<ActionProvider>();
            
            return services;
        }
    }
}