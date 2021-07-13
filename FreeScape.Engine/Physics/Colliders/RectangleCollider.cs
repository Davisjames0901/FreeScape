using System.Collections.Generic;
using System.Numerics;
using FreeScape.Engine.Utilities;

namespace FreeScape.Engine.Physics.Colliders
{
    public class RectangleCollider : ICollider
    {
        public Vector2 Size { get; }
        public Vector2 Position { get; }
        private List<Vector2> _vertices;
        public Vector2[] Vertices => _vertices.ToArray();
        public RectangleCollider(Vector2 size, Vector2 position)
        {
            Size = size;
            Position = position;
            _vertices = new List<Vector2>();
            _vertices.Add(Position);
            _vertices.Add(Position + new Vector2(Size.X, 0));
            _vertices.Add(Position + Size);
            _vertices.Add(Position + new Vector2(0, Size.Y));
        }

        public Vector2? GetIntersectionPoint(Line line)
        {
            return null;
        }
        
        public bool Collides(Vector2 point)
        {
            return (Maths.Floor(point).IsGreaterThanOrEquals(Maths.Floor(Position))) && (Maths.Ceiling(point).IsLessThanOrEquals(Maths.Ceiling(Position + Size)));
        }
    }
}