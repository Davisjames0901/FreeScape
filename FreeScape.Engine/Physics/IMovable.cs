using FreeScape.Engine.GameObjects;
using SFML.System;

namespace FreeScape.Engine.Physics
{
    public interface IMovable : IGameObject
    {
        float Weight { get; }
        Vector2f Velocity { get; }
        new Vector2f Position { get; set; }
    }
}
