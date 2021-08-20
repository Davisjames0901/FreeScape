using AsperandLabs.UnitStrap.Core.Abstracts;
using Microsoft.Extensions.DependencyInjection;

namespace FreeScape.Engine.Sfx
{
    public class SoundUnitStrapper : BaseUnitStrapper
    {
        public override string Namespace => GetType().Assembly.ToString();
        protected override IServiceCollection RegisterInternalDependencies(IServiceCollection services)
        {
            services.AddSingleton<SoundProvider>();

            return services;
        }
    }
}