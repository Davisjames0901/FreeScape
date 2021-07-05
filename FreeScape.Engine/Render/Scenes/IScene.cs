using System;

namespace FreeScape.Engine.Render.Scenes
{
    public interface IScene :IDisposable, IRenderable
    {
        void Tick();
        void Init();
    }
}