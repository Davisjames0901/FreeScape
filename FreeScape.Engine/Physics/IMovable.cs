using System.Numerics;
using FreeScape.Engine.GameObjects;
using SFML.System;

namespace FreeScape.Engine.Physics
{
    public interface IMovable : IGameObject
    {
        float Weight { get; }
        Vector2 Velocity { get; }
        new Vector2 Position { get; set; }
    }
}
