using System.Collections.Generic;
using System.Numerics;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Physics.Collisions.Colliders;
using SFML.Graphics;

namespace FreeScape.Engine.GameObjects.Entities
{
    public class Tile : IGameObject, ICollidable
    {
        public Vector2 Scale { get; set; } = Vector2.One;
        public Vector2 Size { get; set; }
        public Vector2 Position { get; set; }

        public Sprite Sprite { get; }

        public Tile(Vector2 position, Vector2 size, Sprite sprite)
        {
            Size = size;
            Position = position;
            Sprite = sprite;
            Sprite.Position = new Vector2(Position.X, Position.Y);
            Colliders = new List<ICollider>();
        }

        public List<ICollider> Colliders { get; set; }


        public void CollisionEnter(ICollidable collidable)
        {
            //Console.WriteLine("Entered Tile");
        }
        public void Render(RenderTarget target)
        {
            target.Draw(Sprite);
        }

        public void Tick() { }
        public void Init() { }
    }
}