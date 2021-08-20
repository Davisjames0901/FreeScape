using System;

namespace FreeScape.Engine.Physics.Movement
{
    [Flags]
    public enum Direction
    {
        Up = 1,
        Down = 2,
        Left = 4,
        Right = 8,
        None = 16
    }
}