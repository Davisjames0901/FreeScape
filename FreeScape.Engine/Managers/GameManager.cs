using System;

namespace FreeScape.Engine.Managers
{
    public class GameManager
    {
        private bool _isRunning;
        private Action _tick;
        private Action _render;

        public void Start(Action tick, Action render)
        {
            _tick = tick;
            _render = render;
            _isRunning = true;

            if (OperatingSystem.IsLinux())
                Platform.XInitThreads();
            
            while (_isRunning)
            {
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