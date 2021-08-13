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
        private readonly TileSetProvider _tileSetProvider;
        private readonly MapProvider _mapProvider;
        private readonly CollisionEngine _collisionEngine;
        private Movement _movement;
        private List<RectangleShape> _colliderDebugShapes;
        public List<Tile> Tiles;
        public Sprite TiledMapSprite;
        public abstract MapInfo Map { get; }
        public abstract int ZIndex { get; }
        public RenderMode RenderMode => RenderMode.World;


        public TiledMapLayer(TileSetProvider tileSetProvider, MapProvider mapProvider, Movement movement, CollisionEngine collisionEngine)
        {
            _movement = movement;
            _tileSetProvider = tileSetProvider;
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
            //var tileSet = Map.TileSets.First();//
            //_tileSetProvider.GetTileSet(Map.TileSets.First().Image);

            //CachedTileSet cachedTileSet = new CachedTileSet();
            var mapTileLayer = Map.Layers.Where(x => x.Type == "tilelayer").First();
            foreach (var chunk in Map.Layers.Where(x => x.Type == "tilelayer").SelectMany(x => x.Chunks))
            {
                var i = 0;
                foreach(var num in chunk.Data) 
                {
                    CachedTileSet tileSet = _mapProvider.GetTileSetBy((int)num);
                    CachedTileSetTile tileSetTile = _mapProvider.GetTileSetTile(tileSet, (int)num - tileSet.FirstGid);
                    if (tileSetTile == null)
                    {
                        continue;
                    }
                    Tile tile = null;

                    var tilePosition = new Vector2((float)(chunk.X + i % chunk.Width) * Map.TileWidth, (float)(chunk.Y + i / chunk.Height) * Map.TileHeight);
                    var tileSize = new Vector2((float)tileSet.TileWidth, (float)tileSet.TileHeight);

                    tile = new Tile(tilePosition,
                                        tileSize,
                                        tileSetTile,
                                        tileSet.Sheet);

                    if (tileSetTile.Properties.Any(x => x.Name == "HasCollider" && x.Value))
                    {
                        foreach (var tileObject in tileSetTile.ObjectGroup.Objects)
                        {
                            switch (tileObject.Type)
                            {
                                case "rectangle":
                                    RectangleCollider rectCollider = new RectangleCollider(new Vector2(tileObject.Width, tileObject.Height), tilePosition + new Vector2(tileObject.X, tileObject.Y));

                                    rectCollider.ColliderType = ColliderType.Solid;
                                    tile.Colliders.Add(rectCollider);


                                    break;
                                case "circle":

                                    break;
                                default:
                                    break;
                            }
                        }
                        if(tileSetTile.ObjectGroup.Objects.Count > 0)
                            _collisionEngine.RegisterStaticCollidable(tile);

                        //
                        //_movement.Colliders.Add(ctile.Collider);
                        //gameObject = ctile;
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