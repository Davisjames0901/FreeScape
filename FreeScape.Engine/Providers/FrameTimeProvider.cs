using System.Diagnostics;

namespace FreeScape.Engine.Providers
{
    public class FrameTimeProvider
    {
        private const double TICKS_PER_SECOND = 100000000;
        private long _lastTicks;
        private readonly Stopwatch _stopwatch;
        
        public FrameTimeProvider()
        {
            _stopwatch = Stopwatch.StartNew();
        }
        
        internal void Tick()
        {
            _lastTicks = _stopwatch.ElapsedTicks;
            _stopwatch.Restart();
        }

        public double DeltaTimeSeconds => DeltaTimeMilliSeconds/TICKS_PER_SECOND;
        public double DeltaTimeMilliSeconds => (_lastTicks / (float)Stopwatch.Frequency) * 1000;
    }
}