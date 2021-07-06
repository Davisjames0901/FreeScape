using FreeScape.Engine.Config.Map;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render.Layers;

namespace FreeScape.Layers
{
    public class TestTileMap : TiledMapLayer
    {
        private readonly MapProvider _mapProvider;

        public TestTileMap(TextureProvider textureProvider, MapProvider mapProvider) : base(textureProvider)
        {
            _mapProvider = mapProvider;
        }

        public override MapInfo Map => _mapProvider.GetMap("TestMap");
        public override int ZIndex => 0;
        public override void Tick()
        {
            
        }
    }
}