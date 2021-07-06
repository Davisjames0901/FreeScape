using System;
using System.Collections.Generic;
using System.Linq;
using FreeScape.Engine.GameObjects;
using FreeScape.Engine.Render;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace FreeScape.Engine.Managers
{
    public class DisplayManager
    {
        private readonly GameInfo _info;
        private RenderWindow _renderTarget;
        private readonly List<Perspective> _perspectives;

        public DisplayManager(GameInfo info)
        {
            _info = info;
            _perspectives = new List<Perspective>();
            Reset();
        }

        public void Render(IRenderable renderable)
        {
            _renderTarget.Clear();
            _renderTarget.DispatchEvents();
            foreach (var view in _perspectives)
            {
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
            var view = new View(new Vector2f(0, 0), new Vector2f(_info.ScreenWidth, _info.ScreenHeight));
            _perspectives.Add(new Perspective("main", view));

            _renderTarget.SetFramerateLimit(_info.RefreshRate);
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
            _renderTarget.KeyPressed += handle;
        }
    }
}