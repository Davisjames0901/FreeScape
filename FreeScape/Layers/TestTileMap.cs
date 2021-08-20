using FreeScape.Engine.Physics;
using FreeScape.Engine.Physics.Collisions;
using FreeScape.Engine.Physics.Movement;
using FreeScape.Engine.Render.Layers;
using FreeScape.Engine.Render.Layers.LayerTypes;
using FreeScape.Engine.Render.Textures;
using FreeScape.Engine.Render.Tiled;

namespace FreeScape.Layers
{
    public class TestTileMap : TileMapLayer
    {
        private readonly MapProvider _mapProvider;
        

        public TestTileMap(CollisionEngine collisionEngine, MapProvider mapProvider, Movement movement, TextureProvider textureProvider) : base(textureProvider, mapProvider, movement, collisionEngine)
        {
            _mapProvider = mapProvider;
        }

        public override TiledMap Map => _mapProvider.GetMap("TiledTestMap");
        public override int ZIndex => 0;
        public override void Tick()
        {
            
        }
    }
}