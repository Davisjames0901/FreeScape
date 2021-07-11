﻿using FreeScape.Engine.GameObjects;
using FreeScape.Engine.GameObjects.UI;
using FreeScape.Engine.Managers;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render.Layers;
using FreeScape.Scenes;
using System.Numerics;

namespace FreeScape.Layers
{
    public class PlayerUI : UILayer
    {


        private readonly GameObjectProvider _gameObjectProvider;
        private readonly DisplayManager _displayManager;
        private readonly SceneManager _sceneManager;
        private readonly UIObjectProvider _uIObjectProvider;
        private readonly ActionProvider _actionProvider;

        Button HomeButton;
        EmptyGameObject pauseMenuLocation;
        public PlayerUI(ActionProvider actionProvider, GameObjectProvider gameObjectProvider, DisplayManager displayManager, SceneManager sceneManager, UIObjectProvider uIObjectProvider)
        {
            _actionProvider = actionProvider;
            _sceneManager = sceneManager;
            _gameObjectProvider = gameObjectProvider;
            _displayManager = displayManager;
            _uIObjectProvider = uIObjectProvider;

            actionProvider.SubscribeOnPressed(a =>
            {
                if (a == "LeftClick")
                    MouseClick();
            });
        }
        public override void Init()
        {
            CreateUIButtons();
        }
        public void MouseClick()
        {
            var mouseCoords = _actionProvider.GetMouseWorldCoods();
            foreach (var uIObject in UIObjects)
            {
                if (uIObject.Hovered && uIObject is IButton button)
                {
                    button.OnClick();
                    break;
                }
            }
        }

        public override void Tick()
        {
            var view = _displayManager.CurrentPerspective.View;
            //Vector2 homeButtonPos = Vector2.Lerp(HomeButton.Position, view.Center - (view.Size / 2) + (new Vector2(25, 25)), 0.025f);
            Vector2 homeButtonPos = view.Center - (view.Size / 2) * 0.75f;
            HomeButton.Position = homeButtonPos;
            base.Tick();
        }
        private void CreateUIButtons()
        {
            ButtonInfo homeButtonInfo = new ButtonInfo();
            homeButtonInfo.Position = new Vector2(0, 0);
            homeButtonInfo.Size = new Vector2(50, 25);
            homeButtonInfo.Name = "homebutton";
            homeButtonInfo.OnClickAction = () => { _sceneManager.SetScene<MainMenuScene>(); };
            homeButtonInfo.ButtonTextureDefault = "Buttons/Blue/Text/Home";
            homeButtonInfo.ButtonTextureHover = "Buttons/Orange/Text/Home";

            HomeButton = _uIObjectProvider.CreateButton(homeButtonInfo);

            UIObjects.Add(HomeButton);
        }
    }
}
