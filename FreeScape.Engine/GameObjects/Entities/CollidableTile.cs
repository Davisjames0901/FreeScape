using System.Numerics;
using FreeScape.Engine.Config.TileSet;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Physics.Colliders;
using SFML.Graphics;

namespace FreeScape.Engine.GameObjects.Entities
{
    public class CollidableTile : Tile , ICollidable
    {
        public CollidableTile(Vector2 position, Vector2 size, CachedTileSetTile tileInfo, Texture texture) : base(
            position, size, tileInfo, texture)
        {
            Collider = new RectangleCollider(size, position);
        }
        
        public ICollider Collider { get; }

        public void CollisionEnter(ICollidable collidable)
        {
            throw new System.NotImplementedException();
        }
    }
}