using System;
using System.Numerics;
using FreeScape.Engine.GameObjects;
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
        private readonly FrameTimeProvider _frameTime;
        private IScene _currentScene;
        public SceneManager(IServiceProvider serviceProvider, DisplayManager display, ServiceScopeProvider provider, FrameTimeProvider frameTime)
        {
            _serviceProvider = serviceProvider;
            _display = display;
            _provider = provider;
            _frameTime = frameTime;
        }

        public void Render()
        {
            _display.Render(_currentScene);
        }
        public void Tick()
        {
            _currentScene.Tick();
            _frameTime.Tick();
        }
        public void SetPerspectiveTarget(string perspectiveName, IGameObject target)
        {
            _display.Track(x => x.Name == perspectiveName, target);
        }
        public void SetPerspectiveCenter(Vector2 position)
        {
            _display.CurrentPerspective?.SetCenter(position);
        }
        public void SetScene<T>() where T : IScene
        {

            if (_currentScene != null)
            {
                _provider.CurrentScope.Dispose();
                _currentScene.Dispose();
            }
            _provider.CurrentScope = _serviceProvider.CreateScope();
            _currentScene = _provider.CurrentScope.ServiceProvider.GetService<T>();
            _currentScene.Init();
        }
    }
}