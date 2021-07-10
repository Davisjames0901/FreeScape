using FreeScape.Engine.GameObjects.UI;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeScape.Engine.Render.Layers
{
    public abstract class UILayer : ILayer
    {
        public List<IUIObject> UIObjects;
        public int ZIndex { get; }

        public UILayer()
        {
            UIObjects = new List<IUIObject>();
        }

        public abstract void Init();

        public void Render(RenderTarget target)
        {
            foreach(var UIObject in UIObjects)
            {
                UIObject.Render(target);
            }
        }

        public void Tick()
        {
            foreach (var UIObject in UIObjects)
            {
                UIObject.Tick();
            }
        }
    }
}
