using System;
using System.Numerics;
using FreeScape.Engine.Utilities;

namespace FreeScape.Engine.Physics.Colliders
{
    public class CircleCollider : ICollider
    {
        public float Size { get; }
        public Vector2 Position { get; }
        public Vector2 Center { get; }
        public float Radius { get; }

        public CircleCollider(Vector2 position, Vector2 center, float radius)
        {
            Position = position;
            Center = center;
            Radius = radius;
        }

        public Vector2? GetIntersectionPoint(Line line)
        {
            return null;
        }

        public bool Collides(Vector2 point)
        {
            var distance = Math.Sqrt(Math.Pow(point.X - Center.X, 2) + Math.Pow(point.Y - Center.Y, 2));
            Console.WriteLine(distance);
            return distance < Radius;
        }
    }
}