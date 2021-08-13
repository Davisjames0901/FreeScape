
using FreeScape.Engine.GameObjects;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Physics.Collisions;
using Microsoft.Extensions.DependencyInjection;

namespace FreeScape.Engine.Providers
{
    public class GameObjectProvider
    {
        private readonly ServiceScopeProvider _provider;
        private readonly CollisionEngine _collisionEngine;

        public GameObjectProvider(ServiceScopeProvider scope, CollisionEngine collisionEngine)
        {
            _collisionEngine = collisionEngine;
            _provider = scope;
        }
        public T Provide<T>() where T : IGameObject
        {
            var gameObject = _provider.CurrentScope.ServiceProvider.GetService<T>();
            if(gameObject is ICollidable collidable)
            {
                _collisionEngine.RegisterGameObjectCollidable(collidable);
            }
            return gameObject;
        }
    }
}