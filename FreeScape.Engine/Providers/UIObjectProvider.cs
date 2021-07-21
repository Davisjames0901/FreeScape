using FreeScape.Engine.GameObjects.UI;
using Microsoft.Extensions.DependencyInjection;
using FreeScape.Engine.Config.UI;
using FreeScape.Engine.Managers;

namespace FreeScape.Engine.Providers
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