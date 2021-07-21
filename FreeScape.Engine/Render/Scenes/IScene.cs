using System;
using SFML.Graphics;

namespace FreeScape.Engine.Render.Scenes
{
    public interface IScene :IDisposable
    {
        bool Active { get; set; }
        void Tick();
        void Init();
        void RenderScreen(RenderTarget target);
        void RenderWorld(RenderTarget target);
    }
}