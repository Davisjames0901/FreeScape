using Microsoft.Extensions.DependencyInjection;

namespace FreeScape.Engine.Core.Utilities
{
    public class ServiceScopeProvider
    {
        public IServiceScope CurrentScope { get; set; }
    }
}