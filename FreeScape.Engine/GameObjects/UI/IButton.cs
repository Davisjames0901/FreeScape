using SFML.Graphics;
using System;
using System.Numerics;

namespace FreeScape.Engine.GameObjects.UI
{
    public interface IButton : IUIObject
    {
        Texture ButtonTexture { get; set; }
        void OnClick();
        
    }
}