using FreeScape.Engine.Config.Map;
using FreeScape.Engine.Config.TileSet;
using FreeScape.Engine.GameObjects;
using FreeScape.Engine.GameObjects.Entities;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Physics.Colliders;
using FreeScape.Engine.Providers;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace FreeScape.Engine.Render.Layers
{
    public abstract class GameObjectLayer : ILayer
    {

        private readonly TileSetProvider _tileSetProvider;
        public abstract MapInfo Map { get; }
        public abstract int ZIndex { get; }

        public List<IGameObject> GameObjects;

        private Movement _movement;

        public GameObjectLayer(Movement movement, TileSetProvider tileSetProvider)
        {
            _tileSetProvider = tileSetProvider;
            _movement = movement;
            GameObjects = new List<IGameObject>();
        }

        public virtual void Init()
        {
            //LoadObjectLayer();
        }

        public void LoadObjectLayer()
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
                GameObjects.Add(gameObject);
                i++;
            }
        }

        public void Render(RenderTarget target)
        {
            foreach (var gameObject in GameObjects)
            {
                gameObject.Render(target);
            }
        }

        public virtual void Tick()
        {
            foreach(IGameObject gameObject in GameObjects)
            {
                if(gameObject is IMovable movable)
                {
                    _movement.BasicMove(movable);
                }
                gameObject.Tick();
            }
        }

    }
}
