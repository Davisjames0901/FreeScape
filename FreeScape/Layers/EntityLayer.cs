using FreeScape.Engine.Core;
using FreeScape.Engine.Core.GameObjects.UI;
using FreeScape.Engine.Core.Managers;
using FreeScape.Engine.Input;
using FreeScape.Engine.Physics;
using FreeScape.Engine.Render.Layers;
using FreeScape.GameObjects;
using FreeScape.Engine.Physics.Collisions;
using FreeScape.Engine.Physics.Movement;
using FreeScape.Engine.Render.Layers.LayerTypes;
using FreeScape.Engine.Render.Textures;
using FreeScape.Engine.Render.Tiled;
using FreeScape.GameObjects.Player;

namespace FreeScape.Layers
{
    public class EntityLayer : GameObjectLayer
    {
        private readonly GameObjectProvider _gameObjectProvider;
        private readonly DisplayManager _displayManager;
        private readonly ActionProvider _actionProvider;
        private readonly MapProvider _mapProvider;

        public override int ZIndex => 999;
        protected override TiledMap Map => _mapProvider.GetMap("TiledTestMap");

        public EntityLayer(CollisionEngine collisionEngine, ActionProvider actionProvider, GameObjectProvider gameObjectProvider, 
            DisplayManager displayManager, Movement movement, MapProvider mapProvider, TextureProvider textureProvider):base(movement, mapProvider, collisionEngine, textureProvider)
        {
            _actionProvider = actionProvider;
            _gameObjectProvider = gameObjectProvider;
            _displayManager = displayManager;
            _mapProvider = mapProvider;


            actionProvider.SubscribeOnPressed(a =>
            {
                if (a == "LeftClick")
                    MouseClick();
            });
        }
        public override void Init()
        {
            var player = _gameObjectProvider.Provide<Player>();
            GameObjects.Add(player);

            
            foreach(var gameObject in GameObjects)
            {
                gameObject.Init();
            }

            _displayManager.Track(x => x.Name == "main", player);
            base.Init();
        }


        public void MouseClick()
        {
            var mouseCoords = _actionProvider.GetMouseWorldCoods();
            foreach (var gameObject in GameObjects)
            {
                if(gameObject is IUIObject uIObject)
                if (uIObject.Hovered && uIObject is IButton button)
                {
                    button.OnClick();
                    break;
                }
            }
        }
    }
}
