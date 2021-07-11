using FreeScape.Engine.GameObjects;
using FreeScape.Engine.GameObjects.UI;
using FreeScape.Engine.Managers;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render.Layers;
using FreeScape.Scenes;
using SFML.Graphics;
using System;
using System.Numerics;

namespace FreeScape.Layers
{
    public class MainMenuHome : UILayer
    {
        private readonly ActionProvider _actionProvider;
        private readonly SceneManager _sceneManager;
        private readonly UIObjectProvider _uIObjectProvider;
        private readonly GameManager _gameManager;
        Button PlayButton;
        Button QuitButton;
        Button SettingsButton;
        Button BackButton;
        EmptyGameObject MainMenuCenter;
        EmptyGameObject MainMenuSettingsCenter;
        public MainMenuHome(ActionProvider actionProvider, GameManager gameManager, SceneManager sceneManager, UIObjectProvider uIObjectProvider)
        {
            _actionProvider = actionProvider;
            _sceneManager = sceneManager;
            _uIObjectProvider = uIObjectProvider;
            _gameManager = gameManager;
        }
        public override void Init()
        {
            MainMenuCenter = new EmptyGameObject();
            MainMenuSettingsCenter = new EmptyGameObject();
            MainMenuCenter.Position = new Vector2(50, 125);
            MainMenuSettingsCenter.Position = new Vector2(550, 125);

            _sceneManager.SetPerspectiveTarget("main", MainMenuCenter);
            _sceneManager.SetPerspectiveCenter(MainMenuCenter.Position);

            GenerateButtons();

            _actionProvider.SubscribeOnPressed(a =>
            {
                if (a == "LeftClick")
                    MouseClick();
            });
        }
        public void Tick()
        {

        }

        private void GenerateButtons()
        {

            ButtonInfo playButtonInfo = new ButtonInfo();
            playButtonInfo.Position = new Vector2(0, 0);
            playButtonInfo.Size = new Vector2(100, 50);
            playButtonInfo.Name = "playbutton";
            playButtonInfo.OnClickAction = () => { _sceneManager.SetScene<TestScene>(); };
            playButtonInfo.ButtonTextureDefault = "Buttons/Blue/Text/Play";
            playButtonInfo.ButtonTextureHover = "Buttons/Orange/Text/Play";

            ButtonInfo settingsButtonInfo = new ButtonInfo();
            settingsButtonInfo.Position = new Vector2(-7.5f, 75);
            settingsButtonInfo.Size = new Vector2(115, 50);
            settingsButtonInfo.Name = "settingsbutton";
            settingsButtonInfo.OnClickAction = () => { _sceneManager.SetPerspectiveTarget("main", MainMenuSettingsCenter); };
            settingsButtonInfo.ButtonTextureDefault = "Buttons/Blue/Text/Check";
            settingsButtonInfo.ButtonTextureHover = "Buttons/Orange/Text/Check";

            ButtonInfo quitButtonInfo = new ButtonInfo();
            quitButtonInfo.Position = new Vector2(0, 150);
            quitButtonInfo.Size = new Vector2(100, 50);
            quitButtonInfo.Name = "quitbutton";
            quitButtonInfo.OnClickAction = () => { _gameManager.Stop(); };
            quitButtonInfo.ButtonTextureDefault = "Buttons/Blue/Text/Quit";
            quitButtonInfo.ButtonTextureHover = "Buttons/Orange/Text/Quit";

            ButtonInfo backButtonInfo = new ButtonInfo();
            backButtonInfo.Position = new Vector2(500, 150);
            backButtonInfo.Size = new Vector2(100, 50);
            backButtonInfo.Name = "backbutton";
            backButtonInfo.OnClickAction = () => { _sceneManager.SetPerspectiveTarget("main", MainMenuCenter); };
            backButtonInfo.ButtonTextureDefault = "Buttons/Blue/Text/Back";
            backButtonInfo.ButtonTextureHover = "Buttons/Orange/Text/Back";

            BackButton = _uIObjectProvider.CreateButton(backButtonInfo);
            PlayButton = _uIObjectProvider.CreateButton(playButtonInfo);
            SettingsButton = _uIObjectProvider.CreateButton(settingsButtonInfo);
            QuitButton = _uIObjectProvider.CreateButton(quitButtonInfo);

            UIObjects.Add(BackButton);
            UIObjects.Add(PlayButton);
            UIObjects.Add(SettingsButton);
            UIObjects.Add(QuitButton);
        }

        public void MouseClick()
        {
            var mouseCoords = _actionProvider.GetMouseWorldCoods();
            Console.WriteLine(mouseCoords);
            foreach (var UIObject in UIObjects)
            {
                if (UIObject.Hovered && UIObject is IButton button)
                {
                    button.OnClick();
                    break;
                }
            }
        }

        public void Render(RenderTarget target)
        {
            //PlayButton.Render(target);
        }
    }
}
