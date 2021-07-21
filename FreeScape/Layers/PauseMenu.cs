using FreeScape.Engine.GameObjects;
using FreeScape.Engine.GameObjects.UI;
using FreeScape.Engine.Managers;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render.Layers;
using FreeScape.Scenes;
using System.Numerics;
using FreeScape.Engine.Config.UI;
using FreeScape.Engine.Render;
using SFML.Graphics;

namespace FreeScape.Layers
{
    public class PauseMenu : UILayer
    {
        private readonly ActionProvider _actionProvider;
        private readonly DisplayManager _displayManager;
        private readonly SceneManager _sceneManager;
        private readonly UIObjectProvider _uIObjectProvider;
        private readonly SoundProvider _soundProvider;
        public bool IsPaused { get; private set; }
        public override int ZIndex => int.MaxValue;

        public PauseMenu(ActionProvider actionProvider, DisplayManager displayManager,
            SceneManager sceneManager, UIObjectProvider uIObjectProvider, SoundProvider soundProvider)
        {
            _actionProvider = actionProvider;
            _displayManager = displayManager;
            _sceneManager = sceneManager;
            _uIObjectProvider = uIObjectProvider;
            _soundProvider = soundProvider;
        }
        public override void Init()
        {
            GenerateButtons();
            
            _actionProvider.SubscribeOnPressed(a =>
            {
                if (a == "LeftClick")
                    MouseClick();
                if (a == "Pause")
                {
                    if (!IsPaused)
                    {
                        _actionProvider.SwitchActionMap("MainMenu");
                        _soundProvider.PauseMusic();
                        IsPaused = true;
                    }
                    else
                    {
                        _actionProvider.SwitchActionMap("Player");
                        _soundProvider.PlayMusic();
                        IsPaused = false;
                    }
                }
            });
        }

        private void GenerateButtons()
        {
            ButtonInfo playButtonInfo = new ButtonInfo();
            playButtonInfo.Position = new Vector2(0, -100);
            playButtonInfo.Size = new Vector2(100, 34);
            playButtonInfo.Name = "play";
            playButtonInfo.OnClickAction = () => 
            { 
                _actionProvider.SwitchActionMap("Player");
                _soundProvider.PlayMusic();
                IsPaused = false; 
            };
            playButtonInfo.ButtonTexture = "UI/Buttons/MainMenu/Play";
            playButtonInfo.Wigglable = true;

            ButtonInfo settingsButtonInfo = new ButtonInfo();
            settingsButtonInfo.Position = new Vector2(0, -50);
            settingsButtonInfo.Size = new Vector2(100, 34);
            settingsButtonInfo.Name = "settings";
            settingsButtonInfo.OnClickAction = () => {  };
            settingsButtonInfo.ButtonTexture = "UI/Buttons/MainMenu/Settings";
            settingsButtonInfo.Wigglable = true;

            ButtonInfo quitButtonInfo = new ButtonInfo();
            quitButtonInfo.Position = new Vector2(0, 0);
            quitButtonInfo.Size = new Vector2(100, 34);
            quitButtonInfo.Name = "quit";
            quitButtonInfo.OnClickAction = () => { _sceneManager.SetScene<MainMenuScene>(); };
            quitButtonInfo.ButtonTexture = "UI/Buttons/MainMenu/Quit";
            quitButtonInfo.Wigglable = true;

            var playButton = _uIObjectProvider.CreateButton(playButtonInfo);
            var settingsButton = _uIObjectProvider.CreateButton(settingsButtonInfo);
            var quitButton = _uIObjectProvider.CreateButton(quitButtonInfo);

            UIObjects.Add(playButton);
            UIObjects.Add(settingsButton);
            UIObjects.Add(quitButton);
        }

        public override void Render(RenderTarget target)
        {
            if (!IsPaused)
                return;
            foreach (var item in UIObjects)
            {
                item.Render(target);
            }
        }

        public void MouseClick()
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
