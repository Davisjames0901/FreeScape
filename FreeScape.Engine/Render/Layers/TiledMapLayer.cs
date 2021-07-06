using FreeScape.Engine.Config.Map;
using FreeScape.Engine.Providers;
using SFML.Graphics;
using SFML.System;

namespace FreeScape.Engine.Render.Layers
{
    public abstract class TiledMapLayer : ILayer
    {
        private readonly TextureProvider _textureProvider;
        public abstract MapInfo Map { get; }
        public abstract int ZIndex { get; }

        public TiledMapLayer(TextureProvider textureProvider)
        {
            _textureProvider = textureProvider;
        }

        public virtual void Render(RenderTarget target)
        {
            foreach(var tile in Map.Tiles)
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

        public abstract void Tick();
    }
}