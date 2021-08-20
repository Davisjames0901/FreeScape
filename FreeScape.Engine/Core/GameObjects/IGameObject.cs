using System.Numerics;
using FreeScape.Engine.Render;

namespace FreeScape.Engine.Core.GameObjects
{
    public interface IGameObject : IRenderable, ITickable
    {
        Vector2 Size { get; }
        Vector2 Position { get; }
        Vector2 Scale { get; }

        public abstract void Init();

    }
}