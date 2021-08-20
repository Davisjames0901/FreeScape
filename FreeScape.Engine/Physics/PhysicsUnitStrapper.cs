using AsperandLabs.UnitStrap.Core.Abstracts;
using FreeScape.Engine.Physics.Collisions;
using Microsoft.Extensions.DependencyInjection;

namespace FreeScape.Engine.Physics
{
    public class PhysicsUnitStrapper : BaseUnitStrapper
    {
        public override string Namespace => GetType().Assembly.ToString();
        protected override IServiceCollection RegisterInternalDependencies(IServiceCollection services)
        {
            services.AddSingleton<ColliderProvider>();
            services.AddSingleton<CollisionEngine>();

            return services;
        }
    }
}