using FreeScape.Engine.Physics.Collisions.Colliders;
using System.Collections.Generic;

namespace FreeScape.Engine.Physics
{
    public interface ICollidable
    {
        public List<ICollider> Colliders { get; }
        public void CollisionEnter(ICollidable collidable);

    }
}