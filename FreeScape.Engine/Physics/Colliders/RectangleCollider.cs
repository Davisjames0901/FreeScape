using System.Numerics;
using FreeScape.Engine.Utilities;

namespace FreeScape.Engine.Physics.Colliders
{
    public class RectangleCollider : ICollider
    {
        public Vector2 Size { get; }
        public Vector2 Position { get; }

        public RectangleCollider(Vector2 size, Vector2 position)
        {
            Size = size;
            Position = position;
        }

        public Vector2? GetIntersectionPoint(Line line)
        {
            return null;
        }

        public bool Collides(Vector2 point)
        {
            return point.IsGreaterThan(Position) && point.IsLessThan(Position + Size);
        }
    }
}