using System.Collections.Generic;
using FreeScape.Engine.Event;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace FreeScape.Engine.Render
{
    public class DisplayManager
    {
        private readonly GameInfo _info;
        private readonly EventManager _events;
        private RenderWindow _renderTarget;
        private readonly List<Perspective> _perspectives;

        public DisplayManager(GameInfo info, EventManager events)
        {
            _info = info;
            _events = events;
            _perspectives = new List<Perspective>();
            Reset();
        }

        public void Render(IRenderable renderable)
        {
            _renderTarget.Clear();
            _renderTarget.DispatchEvents();
            foreach (var view in _perspectives)
            {
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

            _renderTarget.KeyPressed += _events.TriggerKeyPressed;
            _renderTarget.KeyReleased += _events.TriggerKeyReleased;
        }
    }
}