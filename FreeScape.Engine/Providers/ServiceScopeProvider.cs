using Microsoft.Extensions.DependencyInjection;

namespace FreeScape.Engine.Providers
{
    public class ServiceScopeProvider
    {
        public IServiceScope CurrentScope { get; set; }
    }
}