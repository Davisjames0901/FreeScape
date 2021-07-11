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
        void OnHover();
        void OnHoverEnd();
    }
}
