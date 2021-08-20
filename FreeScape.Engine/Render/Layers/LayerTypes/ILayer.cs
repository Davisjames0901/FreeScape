namespace FreeScape.Engine.Render.Layers.LayerTypes
{
    public interface ILayer :IRenderable
    {
        RenderMode RenderMode { get; }
        int ZIndex { get; }

        void Tick();

        void Init();

    }
}