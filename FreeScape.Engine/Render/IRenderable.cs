using SFML.Graphics;

namespace FreeScape.Engine.Render
{
    public interface IRenderable
    {
        void Render(RenderTarget target);
    }
}