using FreeScape.Engine.Core.GameObjects;
using FreeScape.Engine.Core.Utilities;
using FreeScape.Engine.Physics.Collisions;
using Microsoft.Extensions.DependencyInjection;

namespace FreeScape.Engine.Core
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