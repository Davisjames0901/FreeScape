using System;

namespace FreeScape.Engine.Render
{
    public interface IScene :IDisposable, IRenderable
    {
        bool Tick();
    }
}