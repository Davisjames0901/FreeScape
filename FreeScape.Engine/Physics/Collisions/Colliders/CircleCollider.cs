using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using FreeScape.Engine.Utilities;

namespace FreeScape.Engine.Physics.Colliders
{
    public class CircleCollider : ICollider
    {
        public float Size { get; }
        public Vector2 Position { get; set; }
        public Vector2 Center { get; }
        public float Radius { get; }
        private List<Vector2> _vertices;
        public Vector2[] Vertices => _vertices.ToArray();

        public CircleCollider(Vector2 position, Vector2 center, float radius)
        {
            Position = position;
            Center = center;
            Radius = radius;

            _vertices = new List<Vector2>();
            var totalPoints = (int)radius * 3;
            var degreeIncrements = 360 / totalPoints;
            for (int i = 0; i < 360; i += degreeIncrements)
            {
                _vertices.Add(new Vector2((float)(radius * Math.Sin(i)), (float)(radius * Math.Cos(i))));
            }
        }

        public Vector2? GetIntersectionPoint(Line line)
        {
            return null;
        }

        public bool Collides(Vector2 point)
        {
            var distance = Math.Sqrt(Math.Pow(point.X - Center.X, 2) + Math.Pow(point.Y - Center.Y, 2));
            return distance < Radius;
        }

        public Vector2[] GetVerticesRelativeToPosition()
        {
            return _vertices.Select(x => x + Center).ToArray();
        }
    }
}