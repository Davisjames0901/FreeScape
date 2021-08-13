
using FreeScape.Engine.GameObjects;
using FreeScape.Engine.Physics;
using Microsoft.Extensions.DependencyInjection;

namespace FreeScape.Engine.Providers
{
    public class GameObjectProvider
    {
        private readonly ServiceScopeProvider _provider;

        public GameObjectProvider(ServiceScopeProvider scope)
        {
            _provider = scope;
        }
        public T Provide<T>() where T : IGameObject
        {
            return _provider.CurrentScope.ServiceProvider.GetService<T>();
        }
    }
}