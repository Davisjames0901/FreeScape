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

        private readonly MapProvider _mapProvider;
        public abstract MapInfo Map { get; }
        public abstract int ZIndex { get; }

        public List<IGameObject> GameObjects;
        public List<RectangleShape> _colliderDebugShapes;

        private Movement _movement;

        public GameObjectLayer(Movement movement, MapProvider mapProvider)
        {
            _mapProvider = mapProvider;
            _colliderDebugShapes = new();
            _movement = movement;
            GameObjects = new List<IGameObject>();
        }

        public virtual void Init()
        {
            LoadObjectLayer();
        }

        public void LoadObjectLayer()
        {
            foreach (var mapGameObject in Map.Layers.Where(x => x.Type == "objectgroup" && x.Name == "TerrainObjects").First().Objects)
            {
                CachedTileSet tileSet = _mapProvider.GetTileSetBy(mapGameObject.GId);
                CachedTileSetTile tileSetTile = _mapProvider.GetTileSetTile(tileSet, mapGameObject.GId - tileSet.FirstGid);
                if (tileSetTile == null)
                {
                    continue;
                }
                MapGameObject gameObject = null;

                var objectPosition = new Vector2((float)mapGameObject.x, (float)mapGameObject.y - (float)mapGameObject.Height);
                var objectSize = new Vector2((float)mapGameObject.Width, (float)mapGameObject.Height);
                var objectRotation = 0; // (float)mapGameObject.Rotation;
                var scale = objectSize / (new Vector2(tileSet.TileWidth, tileSet.TileHeight));
                if (tileSetTile.Properties != null && tileSetTile.Properties.Any(x => x.Name == "HasCollider" && x.Value))
                {
                    foreach (var tileObject in tileSetTile.ObjectGroup.Objects)
                    {
                        
                        switch (tileObject.Type)
                        {
                            case "tile":
                                RectangleCollider tileCollider = new RectangleCollider((new Vector2(tileObject.Width, tileObject.Height) * scale), objectPosition + (new Vector2(tileObject.X, tileObject.Y) * scale));
                                _movement.Colliders.Add(tileCollider);
                                //var tileColliderShape = new RectangleShape();
                                //tileColliderShape.Position = tileCollider.Position;
                                //tileColliderShape.Size = tileCollider.Size;
                                //tileColliderShape.FillColor = Color.Transparent;
                                //tileColliderShape.OutlineColor = Color.Red;
                                //tileColliderShape.OutlineThickness = 1;
                                ////tileColliderShape.Rotation = objectRotation;
                                //_colliderDebugShapes.Add(tileColliderShape);

                                break;
                            case "rectangle":
                                RectangleCollider rectCollider = new RectangleCollider((new Vector2(tileObject.Width, tileObject.Height) * scale), objectPosition + (new Vector2(tileObject.X, tileObject.Y) * scale));
                                _movement.Colliders.Add(rectCollider);
                                //var rShape = new RectangleShape();

                                //rShape.Position = rectCollider.Position;
                                //rShape.Size = rectCollider.Size;
                                //rShape.FillColor = Color.Transparent;
                                //rShape.OutlineColor = Color.Red;
                                //rShape.OutlineThickness = 1;
                                //_colliderDebugShapes.Add(rShape);

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
                if (tileSetTile.UsesSheet)
                {
                    gameObject = new MapGameObject(objectPosition, objectSize, scale, objectRotation, tileSetTile, tileSet.Sheet);
                }
                else if (!tileSetTile.UsesSheet)
                {
                    gameObject = new MapGameObject(objectPosition, objectSize, scale, objectRotation, tileSetTile);
                }
                GameObjects.Add(gameObject);
            }
        }

        public void Render(RenderTarget target)
        {
            foreach (var gameObject in GameObjects.OrderBy(x => x.Position.Y + x.Size.Y))
            {
                gameObject.Render(target);
            }
            //foreach (var rshape in _colliderDebugShapes)
            //{
            //    target.Draw(rshape);
            //}
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
