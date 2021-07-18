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

        public Vector2 Scale { get; set; } = Vector2.One;
        public Vector2 Size { get; set; }
        public bool Collidable { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 ColliderSize { get; set; }
        private bool _usesSheet { get; set; }
        public Sprite Sprite;
        public MapGameObject(Vector2 position, Vector2 size, Vector2 scale, float rotation, CachedTileSetTile tileInfo, Texture texture)
        {
            _tileInfo = tileInfo;
            _usesSheet = tileInfo.UsesSheet;
            Size = size;
            Scale = scale;
            Position = position;
            Rotation = rotation;
            ColliderSize = size;
            var tile = new Sprite(texture);
            tile.TextureRect = tileInfo.TextureLocation;
            tile.Texture = texture;
            tile.Position = new Vector2(Position.X, Position.Y);
            Sprite = tile;
        }
        public MapGameObject(Vector2 position, Vector2 size, Vector2 scale, float rotation, CachedTileSetTile tileInfo)
        {
            _tileInfo = tileInfo;
            _usesSheet = tileInfo.UsesSheet;
            Size = size;
            Scale = scale;
            Position = position;
            Rotation = rotation;
            ColliderSize = size;
            Sprite = new Sprite();
            Sprite.Texture = tileInfo.ImageTexture;
            Sprite.Position = position;
            Sprite.Origin = new Vector2(0, 0);
        }
        public void Render(RenderTarget target)
        {
            var scale = Size / (new Vector2(Sprite.TextureRect.Width, Sprite.TextureRect.Height));
            Sprite.Scale = scale;
            Sprite.Rotation = Rotation;
            target.Draw(Sprite);
        }

        public void Tick()
        {
        }
        public void Init()
        {

        }
    }
}