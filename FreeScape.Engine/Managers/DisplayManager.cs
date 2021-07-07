using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using FreeScape.Engine.Config;
using FreeScape.Engine.GameObjects;
using FreeScape.Engine.Render;
using FreeScape.Engine.Utilities;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace FreeScape.Engine.Managers
{
    public class DisplayManager
    {
        private readonly GameInfo _info;
        private readonly GameManager _gameManager;
        private RenderWindow _renderTarget;
        private readonly List<Perspective> _perspectives;
        public Perspective CurrentPerspective { get; private set; }

        public DisplayManager(GameInfo info, GameManager gameManager)
        {
            _info = info;
            _gameManager = gameManager;
            _perspectives = new List<Perspective>();
            Reset();
        }

        public void Render(IRenderable renderable)
        {
            _renderTarget.Clear();
            _renderTarget.DispatchEvents();
            foreach (var view in _perspectives)
            {
                CurrentPerspective = view;
                view.Tick();
                _renderTarget.SetView(view.View);
                renderable.Render(_renderTarget);
                _renderTarget.Display();
            }
        }

        public void Reset()
        {
            _renderTarget?.Close();
            var videoMode = new VideoMode(_info.ScreenWidth, _info.ScreenHeight);
            _renderTarget = new RenderWindow(videoMode, _info.Name);
            var view = new Perspective("main", new Vector2f(0.0f, 0.0f), _info.ScreenSize, 3.0f);
            _perspectives.Add(view);

            _renderTarget.SetFramerateLimit(_info.RefreshRate);
            _renderTarget.SetActive(false);
            _renderTarget.Closed += (sender, args) => _gameManager.Stop();
        }

        public void Track(Func<Perspective, bool> selector, IGameObject target)
        {
            _perspectives.FirstOrDefault(selector)?.Track(target);
        }

        internal void RegisterOnPressed(EventHandler<KeyEventArgs> handle)
        {
            _renderTarget.KeyPressed += handle;
        }
        internal void RegisterOnReleased(EventHandler<KeyEventArgs> handle)
        {
            _renderTarget.KeyReleased += handle;
        }

        internal void RegisterOnPressed(EventHandler<MouseButtonEventArgs> handle)
        {
            _renderTarget.MouseButtonPressed += handle;
        }

        internal void RegisterOnReleased(EventHandler<MouseButtonEventArgs> handle)
        {
            _renderTarget.MouseButtonReleased += handle;
        }

        internal Vector2f GetMouseWindowPosition()
        {
            return Maths.Vector2ITo2F(Mouse.GetPosition(_renderTarget));
        }
        
        internal Vector2f GetMouseWorldPosition()
        {
            Console.WriteLine(CurrentPerspective.Corner);
            return GetMouseWindowPosition() + CurrentPerspective.Corner;
        }
    }
}