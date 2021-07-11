using FreeScape.Engine.GameObjects;
using FreeScape.Engine.Physics;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace FreeScape.Engine.Render.Layers
{
    public abstract class GameObjectLayer : ILayer
    {
        public abstract int ZIndex { get; }
        protected List<IGameObject> _gameObjects;

        private Movement _movement;

        public GameObjectLayer(Movement movement)
        {
            _movement = movement;
            _gameObjects = new List<IGameObject>();
        }

        public abstract void Init();

        public void Render(RenderTarget target)
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Render(target);
            }
        }

        public virtual void Tick()
        {
            foreach(IGameObject gameObject in _gameObjects)
            {
                if(gameObject is IMovable movable)
                {
                    _movement.BasicMove(movable);
                }
                gameObject.Tick();
            }
        }

    }
}
