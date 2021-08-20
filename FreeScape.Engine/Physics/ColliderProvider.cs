using System.Numerics;
using FreeScape.Engine.Physics.Collisions.Colliders;

namespace FreeScape.Engine.Physics
{
    public class ColliderProvider
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
