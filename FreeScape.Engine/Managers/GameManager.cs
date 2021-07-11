using System;
using System.Threading;
using FreeScape.Engine.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace FreeScape.Engine.Managers
{
    public class GameManager
    {
        private bool _isRunning;
        private Action _tick;
        private Action _render;
        private readonly FrameTimeProvider _frameTime;

        public GameManager(FrameTimeProvider frameTime)
        {
            _frameTime = frameTime;
        }

        public void Start(Action tick, Action render)
        {
            _tick = tick;
            _render = render;

            _isRunning = true;

            if (OperatingSystem.IsLinux())
                Platform.XInitThreads();
            while (_isRunning)
            {
                _frameTime.Tick();
                _tick();
                _render();
            }
        }

        public void Stop()
        {
            _isRunning = false;
        }
    }
}