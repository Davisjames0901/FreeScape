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
        Button testButton;

        public MainMenuHome(TextureProvider textureProvider, ActionProvider actionProvider, SceneManager sceneManager)
        {

            _textureProvider = textureProvider;
            _actionProvider = actionProvider;
            Vector2 buttonPos = new Vector2(0, -100);
            Vector2 buttonSize = new Vector2(100, 50);

            Action testButtonOnClick = () => { sceneManager.SetScene<TestScene>(); };

            testButton = new Button(buttonPos, buttonSize, _textureProvider.GetTexture("playbutton:default"), _textureProvider.GetTexture("playbutton:hover"), testButtonOnClick, actionProvider);
            UIObjects.Add(testButton);

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
            testButton.Render(target);
        }
    }
}
