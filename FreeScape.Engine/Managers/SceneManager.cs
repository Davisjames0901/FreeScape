using System;
using FreeScape.Engine.Providers;
using FreeScape.Engine.Render.Scenes;
using Microsoft.Extensions.DependencyInjection;

namespace FreeScape.Engine.Managers
{
    public class SceneManager
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DisplayManager _display;
        private readonly ServiceScopeProvider _provider;
        private IScene _currentScene;
        public SceneManager(IServiceProvider serviceProvider, DisplayManager display, ServiceScopeProvider provider)
        {
            _serviceProvider = serviceProvider;
            _display = display;
            _provider = provider;
        }

        public void Render()
        {
            _display.Render(_currentScene);
        }
        public void Tick()
        {
            _currentScene.Tick();
        }

        public void SetScene<T>() where T : IScene
        {
            _provider.CurrentScope?.Dispose();
            _currentScene?.Dispose();
            _provider.CurrentScope = _serviceProvider.CreateScope();
            _currentScene = _provider.CurrentScope.ServiceProvider.GetService<T>();
            _currentScene.Init();
        }
    }
}