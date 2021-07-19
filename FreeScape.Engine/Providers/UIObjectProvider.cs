﻿
using FreeScape.Engine.GameObjects.UI;
using Microsoft.Extensions.DependencyInjection;
using SFML.Graphics;
using System;
using System.Numerics;
using FreeScape.Engine.Config.UI;

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
        public Button CreateButton(ButtonInfo info)
        {
            return new Button(info, _textureProvider.GetTextureByFile(info.ButtonTexture, $"{info.Name}:default"), _actionProvider);
        }
    }
}