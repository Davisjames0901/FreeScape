using FreeScape.Engine.Render;
using SFML.System;

namespace FreeScape.Engine.GameObjects
{
    public interface IGameObject : IRenderable
    {
        float X { get; }
        float Y { get; }
        public float Size { get; }
        public Vector2f Location => new Vector2f(X, Y);
    }
}