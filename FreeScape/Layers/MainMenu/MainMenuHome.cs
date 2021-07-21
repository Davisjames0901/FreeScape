using System.Numerics;
using FreeScape.Engine.Config.UI;
using FreeScape.Engine.GameObjects.UI;
using FreeScape.Engine.Managers;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render.Layers;
using FreeScape.Scenes;

namespace FreeScape.Layers.MainMenu
{
    public class MainMenuHome : UILayer
    {
        private readonly ActionProvider _actionProvider;
        private readonly SceneManager _sceneManager;
        private readonly UIObjectProvider _uIObjectProvider;
        private readonly GameManager _gameManager;
        public MainMenuHome(ActionProvider actionProvider, GameManager gameManager, 
            SceneManager sceneManager, UIObjectProvider uIObjectProvider)
        {
            _actionProvider = actionProvider;
            _sceneManager = sceneManager;
            _uIObjectProvider = uIObjectProvider;
            _gameManager = gameManager;
        }

        public override int ZIndex => 999;

        public override void Init()
        {
            GenerateButtons();

            _actionProvider.SubscribeOnPressed(a =>
            {
                if (a == "LeftClick")
                    MouseClick();
            });
        }

        private void GenerateButtons()
        {
            ButtonInfo playButtonInfo = new ButtonInfo();
            playButtonInfo.Position = new Vector2(0, -100);
            playButtonInfo.Size = new Vector2(100, 34);
            playButtonInfo.OnClickAction = () => { _sceneManager.SetScene<TestScene>(); };
            playButtonInfo.ButtonTexture = "UI/Buttons/MainMenu/Play";
            playButtonInfo.Wigglable = true;

            ButtonInfo settingsButtonInfo = new ButtonInfo();
            settingsButtonInfo.Position = new Vector2(0, -50);
            settingsButtonInfo.Size = new Vector2(100, 34);
            settingsButtonInfo.OnClickAction = () => { /*Yeah I need to fix this*/ };
            settingsButtonInfo.ButtonTexture = "UI/Buttons/MainMenu/Settings";
            settingsButtonInfo.Wigglable = true;

            ButtonInfo quitButtonInfo = new ButtonInfo();
            quitButtonInfo.Position = new Vector2(0, 0);
            quitButtonInfo.Size = new Vector2(100, 34);
            quitButtonInfo.OnClickAction = () => { _gameManager.Stop(); };
            quitButtonInfo.ButtonTexture = "UI/Buttons/MainMenu/Quit";
            quitButtonInfo.Wigglable = true;

            ButtonInfo backButtonInfo = new ButtonInfo();
            backButtonInfo.Position = new Vector2(500, 150);
            backButtonInfo.Size = new Vector2(100, 34);
            backButtonInfo.OnClickAction = () => { /*Need to figure out a new way to do this too*/ };
            backButtonInfo.ButtonTexture = "UI/Buttons/MainMenu/Back";
            backButtonInfo.Wigglable = true;

            UIObjects.Add(_uIObjectProvider.CreateButton(backButtonInfo));
            UIObjects.Add(_uIObjectProvider.CreateButton(playButtonInfo));
            UIObjects.Add(_uIObjectProvider.CreateButton(settingsButtonInfo));
            UIObjects.Add(_uIObjectProvider.CreateButton(quitButtonInfo));
        }
        
        private void MouseClick()
        {
            foreach (var uiObject in UIObjects)
            {
                if (uiObject.Hovered && uiObject is IButton button)
                {
                    button.OnClick();
                    break;
                }
            }
        }
    }
}
