using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeScape.Engine.Physics.Collisions
{
    public class CollisionEngine
    {
        public ConcurrentBag<ICollidable> Collidables;

        public CollisionEngine()
        {
            Collidables = new ConcurrentBag<ICollidable>();
        }

        public void RegisterCollidable(ICollidable collidable)
        {
            Collidables.Add(collidable);
        }

        public void CheckCollisions()
        {
            var j = 1;
            if (Collidables.Count > 1)
                foreach (var firstCollidable in Collidables)
                {
                    //Console.WriteLine(firstCollidable.Collider.Position);
                    var firstCollider = firstCollidable.Collider;
                    for (int i = j; i < Collidables.Count - 1; i++)
                    {
                        var secondCollidable = Collidables.ElementAt(i);
                        var secondCollider = secondCollidable.Collider;
                        var firstVertices = firstCollider.GetVerticesRelativeToPosition();
                        var secondVertices = secondCollider.GetVerticesRelativeToPosition();

                        if (GJKCollision.GJKCheckCollision(firstVertices, secondVertices, out _))
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
