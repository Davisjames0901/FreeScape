using FreeScape.Engine.Physics;
using FreeScape.Engine.Physics.Colliders;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FreeScape.Engine.Providers
{
    public class ColliderProvider : CollidableProvidable
    {

        public ConcurrentBag<ICollidable> Collidables;

        public ColliderProvider()
        {
            Collidables = new ConcurrentBag<ICollidable>();
        }
        public CircleCollider CreateCircleCollider(Vector2 position, Vector2 center, float radius)
        {
            CircleCollider collider = new CircleCollider(position, center, radius);

            return collider;
        }
        public RectangleCollider CreateRectangleCollider(Vector2 size, Vector2 position)
        {
            RectangleCollider collider = new RectangleCollider(size, position);

            return collider;
        }
        public void RegisterCollidable(ICollidable collidable)
        {
            Collidables.Add(collidable);
        }

        public void CheckCollision()
        {
            var j = 1;
            if (Collidables.Count > 1)
            foreach (var firstCollidable in Collidables)
            {
                Console.WriteLine(firstCollidable.Collider.Position);
                var firstCollider = firstCollidable.Collider;
                for (int i = j; i < Collidables.Count - 1; i++)
                {
                    var secondCollidable = Collidables.ElementAt(i);
                    var secondCollider = secondCollidable.Collider;
                    var firstVertices = firstCollider.GetVerticesRelativeToPosition();
                    var secondVertices = secondCollider.GetVerticesRelativeToPosition();

                    if(GJKCollision.GJKCheckCollision(firstVertices, secondVertices, out _))
                    {
                        ////Trigger callback to collidables
                        firstCollidable.CollisionEnter(secondCollidable);
                        secondCollidable.CollisionEnter(firstCollidable);
                    }
                }
                j++;
            }
        }

    }
}
