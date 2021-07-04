using AsperandLabs.UnitStrap.Core.Abstracts;
using Microsoft.Extensions.DependencyInjection;

namespace FreeScape.Engine
{
    public class EngineUnitstrapper : BaseUnitStrapper
    {
        public override string Namespace => GetType().Assembly.ToString();
        protected override IServiceCollection RegisterInternalDependencies(IServiceCollection services)
        {
            services.AddSingleton<Game>();
            services.AddSingleton<SceneManager>();
            
            return services;
        }
    }
}