using FreeScape.Engine.Render;
using SFML.Graphics;

namespace FreeScape.Engine.GameObjects.UI
{
    public abstract class Button : IRenderable
    {
        public void Render(RenderTarget target)
        {
            throw new System.NotImplementedException();
        }

        public abstract void OnClick();
    }
}