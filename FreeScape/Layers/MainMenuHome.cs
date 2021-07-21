using FreeScape.Engine.GameObjects;
using FreeScape.Engine.GameObjects.UI;
using FreeScape.Engine.Managers;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render.Layers;
using FreeScape.Scenes;
using System;
using System.Numerics;
using FreeScape.Engine.Config.UI;
using FreeScape.Engine.Config;
using SFML.Graphics;
using FreeScape.Engine.Utilities;

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
        Texture BackgroundTexture;
        public MainMenuHome(GameInfo gameInfo, ActionProvider actionProvider, TextureProvider textureProvider, GameManager gameManager, SceneManager sceneManager, UIObjectProvider uIObjectProvider)
        {
            _actionProvider = actionProvider;
            _sceneManager = sceneManager;
            _uIObjectProvider = uIObjectProvider;
            _gameManager = gameManager;
            BackgroundTexture = textureProvider.GetTextureByFile("UI/MenuBackground", "MenuBackground");
            Background = new Sprite(BackgroundTexture);
            Background.Position = new Vector2(-750, -1300);
        }

        public override int ZIndex => 999;

        public override void Init()
        {
            MainMenuCenter = new EmptyGameObject();
            MainMenuSettingsCenter = new EmptyGameObject();
            MainMenuCenter.Position = new Vector2(0, 125);
            MainMenuSettingsCenter.Position = new Vector2(500, 125);


            _sceneManager.SetPerspectiveTarget("main", MainMenuCenter);
            _sceneManager.SetPerspectiveCenter(MainMenuCenter.Position);

            GenerateButtons();

            _actionProvider.SubscribeOnPressed(a =>
            {
                if (a == "LeftClick")
                    MouseClick();
            });
        }
        private Vector2 backgroundTrailPoint1 = new Vector2(-750, -1300);
        private Vector2 backgroundTrailPoint2 = new Vector2(-950, -1500);
        private Vector2 backgroundTrailPoint3 = new Vector2(-1150, -1300);
        private Vector2 backgroundTrailPoint4 = new Vector2(-950, -1100);
        int target = 1;
        private void BackgroundMovement()
        {
            if (Maths.NearEquals(Background.Position, backgroundTrailPoint1, 1f))
                target = 2;
            if (Maths.NearEquals(Background.Position, backgroundTrailPoint2, 1f))
                target = 3;
            if (Maths.NearEquals(Background.Position, backgroundTrailPoint3, 1f))
                target = 4;
            if (Maths.NearEquals(Background.Position, backgroundTrailPoint4, 1f))
                target = 1;

            if (target == 1)
                Background.Position = Maths.Lerp(Background.Position, backgroundTrailPoint1, 0.0005f);
            if (target == 2)
                Background.Position = Maths.Lerp(Background.Position, backgroundTrailPoint2, 0.0005f);
            if (target == 3)
                Background.Position = Maths.Lerp(Background.Position, backgroundTrailPoint3, 0.0005f);
            if (target == 4)
                Background.Position = Maths.Lerp(Background.Position, backgroundTrailPoint4, 0.0005f);
        }
        public override void Tick()
        {
            BackgroundMovement();
            base.Tick();
        }
        private void GenerateButtons()
        {

            ButtonInfo playButtonInfo = new ButtonInfo();
            playButtonInfo.Position = new Vector2(0, -100);
            playButtonInfo.Size = new Vector2(100, 34);
            playButtonInfo.Name = "play";
            playButtonInfo.OnClickAction = () => { _sceneManager.SetScene<TestScene>(); };
            playButtonInfo.ButtonTexture = "UI/Buttons/MainMenu/Play";
            playButtonInfo.Wigglable = true;

            ButtonInfo settingsButtonInfo = new ButtonInfo();
            settingsButtonInfo.Position = new Vector2(0, -50);
            settingsButtonInfo.Size = new Vector2(100, 34);
            settingsButtonInfo.Name = "settings";
            settingsButtonInfo.OnClickAction = () => { _sceneManager.SetPerspectiveTarget("main", MainMenuSettingsCenter); };
            settingsButtonInfo.ButtonTexture = "UI/Buttons/MainMenu/Settings";
            settingsButtonInfo.Wigglable = true;

            ButtonInfo quitButtonInfo = new ButtonInfo();
            quitButtonInfo.Position = new Vector2(0, 0);
            quitButtonInfo.Size = new Vector2(100, 34);
            quitButtonInfo.Name = "quit";
            quitButtonInfo.OnClickAction = () => { _gameManager.Stop(); };
            quitButtonInfo.ButtonTexture = "UI/Buttons/MainMenu/Quit";
            quitButtonInfo.Wigglable = true;

            ButtonInfo backButtonInfo = new ButtonInfo();
            backButtonInfo.Position = new Vector2(500, 150);
            backButtonInfo.Size = new Vector2(100, 34);
            backButtonInfo.Name = "back";
            backButtonInfo.OnClickAction = () => { _sceneManager.SetPerspectiveTarget("main", MainMenuCenter); };
            backButtonInfo.ButtonTexture = "UI/Buttons/MainMenu/Back";
            backButtonInfo.Wigglable = true;

            BackButton = _uIObjectProvider.CreateButton(backButtonInfo);
            PlayButton = _uIObjectProvider.CreateButton(playButtonInfo);
            SettingsButton = _uIObjectProvider.CreateButton(settingsButtonInfo);
            QuitButton = _uIObjectProvider.CreateButton(quitButtonInfo);

            UIObjects.Add(BackButton);
            UIObjects.Add(PlayButton);
            UIObjects.Add(SettingsButton);
            UIObjects.Add(QuitButton);
        }
        public void Render()
        {

        }
        public void MouseClick()
        {
            var mouseCoords = _actionProvider.GetMouseWorldCoods();
            foreach (var UIObject in UIObjects)
            {
                if (UIObject.Hovered && UIObject is IButton button)
                {
                    button.OnClick();
                    break;
                }
            }
        }
    }
}
