namespace FreeScape.Engine.Render.Layers
{
    public interface ILayer :IRenderable
    {
        int ZIndex { get; }

        void Tick();
    }
}