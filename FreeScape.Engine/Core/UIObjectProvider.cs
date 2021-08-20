using FreeScape.Engine.Core.GameObjects.UI;
using FreeScape.Engine.Core.Utilities;
using FreeScape.Engine.Input;
using FreeScape.Engine.Render.Textures;
using Microsoft.Extensions.DependencyInjection;

namespace FreeScape.Engine.Core
{
    public class UIObjectProvider
    {
        private readonly ServiceScopeProvider _provider;

        public UIObjectProvider(ServiceScopeProvider scope)
        {
            _provider = scope;
        }
        public T Provide<T>() where T : IUIObject
        {
            return _provider.CurrentScope.ServiceProvider.GetService<T>();
        }
        public Button CreateButton(ButtonInfo info)
        {
            var texture = _provider.CurrentScope.ServiceProvider.GetService<TextureProvider>();
            var actionProvider = _provider.CurrentScope.ServiceProvider.GetService<ActionProvider>();
            var frameTime = _provider.CurrentScope.ServiceProvider.GetService<FrameTimeProvider>();
            return new Button(info, texture.GetTextureByFile(info.ButtonTexture, $"{info.ButtonTexture}:default"), actionProvider, frameTime);
        }
    }
}