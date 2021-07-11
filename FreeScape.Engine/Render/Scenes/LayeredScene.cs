using System.Collections.Generic;
using System.Linq;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Render.Layers;
using SFML.Graphics;

namespace FreeScape.Engine.Render.Scenes
{
    public abstract class LayeredScene : IScene
    {
        public LayeredScene()
        {
            Layers = new List<ILayer>();
        }
        
        public List<ILayer> Layers { get; }

        public bool Active { get; set; }

        public virtual void Render(RenderTarget target)
        {
            foreach (var layer in Layers.OrderBy(x => x.ZIndex))
            {
                layer.Render(target);
            }
        }
        
        public virtual void Tick()
        {
            foreach (var layer in Layers)
            {
                if(layer is GameObjectLayer gameObjectLayer)
                {
                    
                }
                layer.Tick();
            }
        }
        public abstract void Init();

        public abstract void Dispose();
    }
}