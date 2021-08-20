using AsperandLabs.UnitStrap.Core.Abstracts;
using FreeScape.Engine.Render.Animations;
using FreeScape.Engine.Render.Animations.AnimationTypes;
using FreeScape.Engine.Render.Layers;
using FreeScape.Engine.Render.Textures;
using Microsoft.Extensions.DependencyInjection;

namespace FreeScape.Engine.Render
{
    public class RenderUnitStrapper : BaseUnitStrapper
    {
        public override string Namespace => GetType().Assembly.ToString();
        protected override IServiceCollection RegisterInternalDependencies(IServiceCollection services)
        {
            services.AddSingleton<AnimationProvider>();
            services.AddTransient<CyclicAnimation>();
            services.AddTransient<OneShotAnimation>();
            services.AddSingleton<LayerProvider>();
            services.AddSingleton<TextureProvider>();
            services.AddSingleton<MapProvider>();

            return services;
        }
    }
}