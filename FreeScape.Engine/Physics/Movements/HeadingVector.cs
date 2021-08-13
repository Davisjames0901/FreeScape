using System.Numerics;

namespace FreeScape.Engine.Physics.Movements
{
    public class HeadingVector
    {
        public Vector2 Vector { get; private set; }
        public Direction Direction { get; private set; }

        internal void UpdateHeadingVector(Vector2 newHeading)
        {
            Vector = newHeading;
            
        }

        private Direction GetDirectionFromHeading(Vector2 heading)
        {
            Direction? direction = heading switch
            {
                var v when v.X > 0 && v.Y > 0 => Direction.Down & Direction.Right,
                _ => Direction.None
            };
        }
    }
}