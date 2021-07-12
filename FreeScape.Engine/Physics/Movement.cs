using FreeScape.Engine.Providers;
using System.Collections.Generic;
using System.Numerics;
using FreeScape.Engine.Physics.Colliders;
using FreeScape.Engine.Utilities;

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
            var velocity = Maths.GetVelocity(movable.HeadingVector, movable.Speed, deltaTime);
            var newPosition = movable.Position + velocity;
            movable.Position = CheckCollision(movable, newPosition);
        }

        public Vector2 CheckCollision(IMovable sourceMovable, Vector2 desiredPosition)
        {
            foreach (var target in Colliders)
            {
                if (target.Collides(desiredPosition))
                {
                    var onlyX = new Vector2(desiredPosition.X, sourceMovable.Position.Y);
                    if (!target.Collides(onlyX))
                    {
                        return onlyX;
                    }
                    var onlyY = new Vector2(sourceMovable.Position.X, desiredPosition.Y);
                    if (!target.Collides(onlyY))
                    {
                        return onlyY;
                    }
                    return sourceMovable.Position;
                }
            }
            return desiredPosition;
        }

        public void RegisterCollider(ICollider collider)
        {
            Colliders.Add(collider);
        }
    }

}
