using System.Numerics;
using FreeScape.Engine.Render;
using SFML.System;

namespace FreeScape.Engine.GameObjects
{
    public interface IGameObject : IRenderable, ITickable
    {
        Vector2 Size { get; }
        Vector2 Position { get; }

    }
}