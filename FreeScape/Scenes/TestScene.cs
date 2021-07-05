using FreeScape.Engine.Managers;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render.Scenes;
using FreeScape.Layers;

namespace FreeScape.Scenes
{
    public class TestScene : LayeredScene
    {
        private readonly DisplayManager _displayManager;
        private readonly LayerProvider _layerProvider;

        public TestScene(DisplayManager displayManager, ActionProvider actionProvider, LayerProvider layerProvider)
        {
            _displayManager = displayManager;
            _layerProvider = layerProvider;
            actionProvider.SwitchActionMap("Player");
        }

        public override void Init()
        {
            var player = _layerProvider.Provide<Player>();
            Layers.Add(player);
            _displayManager.Track(x=> x.Name == "main", player);

            var map = _layerProvider.Provide<TestTileMap>();
            Layers.Add(map);
        }

        public override void Dispose()
        {
        }
    }
}