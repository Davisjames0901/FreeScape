using FreeScape.Engine.Render;
using SFML.System;

namespace FreeScape.Engine.GameObjects
{
    public interface IGameObject : IRenderable
    {
        float X { get; }
        float Y { get; }
        float Size { get; }
        Vector2f Position { get; }
    }
}