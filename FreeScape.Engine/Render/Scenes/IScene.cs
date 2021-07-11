using System;

namespace FreeScape.Engine.Render.Scenes
{
    public interface IScene :IDisposable, IRenderable
    {
        bool Active { get; set; }
        void Tick();
        void Init();
    }
}