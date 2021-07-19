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
        public IScene CurrentScene { get; private set; }

        public SceneManager(IServiceProvider serviceProvider, DisplayManager display, ServiceScopeProvider provider)
        {
            _serviceProvider = serviceProvider;
            _display = display;
            _provider = provider;
        }

        public void Render()
        {
            _display.Render(CurrentScene);
        }

        public void Tick()
        {
            CurrentScene.Tick();
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
            _provider.CurrentScope?.Dispose();
            CurrentScene?.Dispose();
            _provider.CurrentScope = _serviceProvider.CreateScope();
            CurrentScene = _provider.CurrentScope.ServiceProvider.GetService<T>();
            CurrentScene.Init();
        }
    }
}