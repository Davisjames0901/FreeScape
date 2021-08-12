using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using FreeScape.Engine.Utilities;

namespace FreeScape.Engine.Physics.Collisions.Colliders
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
            _vertices.Add(Vector2.Zero);
            _vertices.Add(new Vector2(Size.X, 0));
            _vertices.Add(Size);
            _vertices.Add(new Vector2(0, Size.Y));
        }

        public Vector2? GetIntersectionPoint(Line line)
        {
            return null;
        }
        
        public bool Collides(Vector2 point)
        {
            return (Maths.Floor(point).IsGreaterThanOrEquals(Maths.Floor(Position))) && (Maths.Ceiling(point).IsLessThanOrEquals(Maths.Ceiling(Position + Size)));
        }

        public Vector2[] GetVerticesRelativeToPosition()
        {
            return _vertices.Select(x => x + Position).ToArray();
        }
    }
}