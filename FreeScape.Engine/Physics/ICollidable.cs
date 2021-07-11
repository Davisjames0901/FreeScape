using FreeScape.Engine.Physics.Colliders;

namespace FreeScape.Engine.Physics
{
    public interface ICollidable
    {
        public ICollider Collider { get; }
    }
}