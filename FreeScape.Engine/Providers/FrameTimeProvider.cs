using System.Diagnostics;

namespace FreeScape.Engine.Providers
{
    public class FrameTimeProvider
    {
        private readonly Stopwatch _stopwatch;
        public FrameTimeProvider()
        {
            _stopwatch = Stopwatch.StartNew();
        }
        
        internal void Tick()
        {
            _stopwatch.Restart();
        }

        public long TimeSinceLastTick => _stopwatch.ElapsedTicks;
    }
}