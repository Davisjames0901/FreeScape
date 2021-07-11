using System.Numerics;
using FreeScape.Engine.GameObjects;
using SFML.System;

namespace FreeScape.Engine.Physics
{
    public interface IMovable : IGameObject
    {
        float Weight { get; }
        float Speed { get; }
        Vector2 HeadingVector { get; }
        new Vector2 Position { get; set; }
    }
}
