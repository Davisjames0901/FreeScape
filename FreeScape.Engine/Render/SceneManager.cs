using System;
using Microsoft.Extensions.DependencyInjection;

namespace FreeScape.Engine.Render
{
    public class SceneManager
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DisplayManager _display;
        private IScene _currentScene;

        public SceneManager(IServiceProvider serviceProvider, DisplayManager display)
        {
            _serviceProvider = serviceProvider;
            _display = display;
        }

        public void Tick()
        {
            _display.Render(_currentScene);
            _currentScene.Tick();
        }

        public void SetScene<T>() where T : IScene
        {
            _currentScene?.Dispose();
            _currentScene = _serviceProvider.GetService<T>();
        }
    }
}