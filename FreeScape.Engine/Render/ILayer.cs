namespace FreeScape.Engine.Render
{
    public interface ILayer :IRenderable
    {
        int ZIndex { get; set; }

        void Tick();
    }
}