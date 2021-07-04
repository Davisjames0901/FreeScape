using SFML.Graphics;

namespace FreeScape.Engine
{
    public interface ILayer
    {
        int ZIndex { get; set; }

        void Render(RenderTarget target);
        bool Tick();
    }
}