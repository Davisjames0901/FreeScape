using System.Numerics;

namespace FreeScape.Engine.Physics.Movements
{
    public class HeadingVector
    {
        public Vector2 Vector { get; private set; }
        public Direction Direction { get; private set; }

        public Direction CollapseDirection()
        {
            if (Direction.HasFlag(Direction.Left)) return Direction.Left;
            if (Direction.HasFlag(Direction.Right)) return Direction.Right;
            if (Direction.HasFlag(Direction.Up)) return Direction.Up;
            if (Direction.HasFlag(Direction.Down)) return Direction.Down;
            return Direction.None;
        }


        internal void UpdateHeadingVector(Vector2 newHeading)
        {
            Vector = newHeading;
            Direction = GetDirectionFromHeading(Vector);
        }

        private Direction GetDirectionFromHeading(Vector2 heading) => heading switch
        {
            {
                X: > 0, Y: > 0
            } => Direction.Down & Direction.Right,
            {
                X: < 0, Y: > 0
            } => Direction.Down & Direction.Left,
            {
                X: > 0, Y: < 0
            } => Direction.Up & Direction.Right,
            {
                X: < 0, Y: < 0
            } => Direction.Up & Direction.Left,
            {
                X:
                0,
                Y: < 0
            } => Direction.Up,
            {
                X:
                0,
                Y: > 0
            } => Direction.Down,
            {
                X: > 0, Y:
                0
            } => Direction.Right,
            {
                X: < 0, Y:
                0
            } => Direction.Left,
            _ => Direction.None
        };
    }
}