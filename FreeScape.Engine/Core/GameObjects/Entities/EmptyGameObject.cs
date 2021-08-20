using System.Numerics;
using SFML.Graphics;

namespace FreeScape.Engine.Core.GameObjects.Entities
{
    public class EmptyGameObject : IGameObject
    {
        public Vector2 Size { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; } = Vector2.Zero;

        public EmptyGameObject(Vector2 position)
        {
            Position = position;
        }

        public void Render(RenderTarget target)
        {
        }

        public void Tick()
        {
        }
        public void Init()
        {

        }
    }
}
