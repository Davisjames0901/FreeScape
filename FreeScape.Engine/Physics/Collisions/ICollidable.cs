using System.Collections.Generic;
using FreeScape.Engine.Physics.Collisions.Colliders;

namespace FreeScape.Engine.Physics.Collisions
{
    public interface ICollidable
    {
        public List<ICollider> Colliders { get; }
        public void CollisionEnter(ICollidable collidable);

    }
}