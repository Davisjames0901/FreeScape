using System.Collections.Generic;
using System.Linq;
using FreeScape.Engine;
using FreeScape.Engine.Event;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render;
using FreeScape.Layers;
using SFML.Graphics;

namespace FreeScape.Scenes
{
    public class TestScene : IScene
    {
        private readonly TiledMapRenderer _mapRenderer;
        private readonly MapInfo _map;
        private readonly List<ILayer> _layers;
        private bool _isDirty = true;

        public TestScene(MapProvider mapProvider, TiledMapRenderer mapRenderer, EventManager events)
        {
            _layers = new List<ILayer>();
            _mapRenderer = mapRenderer;
            _map = mapProvider.GetMap("TestMap");
            var player = new Player();
            _layers.Add(player);
            events.RegisterEventListener(player);
        }
        
        public void Render(RenderTarget target)
        {
            _mapRenderer.RenderTileMap(target, _map);
            foreach (var layer in _layers.OrderBy(x => x.ZIndex))
            {
                layer.Render(target);
            }
        }

        public bool Tick()
        {
            foreach (var layer in _layers.OrderBy(x => x.ZIndex))
            {
                layer.Tick();
            }
            return true;
        }
        
        public void Dispose()
        {
        }
    }
}