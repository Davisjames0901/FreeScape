using SFML.Graphics;
using System;
using System.Numerics;

namespace FreeScape.Engine.GameObjects.UI
{
    public interface IButton : IUIObject
    {
        Texture ButtonTextureDefault { get; set; }
        Texture ButtonTextureHover { get; set; }
        void OnClick();
        
    }
}