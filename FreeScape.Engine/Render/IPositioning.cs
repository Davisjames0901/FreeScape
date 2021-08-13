using System.Numerics;

namespace FreeScape.Engine.Render
{
    public interface IPositioning
    {
        void Tick();
        public Vector2 Position { get; }
    }
}