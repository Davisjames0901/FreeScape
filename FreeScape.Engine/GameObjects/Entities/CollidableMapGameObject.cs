using FreeScape.Engine.Config.TileSet;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Physics.Colliders;
using SFML.Graphics;
using System.Numerics;

namespace FreeScape.Engine.GameObjects.Entities
{
    public class CollidableMapGameObject : MapGameObject, ICollidable
    {

        public ICollider Collider { get; }
        public CollidableMapGameObject(Vector2 position, Vector2 size, float rotation, CachedTileSetTile tileInfo, Texture texture) : base(
            position, size, rotation, tileInfo, texture)
        {

            Collider = new CircleCollider( position, position + (size/2), size.X/2);
        }
    }
}
