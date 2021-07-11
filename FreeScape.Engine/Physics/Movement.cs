using FreeScape.Engine.Providers;
using System.Collections.Generic;
using System.Numerics;
using FreeScape.Engine.Physics.Colliders;

namespace FreeScape.Engine.Physics
{
    public class Movement
    {
        FrameTimeProvider _frameTime;

        public List<ICollider> Colliders;
        public Movement(FrameTimeProvider frameTime)
        {
            _frameTime = frameTime;
            Colliders = new List<ICollider>();
        }
        public void BasicMove(IMovable movable)
        {
            var deltaTime = (float)_frameTime.DeltaTimeMilliSeconds;
            var velocity = movable.HeadingVector * movable.Speed;
            var newPosition = movable.Position + velocity * deltaTime;
            
            if(movable is ICollidable collidable)
                movable.Position = CheckCollision(collidable, movable, newPosition);
            movable.Position = newPosition;
        }

        public Vector2 CheckCollision(ICollidable source, IMovable souceMovable, Vector2 desiredPosition)
        {
            foreach (var target in Colliders)
            {
                //if()
            }

            return desiredPosition;
        }

        public void RegisterCollider(ICollider collider)
        {
            Colliders.Add(collider);
        }
    }

}
