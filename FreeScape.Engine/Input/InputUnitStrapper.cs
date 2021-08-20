using AsperandLabs.UnitStrap.Core.Abstracts;
using FreeScape.Engine.Input.Config.Action;
using FreeScape.Engine.Input.Controllers;
using FreeScape.Engine.Physics.Movement;
using Microsoft.Extensions.DependencyInjection;

namespace FreeScape.Engine.Input
{
    public class InputUnitStrapper : BaseUnitStrapper
    {
        public override string Namespace => GetType().Assembly.ToString();
        protected override IServiceCollection RegisterInternalDependencies(IServiceCollection services)
        {
            services.AddSingleton<SfmlActionResolver>();
            services.AddSingleton<Movement>();
            services.AddScoped<ActionProvider>();
            services.AddScoped<KeyboardController>();
            services.AddScoped<UserInputController>();

            return services;
        }
    }
}