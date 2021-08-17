using FreeScape.Engine.Providers;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using FreeScape.Engine.Physics.Collisions.Colliders;
using FreeScape.Engine.Utilities;

namespace FreeScape.Engine.Physics
{
    public class Movement
    {
        private readonly FrameTimeProvider _frameTime;
        private readonly List<ICollider> _colliders;

        public Movement(FrameTimeProvider frameTime)
        {
            _frameTime = frameTime;
            _colliders = new List<ICollider>();
        }

        public void BasicMove(IMovable movable)
        {
            if (movable.HeadingVector.Vector == Vector2.Zero)
                return;
            var deltaTime = (float) _frameTime.DeltaTimeMilliSeconds;
            var distance = Maths.GetDistance(movable.HeadingVector.Vector, movable.Speed, deltaTime);
            movable.Position += distance;
        }

        public Vector2 CheckCollision(IMovable sourceMovable, ICollider collider, Vector2 desiredPosition)
        {
            var collidedX = false;
            var collidedY = false;
            var source = collider.Vertices;
            foreach (var target in _colliders)
            {
                if (collidedX && collidedY)
                    return sourceMovable.Position;
                
                var targetShape = target.GetVerticesRelativeToPosition();

                if (!collidedX)
                {
                    var onlyX = new Vector2(desiredPosition.X, sourceMovable.Position.Y);
                    var desiredPositionVerts = source.Select(x => x + onlyX).ToArray();
                    collidedX = GJKCollision.GJKCheckCollision(desiredPositionVerts, targetShape, out _);
                }

                if (!collidedY)
                {
                    var onlyY = new Vector2(sourceMovable.Position.X, desiredPosition.Y);
                    var desiredPositionVerts = source.Select(x => x + onlyY).ToArray();
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
            _colliders.Add(collider);
        }
    }
}