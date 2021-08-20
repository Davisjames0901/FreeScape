using SFML.Graphics;

namespace FreeScape.Engine.Core.GameObjects.UI
{
    public interface IButton : IUIObject
    {
        Texture ButtonTexture { get; set; }
        void OnClick();
        
    }
}