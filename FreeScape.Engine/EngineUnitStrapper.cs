using System.Text.Json;
using AsperandLabs.UnitStrap.Core.Abstracts;
using AsperandLabs.UnitStrap.Core.Extenstions;
using FreeScape.Engine.Core;
using FreeScape.Engine.Input;
using FreeScape.Engine.Physics;
using Microsoft.Extensions.DependencyInjection;
using FreeScape.Engine.Render;
using FreeScape.Engine.Sfx;

namespace FreeScape.Engine
{
    public class EngineUnitStrapper : BaseUnitStrapper<GameInfo>
    {
        public override string Namespace => GetType().Assembly.ToString();
        protected override IServiceCollection RegisterInternalDependencies(IServiceCollection services, GameInfo info)
        {
            services.AddSingleton<Game>();
            services.AddSingleton(info);

            services.AddUnit<RenderUnitStrapper>();
            services.AddUnit<CoreUnitStrapper>();
            services.AddUnit<InputUnitStrapper>();
            services.AddUnit<PhysicsUnitStrapper>();
            services.AddUnit<SoundUnitStrapper>();

            services.AddSingleton(new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            
            return services;
        }
    }
}