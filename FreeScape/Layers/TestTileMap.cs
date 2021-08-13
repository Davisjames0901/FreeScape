using FreeScape.Engine.Config.Map;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Physics.Collisions;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render.Layers;

namespace FreeScape.Layers
{
    public class TestTileMap : TiledMapLayer
    {
        private readonly MapProvider _mapProvider;
        

        public TestTileMap(CollisionEngine collisionEngine, TileSetProvider tileSetProvider, MapProvider mapProvider, Movement movement) : base(tileSetProvider, mapProvider, movement, collisionEngine)
        {
            _mapProvider = mapProvider;
        }

        public override MapInfo Map => _mapProvider.GetMap("TiledTestMap");
        public override int ZIndex => 0;
        public override void Tick()
        {
            
        }
    }
}