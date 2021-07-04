using FreeScape.Engine;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render;
using SFML.Graphics;

namespace FreeScape.Scenes
{
    public class Menu
    {
        private bool _isDirty = true;

        public Menu()
        { }

        public void Render(RenderTarget target)
        {
        }

        public bool Tick()
        {
            return true;
        }

        public void Dispose()
        {
        }
    }
}