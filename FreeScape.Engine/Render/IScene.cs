using System;
using SFML.Graphics;

namespace FreeScape.Engine
{
    public interface IScene :IDisposable
    {
        void Render(RenderTarget target);
        bool Tick();
    }
}