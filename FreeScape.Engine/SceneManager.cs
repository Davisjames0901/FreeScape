using System;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace FreeScape.Engine
{
    public class SceneManager
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly GameInfo _gameInfo;
        private readonly RenderWindow _target;
        private IScene _currentScene;
        private readonly List<View> _views;

        public SceneManager(IServiceProvider serviceProvider, GameInfo gameInfo)
        {
            _serviceProvider = serviceProvider;
            _gameInfo = gameInfo;
            _target = CreateRenderWindow();
            _views = new List<View>();
            var view = new View(new Vector2f(0, 0), new Vector2f(gameInfo.ScreenWidth, gameInfo.ScreenHeight));
            _views.Add(view);
        }

        public void Tick()
        {
            _target.Clear();
            _target.DispatchEvents();

            foreach (var view in _views)
            {
                _target.SetView(view);
                if (_currentScene == null)
                    throw new Exception("No scene has been set!");
                if (_currentScene.Tick())
                {
                    _currentScene.Render(_target);
                    _target.Display();
                }
            }

        }

        private RenderWindow CreateRenderWindow()
        {
            var videoMode = new VideoMode(_gameInfo.ScreenWidth, _gameInfo.ScreenHeight);
            var target = new RenderWindow(videoMode, _gameInfo.Name);

            target.SetFramerateLimit(_gameInfo.RefreshRate);

            return target;
        }

        public void SetScene<T>() where T : IScene
        {
            _currentScene?.Dispose();
            _currentScene = _serviceProvider.GetService<T>();
        }
    }
}