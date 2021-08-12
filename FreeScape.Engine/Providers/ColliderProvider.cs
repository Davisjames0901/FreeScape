using FreeScape.Engine.Physics.Collisions.Colliders;
using System.Numerics;

namespace FreeScape.Engine.Providers
{
    public class ColliderProvider : CollidableProvidable
    {
        public ColliderProvider()
        {
        }
        public CircleCollider CreateCircleCollider(Vector2 position, Vector2 center, float radius)
        {
            CircleCollider collider = new CircleCollider(position, center, radius);

            return collider;
        }
        public RectangleCollider CreateRectangleCollider(Vector2 size, Vector2 position)
        {
            RectangleCollider collider = new RectangleCollider(size, position);

            return collider;
        }


    }
}
