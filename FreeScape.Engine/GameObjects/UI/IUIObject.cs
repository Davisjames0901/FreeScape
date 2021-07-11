using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FreeScape.Engine.GameObjects.UI
{
    public interface IUIObject : IGameObject
    {
        bool Hovered { get; set; }
        void Render(RenderTarget target);
        void Tick();
        void OnHover();
        void OnHoverEnd();
    }
}
