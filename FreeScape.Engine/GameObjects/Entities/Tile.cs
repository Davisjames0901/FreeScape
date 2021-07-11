using System.Numerics;
using FreeScape.Engine.Config.TileSet;
using SFML.Graphics;

namespace FreeScape.Engine.GameObjects.Entities
{
    public class Tile : IGameObject, ICollider
    {
        private readonly CachedTileSetTile _tileInfo;

        public Vector2 Size { get; set; }
        public bool Collidable { get; set; }
        public Vector2 Position { get; set; }

        public Vector2 ColliderSize { get; set; }

        public Sprite Sprite;
        public Tile(Vector2 position, Vector2 size, CachedTileSetTile tileInfo, Texture texture)
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
            target.Draw(Sprite);
            // var rect = new RectangleShape(new Vector2(Size, Size));
            // rect.Position = Sprite.Position;
            // rect.FillColor = Color.Transparent;
            // rect.OutlineThickness = 2;
            // rect.OutlineColor = Color.Yellow;
            // target.Draw(rect);
        }

        public void Tick()
        {
        }
        public void Init()
        {

        }
    }
}