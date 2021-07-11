using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FreeScape.Engine.GameObjects
{
    public class EmptyGameObject : IGameObject
    {
        public Vector2 Size { get; set; }

        public Vector2 Position { get; set; }

        public void Render(RenderTarget target)
        {
        }

        public void Tick()
        {
        }
        public void Init()
        {

        }
    }
}
