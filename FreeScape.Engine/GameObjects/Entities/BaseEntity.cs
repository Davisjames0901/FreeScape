using System.Collections.Generic;
using System.Numerics;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Physics.Collisions.Colliders;
using FreeScape.Engine.Physics.Movements;
using SFML.Graphics;

namespace FreeScape.Engine.GameObjects.Entities
{
    public abstract class BaseEntity : IMovable, ICollidable 
    {
        public Vector2 Size { get; }
        public float Weight { get; }
        public float Speed { get; }
        public HeadingVector HeadingVector { get; }
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; }
        public List<ICollider> Colliders { get; }
        
        
        public void Render(RenderTarget target)
        {
            throw new System.NotImplementedException();
        }

        public void Tick()
        {
            throw new System.NotImplementedException();
        }

        public void Init()
        {
            throw new System.NotImplementedException();
        }

        public abstract void CollisionEnter(ICollidable collidable);
    }
}