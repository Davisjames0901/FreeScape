using FreeScape.Engine.GameObjects.UI;
using SFML.Graphics;
using System.Collections.Generic;

namespace FreeScape.Engine.Render.Layers
{
    public abstract class UILayer : ILayer
    {
        protected List<IUIObject> UIObjects;
        public abstract int ZIndex { get; }

        public RenderMode RenderMode => RenderMode.Screen;

        public UILayer()
        {
            UIObjects = new List<IUIObject>();
        }

        public abstract void Init();

        public virtual void Render(RenderTarget target)
        {
            foreach(var UIObject in UIObjects)
            {
                UIObject.Render(target);
            }
        }

        public virtual void Tick()
        {
            foreach (var UIObject in UIObjects)
            {
                UIObject.Tick();
            }
        }
    }
}
