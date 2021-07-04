using FreeScape.Engine.Render;

namespace FreeScape.Engine
{
    public interface IBaseLayer<T> : ILayer
    {
        T LayerState { get; set; }
    }
}