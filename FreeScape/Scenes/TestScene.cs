using FreeScape.Engine;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render;
using SFML.Graphics;

namespace FreeScape.Scenes
{
    public class TestScene : IScene
    {
        private readonly TiledMapRenderer _mapRenderer;
        private readonly MapInfo _map;

        public TestScene(MapProvider mapProvider, TiledMapRenderer mapRenderer)
        {
            _mapRenderer = mapRenderer;
            _map = mapProvider.GetMap("TestMap");
        }
        
        public void Render(RenderTarget target)
        {
            _mapRenderer.RenderTileMap(target, _map);
        }

        public bool Tick()
        {
            return true;
        }
        
        public void Dispose()
        {
        }
    }
}