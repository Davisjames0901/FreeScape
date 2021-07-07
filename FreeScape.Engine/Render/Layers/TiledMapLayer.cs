using FreeScape.Engine.Config.Map;
using FreeScape.Engine.GameObjects;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Providers;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace FreeScape.Engine.Render.Layers
{
    public abstract class TiledMapLayer : ILayer
    {
        private readonly TextureProvider _textureProvider;
        private Movement _movement;
        public abstract MapInfo Map { get; }
        public abstract int ZIndex { get; }
        public List<Tile> Tiles;
        

        public TiledMapLayer(TextureProvider textureProvider, Movement movement)
        {
            _movement = movement;
            _textureProvider = textureProvider;
            Tiles = new List<Tile>();
        }

        public virtual void Render(RenderTarget target)
        {
            foreach(var tile in Tiles)
            {
                RenderTile(target, tile);
            }
        }
        public void Init()
        {
            foreach (var tileInfo in Map.Tiles)
            {
                var texture = _textureProvider.GetTexture(tileInfo.Texture);
                Tile tile = new Tile(new Vector2f(tileInfo.X, tileInfo.Y), tileInfo.Collidable,tileInfo.Size, texture);
                Tiles.Add(tile);
                if(tile.Collidable)
                _movement.RegisterCollider(tile);
            }
        }
        private void RenderTile(RenderTarget target, Tile tile)
        {
            tile.Render(target);
        }

        public abstract void Tick();


    }
}