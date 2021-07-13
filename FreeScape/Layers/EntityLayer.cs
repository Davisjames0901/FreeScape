﻿using FreeScape.Engine.Physics;
using FreeScape.Engine.Render.Layers;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Managers;
using FreeScape.Engine.GameObjects.UI;
using System.Numerics;
using FreeScape.Scenes;
using FreeScape.Engine.GameObjects;
using FreeScape.GameObjects;
using FreeScape.Engine.Config.Map;

namespace FreeScape.Layers
{
    public class EntityLayer : GameObjectLayer
    {

        private readonly GameObjectProvider _gameObjectProvider;
        private readonly DisplayManager _displayManager;
        private readonly SceneManager _sceneManager;
        private readonly UIObjectProvider _uIObjectProvider;
        private readonly ActionProvider _actionProvider;
        private readonly MapProvider _mapProvider;

        public override int ZIndex => 999;
        public override MapInfo Map => _mapProvider.GetMap("TiledTestMap");

        public EntityLayer(ActionProvider actionProvider, GameObjectProvider gameObjectProvider, DisplayManager displayManager, SceneManager sceneManager, UIObjectProvider uIObjectProvider, Movement movement, TileSetProvider tileSetProvider, MapProvider mapProvider):base(movement, tileSetProvider)
        {
            _actionProvider = actionProvider;
            _sceneManager = sceneManager;
            _gameObjectProvider = gameObjectProvider;
            _displayManager = displayManager;
            _uIObjectProvider = uIObjectProvider;
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

        public override void Tick()
        {
            base.Tick();
        }
        
    }
}
