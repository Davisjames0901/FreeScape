using FreeScape.Engine.Render;
using SFML.System;

namespace FreeScape.Engine.GameObjects
{
    public interface IGameObject : IRenderable, ITickable
    {
        float Size { get; }
        Vector2f Position { get; }
        bool Collidable { get; set; }

    }
}