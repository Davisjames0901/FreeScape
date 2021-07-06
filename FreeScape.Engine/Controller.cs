using System;
using System.Threading;

namespace FreeScape.Engine
{
    public class Controller
    {
        private readonly Thread _tickThread;

        private bool _isRunning;

        private Action _tick;
        private Action _render;
        public Controller()
        {
            _tickThread = new Thread(TickThreadLoop);
        }

        public void Start(Action tick, Action render)
        {
            _tick = tick;
            _render = render;
            
            _isRunning = true;

            if (OperatingSystem.IsLinux())
            {
                while (_isRunning)
                {
                    _tick();
                    _render();
                }
            }
            else
            {
                _tickThread.Start();
                RenderThreadLoop();
            }
        }
        public void Stop()
        {
            _isRunning = false;
        }

        private void TickThreadLoop()
        {
            while (_isRunning)
            {
                Thread.Sleep(10);
                _tick();
            }
        }
        private void RenderThreadLoop()
        {
            while (_isRunning)
            {
                _render();
            }
        }

    }
}
