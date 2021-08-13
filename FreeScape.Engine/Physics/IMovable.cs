using System.Numerics;
using FreeScape.Engine.GameObjects;
using FreeScape.Engine.Physics.Movements;
using SFML.System;

namespace FreeScape.Engine.Physics
{
    public interface IMovable : IGameObject
    {
        float Weight { get; }
        float Speed { get; }
        HeadingVector HeadingVector { get; }
        new Vector2 Position { get; set; }
    }
}
