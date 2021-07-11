using System;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace FreeScape.Engine.Managers
{
    public class GameManager
    {
        private readonly Thread _tickThread;

        private bool _isRunning;

        private Action _tick;
        private Action _render;

        public GameManager()
        {
            //_tickThread = new Thread(TickThreadLoop);
        }

        public void Start(Action tick, Action render)
        {
            _tick = tick;
            _render = render;

            _isRunning = true;

            //if (true || OperatingSystem.IsLinux())
            //{
                if (OperatingSystem.IsLinux())
                    Platform.XInitThreads();
                while (_isRunning)
                {
                    Thread.Sleep(10);
                    _tick();
                    _render();
                }
            //}

            //_tickThread.Start();
            //RenderThreadLoop();
        }

        public void Stop()
        {
            _isRunning = false;
        }

        private void TickThreadLoop()
        {
            while (_isRunning)
            {
                Thread.Sleep(1);
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