using FreeScape.Engine.GameObjects.UI;
using SFML.Graphics;
using System.Collections.Generic;

namespace FreeScape.Engine.Render.Layers
{
    public abstract class UILayer : ILayer
    {
        protected List<IUIObject> UIObjects;
        public abstract int ZIndex { get; }
        public Sprite Background;

        public UILayer()
        {
            UIObjects = new List<IUIObject>();
        }

        public abstract void Init();

        public virtual void Render(RenderTarget target)
        {
            if(Background is not null)
                target.Draw(Background);
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
