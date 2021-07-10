using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FreeScape.Engine.GameObjects.UI
{
    public interface IUIObject
    {
        Vector2 Position { get; set; }
        Vector2 Size { get; set; }
        bool Hovered { get; set; }
        void Render(RenderTarget target);
        void Tick();
        void OnHover();
        void OnHoverEnd();
    }
}
