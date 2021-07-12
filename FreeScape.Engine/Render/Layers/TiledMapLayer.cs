using FreeScape.Engine.Config.Map;
using FreeScape.Engine.GameObjects;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Providers;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using FreeScape.Engine.GameObjects.Entities;

namespace FreeScape.Engine.Render.Layers
{
    public abstract class TiledMapLayer : ILayer
    {
        private readonly TileSetProvider _tileSetProvider;
        private Movement _movement;
        public abstract MapInfo Map { get; }
        public abstract int ZIndex { get; }
        public List<Tile> Tiles;


        public TiledMapLayer(TileSetProvider tileSetProvider, Movement movement)
        {
            _movement = movement;
            _tileSetProvider = tileSetProvider;
            Tiles = new List<Tile>();
        }

        public virtual void Render(RenderTarget target)
        {
            foreach (var tile in Tiles)
            {
                RenderTile(target, tile);
            }
        }

        public void Init()
        {
            var tileSize = new Vector2(Map.TileWidth, Map.TileHeight);
            var tileSet = _tileSetProvider.GetTileSet(Map.TileSets.First().Source);
            // foreach (var i in tileSet.Tiles)
            // { 
            //     var tile = new Tile(pos, tileSize, i.Value, tileSet.Sheet);
            //     Tiles.Add(tile);
            //     pos += increment;
            // }
            foreach (var chunk in Map.Layers.Where(x => x.Type == "tilelayer").SelectMany(x => x.Chunks))
            {
                var i = 0;
                foreach (var num in chunk.Data)
                {
                    var texture = tileSet.Tiles[num-1];
                    Tile tile;
                    if (texture.Properties.Any(x => x.Name == "Collidable" && x.Type != "none"))
                    {
                        var ctile = new CollidableTile(new Vector2((chunk.X + i%chunk.Width)*Map.TileWidth, (chunk.Y + i/chunk.Height)*Map.TileHeight), tileSize, texture, tileSet.Sheet);
                        _movement.Colliders.Add(ctile.Collider);
                        tile = ctile;
                    }
                    else
                    {
                        tile = new Tile(new Vector2((chunk.X + i%chunk.Width)*Map.TileWidth, (chunk.Y + i/chunk.Height)*Map.TileHeight), tileSize, texture, tileSet.Sheet);
                    }
                    Tiles.Add(tile);
                    i++;
                }
            }
        }

        private void RenderTile(RenderTarget target, Tile tile)
        {
            tile.Render(target);
        }

        public abstract void Tick();
    }
}