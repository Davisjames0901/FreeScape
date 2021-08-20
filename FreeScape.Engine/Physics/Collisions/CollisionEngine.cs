using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeScape.Engine.Core.GameObjects;
using FreeScape.Engine.Core.GameObjects.Entities;

namespace FreeScape.Engine.Physics.Collisions
{
    public class CollisionEngine
    {
        public List<ICollidable> StaticCollidables;
        public List<ICollidable> GameObjectCollidables;

        public CollisionEngine()
        {
            StaticCollidables = new List<ICollidable>();
            GameObjectCollidables = new List<ICollidable>();
        }

        public void RegisterStaticCollidable(ICollidable collidable)
        {
            StaticCollidables.Add(collidable);
        }
        public void RegisterGameObjectCollidable(ICollidable collidable)
        {
            GameObjectCollidables.Add(collidable);
        }

        public void CheckCollisions()
        {
            if (GameObjectCollidables.Count + StaticCollidables.Count > 1)
            {
                foreach (var firstCollidable in GameObjectCollidables)
                {
                    if (firstCollidable is not IGameObject movableCollidable) continue;
                    foreach (var collider in firstCollidable.Colliders)
                    {
                        var firstVertices = collider.Vertices.Select(x => x + movableCollidable.Position).ToArray();

                        for (var i = 1; i < GameObjectCollidables.Count; i++)
                        {
                            var secondCollidable = GameObjectCollidables.ElementAt(i);
                            if (secondCollidable is not IGameObject secondMovableCollidable) continue;
                            foreach (var secondCollider in secondCollidable.Colliders)
                            {
                                var secondVertices = secondCollider.Vertices.Select(x => x + secondMovableCollidable.Position).ToArray();
                                if (GJKCollision.GJKCheckCollision(firstVertices, secondVertices, out var test))
                                {
                                    firstCollidable.CollisionEnter(secondCollidable);
                                    secondCollidable.CollisionEnter(firstCollidable);
                                }
                            }
                        }

                        foreach (var staticCollidable in StaticCollidables)
                        {
                            foreach (var staticCollider in staticCollidable.Colliders)
                            {
                                var secondVertices = staticCollider.GetVerticesRelativeToPosition();
                                if(staticCollidable is Tile mgo)
                                {
                                    
                                }
                                if (GJKCollision.GJKCheckCollision(firstVertices, secondVertices, out var test))
                                {
                                    firstCollidable.CollisionEnter(staticCollidable);
                                    staticCollidable.CollisionEnter(firstCollidable);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
