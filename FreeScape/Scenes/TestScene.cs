using System.Collections.Generic;
using System.Linq;
using FreeScape.Engine;
using FreeScape.Engine.Actions;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render;
using FreeScape.Layers;
using SFML.Graphics;

namespace FreeScape.Scenes
{
    public class TestScene : IScene
    {
        private readonly TiledMapRenderer _mapRenderer;
        private readonly ActionProvider _actionProvider;
        private readonly MapInfo _map;
        private readonly List<ILayer> _layers;
        private bool _isDirty = true;

        public TestScene(MapProvider mapProvider, TiledMapRenderer mapRenderer, EventManager events, DisplayManager displayManager, ActionProvider actionProvider)
        {
            _layers = new List<ILayer>();
            _mapRenderer = mapRenderer;
            _actionProvider = actionProvider;
            _map = mapProvider.GetMap("TestMap");
            _actionProvider.SwitchActionMap("Player");
            var player = new Player(_actionProvider);
            _layers.Add(player);
            displayManager.Track(x=> x.Name == "main", player);
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