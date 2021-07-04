using FreeScape.Engine.Render;
using SFML.System;

namespace FreeScape.Engine.GameObjects
{
    public interface IGameObject : IRenderable
    {
        int X { get; set; }
        int Y { get; set; }
        public Vector2f Location => new Vector2f(X, Y);
    }
}