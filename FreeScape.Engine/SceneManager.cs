using System;
using Microsoft.Extensions.DependencyInjection;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace FreeScape.Engine
{
    public class SceneManager
    {
        private readonly IServiceProvider _serviceProvider;
        private GameInfo _gameInfo;
        private readonly RenderWindow _target;
        private IScene _currentScene;
        public SceneManager(IServiceProvider serviceProvider, GameInfo gameInfo)
        {
            _serviceProvider = serviceProvider;
            _gameInfo = gameInfo;
            _target = new RenderWindow(new VideoMode(gameInfo.ScreenWidth, gameInfo.ScreenHeight), _gameInfo.Name);
        }
        public void Tick()
        {
            _target.DispatchEvents();
            
            if (_currentScene == null)
                throw new Exception("No scene has been set!");
            if (_currentScene.Tick())
            {
                var rect = new RectangleShape(new Vector2f(_gameInfo.ScreenWidth, _gameInfo.ScreenHeight));
                rect.FillColor = Color.Black;
                _target.Draw(rect);
                
                _currentScene.Render(_target);
                _target.Display();
            }
        }

        public void SetScene<T>() where T : IScene
        {
            _currentScene?.Dispose();
            _currentScene = _serviceProvider.GetService<T>();
        }
    }
}