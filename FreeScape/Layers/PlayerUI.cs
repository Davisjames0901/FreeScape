using FreeScape.Engine.Render.Layers;
using FreeScape.Scenes;
using System.Numerics;
using FreeScape.Engine.Core;
using FreeScape.Engine.Core.GameObjects.UI;
using FreeScape.Engine.Core.Managers;
using FreeScape.Engine.Input;
using FreeScape.Engine.Render.Layers.LayerTypes;

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
        
        public PlayerUI(ActionProvider actionProvider, GameObjectProvider gameObjectProvider, DisplayManager displayManager, SceneManager sceneManager, UIObjectProvider uIObjectProvider)
        {
            _actionProvider = actionProvider;
            _sceneManager = sceneManager;
            _gameObjectProvider = gameObjectProvider;
            _displayManager = displayManager;
            _uIObjectProvider = uIObjectProvider;
            ZIndex = 9999;
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

        public override int ZIndex { get; }

        public override void Tick()
        {
            var view = _displayManager.CurrentPerspective.ScreenView;
            HomeButton.Position = view.Center - (view.Size / 2) * 0.9f;
            base.Tick();
        }
        private void CreateUIButtons()
        {
            var homeButtonInfo = new ButtonInfo();
            homeButtonInfo.Position = new Vector2(0, 0);
            homeButtonInfo.Size = new Vector2(50, 25);
            homeButtonInfo.OnClickAction = () => { _sceneManager.SetScene<MainMenuScene>(); };
            homeButtonInfo.ButtonTexture = "UI/Buttons/MainMenu/Menu";

            HomeButton = _uIObjectProvider.CreateButton(homeButtonInfo);

            UIObjects.Add(HomeButton);
        }
    }
}
