namespace FreeScape.Engine.Core.GameObjects.UI
{
    public interface IUIObject : IGameObject
    {
        bool Hovered { get; set; }
        void OnHover();
        void OnHoverEnd();
    }
}
