

using FreeScape.Engine.Physics;
using FreeScape.Engine.Render.Layers;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Managers;

namespace FreeScape.Layers
{
    public class EntityLayer : GameObjectLayer
    {

        private readonly GameObjectProvider _gameObjectProvider;
        private readonly DisplayManager _displayManager;

        public override int ZIndex => 999;

        public EntityLayer(GameObjectProvider gameObjectProvider, DisplayManager displayManager, Movement movement):base(movement)
        {
            _gameObjectProvider = gameObjectProvider;
            _displayManager = displayManager;
        }
        public override void Init()
        {
            var player = _gameObjectProvider.Provide<Player>();
            _gameObjects.Add(player);
            _displayManager.Track(x => x.Name == "main", player);
        }
    }
}
