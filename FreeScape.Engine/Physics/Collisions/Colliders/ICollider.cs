using System.Collections.Generic;
using System.Numerics;

namespace FreeScape.Engine.Physics.Collisions.Colliders
{
    public interface ICollider
    {
        public Vector2? GetIntersectionPoint(Line line);
        public Vector2 Position { get; set; }
        public Vector2[] Vertices { get; }
        public bool Collides(Vector2 point);
        public ColliderType ColliderType { get; set; }
        Vector2[] GetVerticesRelativeToPosition();
    }
}