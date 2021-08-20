using AsperandLabs.UnitStrap.Core.Abstracts;
using FreeScape.Engine.Core.Managers;
using FreeScape.Engine.Core.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace FreeScape.Engine.Core
{
    public class CoreUnitStrapper : BaseUnitStrapper
    {
        public override string Namespace => GetType().Assembly.ToString();
        protected override IServiceCollection RegisterInternalDependencies(IServiceCollection services)
        {
            services.AddSingleton<SceneManager>();
            services.AddSingleton<DisplayManager>();
            services.AddSingleton<GameManager>();
            
            services.AddSingleton<GameObjectProvider>();
            services.AddSingleton<UIObjectProvider>();
            services.AddSingleton<FrameTimeProvider>();
            services.AddSingleton<ServiceScopeProvider>();

            return services;
        }
    }
}