using System.Collections.Generic;
using System.Numerics;
using FreeScape.Engine.Physics.Collisions;
using FreeScape.Engine.Physics.Collisions.Colliders;
using FreeScape.Engine.Render.Tiled;
using SFML.Graphics;

namespace FreeScape.Engine.Core.GameObjects.Entities
{
    public class MapGameObject : IGameObject, ICollidable
    {
        public readonly TiledTile _tileInfo;

        public Vector2 Scale { get; set; } = Vector2.One;
        public Vector2 Size { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Sprite Sprite { get; }
        public List<ICollider> Colliders { get; set; }
        public MapGameObject(Vector2 position, Vector2 size, Vector2 scale, float rotation, TiledTile tileInfo, Sprite sprite)
        {
            _tileInfo = tileInfo;
            Size = size;
            Scale = scale;
            Position = position;
            Rotation = rotation;
            Sprite = sprite;
            Sprite.Position = new Vector2(Position.X, Position.Y);
            Colliders = new List<ICollider>();
        }

        public void CollisionEnter(ICollidable collidable)
        {
            //if(collidable is IMovable player)
            //Console.WriteLine("object test " + player.Position);
        }
        
        public void Render(RenderTarget target)
        {
            var scale = Size / (new Vector2(Sprite.TextureRect.Width, Sprite.TextureRect.Height));
            Sprite.Scale = scale;
            Sprite.Rotation = Rotation;
            target.Draw(Sprite);
        }

        public void Tick() { }
        public void Init() { }
    }
}