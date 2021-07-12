using System.Numerics;

namespace FreeScape.Engine.Physics.Colliders
{
    public interface ICollider
    {
        public Vector2? GetIntersectionPoint(Line line);
        public bool Collides(Vector2 point);
    }
}