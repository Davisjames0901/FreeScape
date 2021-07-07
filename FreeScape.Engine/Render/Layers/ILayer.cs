using FreeScape.Engine.GameObjects;
using SFML.System;

namespace FreeScape.Engine.Render.Layers
{
    public interface ILayer :IRenderable
    {
        int ZIndex { get; }

        void Tick();

        void Init();

    }
}