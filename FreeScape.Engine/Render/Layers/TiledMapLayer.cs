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
using FreeScape.Engine.Config.TileSet;
using FreeScape.Engine.Physics.Collisions.Colliders;
using FreeScape.Engine.Physics.Collisions;
using System;

namespace FreeScape.Engine.Render.Layers
{
    public abstract class TiledMapLayer : ILayer
    {
        private readonly TextureProvider _textureProvider;
        private readonly MapProvider _mapProvider;
        private readonly CollisionEngine _collisionEngine;
        private Movement _movement;
        private List<RectangleShape> _colliderDebugShapes;
        public List<Tile> Tiles;
        public Sprite TiledMapSprite;
        public abstract MapInfo Map { get; }
        public abstract int ZIndex { get; }
        public RenderMode RenderMode => RenderMode.World;


        public TiledMapLayer(TextureProvider textureProvider, MapProvider mapProvider, Movement movement, CollisionEngine collisionEngine)
        {
            _movement = movement;
            _textureProvider = textureProvider;
            _mapProvider = mapProvider;
            _collisionEngine = collisionEngine;
            _colliderDebugShapes = new List<RectangleShape>();
            Tiles = new List<Tile>();
        }

        public virtual void Render(RenderTarget target)
        {
            if(TiledMapSprite is not null)
            {
                target.Draw(TiledMapSprite);
            }
            else
                foreach (var tile in Tiles.OrderBy(x => x.Position.Y))
                {
                    RenderTile(target, tile);
                }
        }

        public void Init()
        {
            // foreach (var i in tileSet.Tiles)
            // { 
            //     var tile = new Tile(pos, tileSize, i.Value, tileSet.Sheet);
            //     Tiles.Add(tile);
            //     pos += increment;
            // }
            LoadTileLayer();
        }
        private void LoadTileLayer()
        {
            var mapTileLayer = Map.Layers.First(x => x.Type == "tilelayer");
            foreach (var chunk in Map.Layers.Where(x => x.Type == "tilelayer").SelectMany(x => x.Chunks))
            {
                var i = 0;
                foreach(var num in chunk.Data) 
                {
                    var tileSetTile = _mapProvider.GetTile(num);
                    if (tileSetTile == null)
                        continue;
                    
                    var tilePosition = new Vector2((float)(chunk.X + i % chunk.Width) * Map.TileWidth, (float)(chunk.Y + i / chunk.Height) * Map.TileHeight);
                    var tileSize = new Vector2(Map.TileWidth, Map.TileHeight);
                    var sprite = _textureProvider.GetSprite($"tiled:{tileSetTile.Id}");
                    var tile = new Tile(tilePosition, tileSize, sprite);

                    if (tileSetTile.Properties.Any(x => x.Name == "HasCollider" && x.Value))
                    {
                        foreach (var tileObject in tileSetTile.ObjectGroup.Objects)
                        {
                            switch (tileObject.Type)
                            {
                                case "rectangle":
                                    var rectCollider = new RectangleCollider(new Vector2(tileObject.Width, tileObject.Height), tilePosition + new Vector2(tileObject.X, tileObject.Y));
                                    rectCollider.ColliderType = ColliderType.Solid;
                                    tile.Colliders.Add(rectCollider);
                                    break;
                                case "circle":

                                    break;
                            }
                        }
                        if(tileSetTile.ObjectGroup.Objects.Count > 0)
                            _collisionEngine.RegisterStaticCollidable(tile);
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