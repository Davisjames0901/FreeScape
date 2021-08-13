using System;
using FreeScape.Engine.Providers;
using System.Collections.Generic;
using System.Linq;
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
            var deltaTime = (float) _frameTime.DeltaTimeMilliSeconds;
            var distance = Maths.GetDistance(movable.HeadingVector, movable.Speed, deltaTime);
            var newPosition = movable.Position + distance;
            if (movable is ICollidable collidable)
            {
                movable.Position = CheckCollision(movable, collidable.Collider, newPosition);
                return;
            }

            movable.Position = newPosition;
        }

        public Vector2 CheckCollision(IMovable sourceMovable, ICollider collider, Vector2 desiredPosition)
        {
            var collidedX = false;
            var collidedY = false;
            var source = collider.Vertices;
            var desiredPositionVerts = source.Select(x => x + desiredPosition).ToArray();
            foreach (var target in Colliders)
            {
                if (collidedX && collidedY)
                    return sourceMovable.Position;
                
                var targetShape = target.GetVerticesRelativeToPosition();

                if (!collidedX)
                {
                    var onlyX = new Vector2(desiredPosition.X, sourceMovable.Position.Y);
                    desiredPositionVerts = source.Select(x => x + onlyX).ToArray();
                    collidedX = GJKCollision.GJKCheckCollision(desiredPositionVerts, targetShape, out _);
                }

                if (!collidedY)
                {
                    var onlyY = new Vector2(sourceMovable.Position.X, desiredPosition.Y);
                    desiredPositionVerts = source.Select(x => x + onlyY).ToArray();
                    collidedY = GJKCollision.GJKCheckCollision(desiredPositionVerts, targetShape, out _);
                }
            }

            if (collidedX)
            {
                return new Vector2(sourceMovable.Position.X, desiredPosition.Y);
            }

            if (collidedY)
            {
                return new Vector2(desiredPosition.X, sourceMovable.Position.Y);
            }
            
            return desiredPosition;
        }

        public void RegisterCollider(ICollider collider)
        {
            Colliders.Add(collider);
        }
    }
}