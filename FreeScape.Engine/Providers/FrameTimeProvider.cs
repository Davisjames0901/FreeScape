using System;
using System.Diagnostics;

namespace FreeScape.Engine.Providers
{
    public class FrameTimeProvider
    {
        private const double MS_PER_SECOND = 1000;
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

        public double DeltaTimeSeconds => DeltaTimeMilliSeconds/MS_PER_SECOND;
        public double DeltaTimeMilliSeconds => (_lastTicks / (float)Stopwatch.Frequency) * 1000;
    }
}