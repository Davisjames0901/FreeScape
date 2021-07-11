
using FreeScape.Engine.GameObjects.UI;
using Microsoft.Extensions.DependencyInjection;

namespace FreeScape.Engine.Providers
{
    public class UIObjectProvider
    {
        private readonly ServiceScopeProvider _provider;

        public UIObjectProvider(ServiceScopeProvider scope)
        {
            _provider = scope;
        }
        public T Provide<T>() where T : IUIObject
        {
            return _provider.CurrentScope.ServiceProvider.GetService<T>();
        }
    }
}