﻿using FreeScape.Engine.GameObjects.Entities;
using FreeScape.Engine.Providers;
using SFML.System;
using System;
using System.Collections.Generic;

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

            var newPosition = new Vector2f(movable.Position.X + (movable.Velocity.X * deltaTime), movable.Position.Y + (movable.Velocity.Y * deltaTime));
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

        public ICollider CheckCollision(Vector2f positionToCheck, float Size)
        {
            foreach (var collider in Colliders)
            {
                if (   positionToCheck.X + Size > collider.Position.X
                    && positionToCheck.Y + Size > collider.Position.Y 
                    && positionToCheck.X - Size < collider.Position.X + collider.ColliderSize.X 
                    && positionToCheck.Y - Size < collider.Position.Y + collider.ColliderSize.Y)
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
