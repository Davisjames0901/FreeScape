using System;
using System.Threading;

namespace FreeScape.Engine
{
    public class Controller
    {
        public Thread TickThread { get; set; }
        private bool _isRunning = false;
        public Thread RenderThread { get; set; }

        private Action _tick;
        private Action _render;
        private Object locker = new Object();
        public Controller()
        {
            TickThread = new Thread(TickThreadLoop);
            RenderThread = new Thread(RenderThreadLoop);
        }

        public void Start(Action tick, Action render)
        {
            _tick = tick;
            _render = render;
            
            _isRunning = true;

            TickThread.Start();
            RenderThread.Start();
        }
        public void Stop()
        {
            _isRunning = false;
        }

        public void TickThreadLoop()
        {
            while (_isRunning)
            {
                lock (locker)
                {
                    _tick();
                }
            }
        }
        public void RenderThreadLoop()
        {
            while (_isRunning)
            {
                lock (locker)
                {
                    _render();
                }
            }
        }

    }
}
