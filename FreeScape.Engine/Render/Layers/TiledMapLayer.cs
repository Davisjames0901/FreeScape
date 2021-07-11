using FreeScape.Engine.Config.Map;
using FreeScape.Engine.GameObjects;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Providers;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

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
            var pos = new Vector2(0, 0);
            var increment = new Vector2(0, 32);
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
                    var tile = new Tile(new Vector2((chunk.X + i%chunk.Width)*Map.TileWidth, (chunk.Y + i/chunk.Height)*Map.TileHeight), tileSize, texture, tileSet.Sheet);
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