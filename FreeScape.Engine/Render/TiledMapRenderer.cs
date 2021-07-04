using FreeScape.Engine.GameObjects;
using FreeScape.Engine.Providers;
using SFML.Graphics;
using SFML.System;

namespace FreeScape.Engine.Render
{
    public class TiledMapRenderer
    {
        private readonly TextureProvider _textureProvider;
        private readonly int _size;
        public TiledMapRenderer(TextureProvider textureProvider)
        {
            _textureProvider = textureProvider;
        }

        public void RenderTileMap(RenderTarget target, MapInfo info)
        {
            foreach(var tile in info.Tiles)
            {
                RenderTile(target, tile);
            }
        }

        private void RenderTile(RenderTarget target, TileInfo info)
        {
            var tile = new RectangleShape(new Vector2f(info.Size, info.Size));
            tile.Position = new Vector2f(info.X, info.Y);
            var texture = _textureProvider.GetTexture(info.Texture);
            if (texture != null)
                tile.Texture = texture;
            
            else
            {
                tile.FillColor = Color.Yellow;
                tile.OutlineColor = Color.Black;
                tile.OutlineThickness = 1;
            }
            target.Draw(tile);
        }
    }
}