using System;
using FreeScape.Engine.Providers;

namespace FreeScape.Engine.Managers
{
    public class GameManager
    {
        private bool _isRunning;
        private Action _tick;
        private Action _render;
        private Action _collisions;
        private readonly FrameTimeProvider _frameTime;

        public GameManager(FrameTimeProvider frameTime)
        {
            _frameTime = frameTime;
        }

        public void Start(Action tick, Action render, Action collisions)
        {
            _tick = tick;
            _render = render;
            _collisions = collisions;
            _isRunning = true;

            if (OperatingSystem.IsLinux())
                Platform.XInitThreads();
            
            while (_isRunning)
            {
                _render();
                _tick();
                _collisions();
                _frameTime.Tick();            
            }
        }

        public void Stop()
        {
            _isRunning = false;
        }
    }
}