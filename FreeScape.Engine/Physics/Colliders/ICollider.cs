using System.Numerics;

namespace FreeScape.Engine.Physics.Colliders
{
    public interface ICollider
    {
        public Vector2? GetIntersectionPoint(Line line);
    }
}