using System;
using System.Numerics;
using FreeScape.Engine.Config.TileSet;
using FreeScape.Engine.Physics;
using SFML.Graphics;

namespace FreeScape.Engine.GameObjects.Entities
{
    public class MapGameObject : IGameObject
    {
        private readonly CachedTileSetTile _tileInfo;

        public Vector2 Size { get; set; }
        public bool Collidable { get; set; }
        public Vector2 Position { get; set; }

        public Vector2 ColliderSize { get; set; }

        public Sprite Sprite;
        public MapGameObject(Vector2 position, Vector2 size, TileSetTile tileInfo, Texture texture)
        {
            _tileInfo = tileInfo;
            Size = size;
            Position = position;
            ColliderSize = size;
            var tile = new Sprite(texture);
            tile.TextureRect = tileInfo.TextureLocation;

            tile.Texture = texture;
            tile.Position = new Vector2(Position.X, Position.Y);
            Sprite = tile;
        }
        public void Render(RenderTarget target)
        {
            var scale = Size / (new Vector2(Sprite.TextureRect.Width, Sprite.TextureRect.Height));
            Sprite.Scale = scale;
            target.Draw(Sprite);
            if (this is CollidableMapGameObject)
            {
                var circ = new CircleShape(Size.X/2);
                circ.Position = Sprite.Position;
                circ.FillColor = Color.Transparent;
                circ.OutlineThickness = 2;
                circ.OutlineColor = Color.Yellow;
                target.Draw(circ);
            }
        }

        public void Tick()
        {
        }
        public void Init()
        {

        }
    }
}