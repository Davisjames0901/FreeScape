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
            var tileSet = Map.TileSets.First();//
            _tileSetProvider.GetTileSet(Map.TileSets.First().Image);

            //CachedTileSet cachedTileSet = new CachedTileSet();
            var mapObjectLayer = Map.Layers.Where(x => x.Type == "objectgroup").First();
            var i = 0;
            foreach (var mapObject in mapObjectLayer.Objects)
            {
                TileSetTile mapObjectTile = tileSet.Tiles.ElementAt(mapObject.GId - 1);
                if (mapObjectTile == null)
                {
                    continue;
                }
                MapGameObject gameObject = null;
                if (mapObjectTile.Properties.Any(x => x.Name == "HasCollider" && x.Value))
                {

                    var tileObjectGroup = mapObjectTile.ObjectGroup;
                    var tileObjects = tileObjectGroup.Objects;
                    foreach(var tileObject in tileObjects)
                    {
                        switch (tileObject.Type)
                        {
                            case "rectangle":
                                //var ctile = new CollidableMapGameObject(
                                //            new Vector2((float)mapObject.x, (float)(mapObject.y - mapObject.Height)),
                                //            new Vector2((float)mapObject.Width, (float)mapObject.Height), 
                                //            mapObjectTile, 
                                //            tileSet.Sheet);
                                //_movement.Colliders.Add(ctile.Collider);
                                break;
                            case "circle":

                                break;
                            default:
                                break;
                        }
                    }

                    //
                    //_movement.Colliders.Add(ctile.Collider);
                    //gameObject = ctile;
                }
                Tiles.Add(gameObject);
                i++;
            }
        }

        private void RenderTile(RenderTarget target, Tile tile)
        {
            tile.Render(target);
        }

        public abstract void Tick();
    }
}