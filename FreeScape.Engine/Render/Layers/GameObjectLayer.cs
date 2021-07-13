using FreeScape.Engine.Config.Map;
using FreeScape.Engine.Config.TileSet;
using FreeScape.Engine.GameObjects;
using FreeScape.Engine.GameObjects.Entities;
using FreeScape.Engine.Physics;
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
            LoadObjectLayer();
        }

        public void LoadObjectLayer()
        {

            var tileSet = _tileSetProvider.GetTileSet(Map.TileSets.First().Source);
            var mapObjectLayer = Map.Layers.Where(x => x.Type == "objectgroup").First();
            var i = 0;
            foreach (var mapObject in mapObjectLayer.Objects)
            {
                CachedTileSetTile mapObjectTile;
                if (tileSet.Tiles.ContainsKey((uint)mapObject.GId))
                {
                    mapObjectTile = tileSet.Tiles[(uint)mapObject.GId - 1];
                }
                else continue;
                MapGameObject gameObject;
                if (mapObjectTile.Properties.Any(x => x.Name == "Collidable" && x.Type == "circle"))
                {
                    var ctile = new CollidableMapGameObject(new Vector2((float)mapObject.x, (float)(mapObject.y - mapObject.Height)), new Vector2((float)mapObject.Width, (float)mapObject.Height), mapObjectTile, tileSet.Sheet);
                    _movement.Colliders.Add(ctile.Collider);
                    gameObject = ctile;
                }
                else if (mapObjectTile.Properties.Any(x => x.Name == "Collidable" && x.Type == "rect"))
                {
                    var ctile = new CollidableMapGameObject(new Vector2((float)mapObject.x, (float)(mapObject.y - mapObject.Height)), new Vector2((float)mapObject.Width, (float)mapObject.Height), mapObjectTile, tileSet.Sheet);
                    _movement.Colliders.Add(ctile.Collider);
                    gameObject = ctile;
                }
                else
                {
                    gameObject = new MapGameObject(new Vector2((float)mapObject.x, (float)(mapObject.y - mapObject.Height)), new Vector2((float)mapObject.Width, (float)mapObject.Height), mapObjectTile, tileSet.Sheet);
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
