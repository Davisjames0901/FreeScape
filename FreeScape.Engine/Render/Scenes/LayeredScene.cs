using System.Collections.Generic;
using System.Linq;
using FreeScape.Engine.Render.Layers;
using FreeScape.Engine.Render.Layers.LayerTypes;
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

        public virtual void RenderWorld(RenderTarget target)
        {
            foreach (var layer in Layers.Where(x=> x.RenderMode == RenderMode.World).OrderBy(x => x.ZIndex))
            {
                layer.Render(target);
            }
        }
        public virtual void RenderScreen(RenderTarget target)
        {
            foreach (var layer in Layers.Where(x=> x.RenderMode == RenderMode.Screen).OrderBy(x => x.ZIndex))
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
                    gameObjectLayer.Tick();   
                }
                layer.Tick();
            }
        }
        public abstract void Init();

        public abstract void Dispose();
    }
}