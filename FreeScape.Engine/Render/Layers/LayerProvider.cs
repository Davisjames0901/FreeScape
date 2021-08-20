using FreeScape.Engine.Core.Utilities;
using FreeScape.Engine.Render.Layers.LayerTypes;
using Microsoft.Extensions.DependencyInjection;

namespace FreeScape.Engine.Render.Layers
{
    public class LayerProvider
    {
        private readonly ServiceScopeProvider _provider;

        public LayerProvider(ServiceScopeProvider scope)
        {
            _provider = scope;
        }
        public T Provide<T>() where T : ILayer
        {
            var layer = _provider.CurrentScope.ServiceProvider.GetService<T>();
            layer.Init();
            return layer;
        }
    }
}