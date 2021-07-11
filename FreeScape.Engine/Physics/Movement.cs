using FreeScape.Engine.GameObjects.Entities;
using FreeScape.Engine.Providers;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Numerics;

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

            var newPosition = new Vector2(movable.Position.X + (movable.Velocity.X * deltaTime), movable.Position.Y + (movable.Velocity.Y * deltaTime));
            var isCollision = false;
            foreach(ICollider collider in Colliders)
            {
                if (CheckCollision(newPosition, movable.Size) != null)
                {
                    isCollision = true;
                }
            }
            if (!isCollision)
            {
                movable.Position = newPosition;
            }
        }

        public ICollider CheckCollision(Vector2 positionToCheck, Vector2 Size)
        {
            foreach (var collider in Colliders)
            {
                if (   positionToCheck.X + Size.X > collider.Position.X
                    && positionToCheck.Y + Size.X > collider.Position.Y 
                    && positionToCheck.X - Size.Y < collider.Position.X + collider.ColliderSize.X 
                    && positionToCheck.Y - Size.Y < collider.Position.Y + collider.ColliderSize.Y)
                {
                    return collider;
                }
            }

            return null;
        }

        public void RegisterCollider(ICollider collider)
        {
            Colliders.Add(collider);
        }
    }

}
