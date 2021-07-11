
using FreeScape.Engine.GameObjects.UI;
using Microsoft.Extensions.DependencyInjection;
using SFML.Graphics;
using System;
using System.Numerics;

namespace FreeScape.Engine.Providers
{
    public class UIObjectProvider
    {
        private readonly ServiceScopeProvider _provider;
        private readonly TextureProvider _textureProvider;
        private readonly ActionProvider _actionProvider;

        public UIObjectProvider(ServiceScopeProvider scope, TextureProvider textureProvider, ActionProvider actionProvider)
        {
            _textureProvider = textureProvider;
            _actionProvider = actionProvider;
            _provider = scope;
        }
        public T Provide<T>() where T : IUIObject
        {
            return _provider.CurrentScope.ServiceProvider.GetService<T>();
        }
        public Button CreateButton(string name, Vector2 size, Vector2 position, string defaultFilePath, string hoverFilePath, Action onClick)
        {
            Button button;

            Vector2 buttonSize = size;

            Vector2 playButtonPos = new Vector2(0, 0);


            Texture playButtonDefaultTexture = _textureProvider.GetTextureByFile(defaultFilePath, $"{name}:default");
            Texture playButtonHoverTexture = _textureProvider.GetTextureByFile(hoverFilePath, $"{name}:hover");

            button = new Button(position, size, playButtonDefaultTexture, playButtonHoverTexture, onClick, _actionProvider);

            return button;
        }
    }
}