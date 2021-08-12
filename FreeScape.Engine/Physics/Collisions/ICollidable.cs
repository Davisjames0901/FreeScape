using FreeScape.Engine.Physics.Collisions.Colliders;

namespace FreeScape.Engine.Physics
{
    public interface ICollidable
    {
        public ICollider Collider { get; }
        public ColliderType ColliderType { get; }
        public void CollisionEnter(ICollidable collidable);

    }
}