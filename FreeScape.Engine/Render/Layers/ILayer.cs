using FreeScape.Engine.GameObjects;
using SFML.System;

namespace FreeScape.Engine.Render.Layers
{
    public interface ILayer :IRenderable
    {
        RenderMode RenderMode { get; }
        int ZIndex { get; }

        void Tick();

        void Init();

    }
}