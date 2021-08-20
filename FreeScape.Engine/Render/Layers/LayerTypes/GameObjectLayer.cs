using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using FreeScape.Engine.Core.GameObjects;
using FreeScape.Engine.Core.GameObjects.Entities;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Physics.Collisions;
using FreeScape.Engine.Physics.Collisions.Colliders;
using FreeScape.Engine.Physics.Movement;
using FreeScape.Engine.Render.Textures;
using FreeScape.Engine.Render.Tiled;
using SFML.Graphics;

namespace FreeScape.Engine.Render.Layers.LayerTypes
{
    public abstract class GameObjectLayer : ILayer
    {

        private readonly MapProvider _mapProvider;
        private readonly Movement _movement;
        private readonly CollisionEngine _collisionEngine;
        private readonly TextureProvider _textureProvider;

        protected readonly List<IGameObject> GameObjects;

        public RenderMode RenderMode => RenderMode.World;
        
        protected abstract TiledMap Map { get; }
        public abstract int ZIndex { get; }


        public GameObjectLayer(Movement movement, MapProvider mapProvider, CollisionEngine collisionEngine, TextureProvider textureProvider)
        {
            _collisionEngine = collisionEngine;
            _textureProvider = textureProvider;
            _mapProvider = mapProvider;
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
                var tileSetTile = _mapProvider.GetTile((uint)mapGameObject.Gid);
                if (tileSetTile == null)
                {
                    continue;
                }
                
                var objectPosition = new Vector2((float)mapGameObject.X, (float)mapGameObject.Y - (float)mapGameObject.Height);
                var objectSize = new Vector2((float)mapGameObject.Width, (float)mapGameObject.Height);
                var objectRotation = 0; // (float)mapGameObject.Rotation;
                var scale = objectSize / (new Vector2(Map.TileWidth, Map.TileHeight));
                var sprite = _textureProvider.GetSprite($"tiled:{mapGameObject.Gid}");
                var gameObject = new MapGameObject(objectPosition, objectSize, scale, objectRotation, tileSetTile, sprite);

                if (tileSetTile.Properties != null && tileSetTile.Properties.Any(x => x.Name == "HasCollider" && x.Value))
                {
                    foreach (var tileObject in tileSetTile.ObjectGroup.Objects)
                    {
                        switch (tileObject.Type)
                        {
                            case "rectangle":
                                RectangleCollider rectCollider = new RectangleCollider((new Vector2(tileObject.Width, tileObject.Height) * scale), objectPosition + (new Vector2(tileObject.X, tileObject.Y) * scale));

                                rectCollider.ColliderType = ColliderType.Solid;
                                gameObject.Colliders.Add(rectCollider);

                                break;
                            case "circle":

                                break;
                            default:
                                break;
                        }
                    }
                    if(tileSetTile.ObjectGroup.Objects.Count > 0)
                        _collisionEngine.RegisterStaticCollidable(gameObject);

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
        }

        public virtual void Tick()
        {
            foreach(IGameObject gameObject in GameObjects)
            {
                gameObject.Tick();
                if(gameObject is IMovable movable)
                {
                    _movement.BasicMove(movable);
                }
            }
        }

    }
}
