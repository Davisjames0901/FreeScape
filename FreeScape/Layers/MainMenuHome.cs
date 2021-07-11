using FreeScape.Engine.Config;
using FreeScape.Engine.GameObjects;
using FreeScape.Engine.GameObjects.UI;
using FreeScape.Engine.Managers;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render.Layers;
using FreeScape.Scenes;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FreeScape.Layers
{
    public class MainMenuHome : UILayer
    {
        private readonly TextureProvider _textureProvider;
        private readonly ActionProvider _actionProvider;
        private readonly SceneManager _sceneManager;
        private readonly DisplayManager _displayManager;
        Button PlayButton;
        Button quitButton;
        Button settingsButton;
        EmptyGameObject MainMenuCenter;
        public MainMenuHome(TextureProvider textureProvider, ActionProvider actionProvider, SceneManager sceneManager, DisplayManager displayManager, UIObjectProvider uIObjectProvider)
        {
            _textureProvider = textureProvider;
            _actionProvider = actionProvider;
            _displayManager = displayManager;

            MainMenuCenter = new EmptyGameObject();
            MainMenuCenter.Position = new Vector2(50, 125);

            //sceneManager.SetPerspectiveTarget("main", MainMenuCenter);

            Action playButtonOnClick = () => { sceneManager.SetScene<TestScene>(); };

            //PlayButton = uIObjectProvider.CreateButton();

            Vector2 buttonSize = new Vector2(100, 50);

            Vector2 playButtonPos = new Vector2(0, 0);


            Texture playButtonDefaultTexture = _textureProvider.GetTextureByFile("Buttons/Blue/Text/Play", "play:default");
            Texture playButtonHoverTexture = _textureProvider.GetTextureByFile("Buttons/Orange/Text/Play", "play:hover");

            PlayButton = new Button(playButtonPos, buttonSize, playButtonDefaultTexture, playButtonHoverTexture, playButtonOnClick, actionProvider);

            UIObjects.Add(PlayButton);

            actionProvider.SubscribeOnPressed(a =>
            {
                if (a == "LeftClick")
                    MouseClick();
            });
        }
        public override void Init()
        {
        }
        public void Tick()
        {

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
            PlayButton.Render(target);
        }
    }
}
