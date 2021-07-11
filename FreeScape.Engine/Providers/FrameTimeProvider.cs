using System.Diagnostics;
using System;

namespace FreeScape.Engine.Providers
{
    public class FrameTimeProvider
    {
        private readonly Stopwatch _stopwatch;
        private const double TICKS_PER_SECOND = 1000.0;
        public FrameTimeProvider()
        {
            _stopwatch = Stopwatch.StartNew();
        }
        
        internal void Tick()
        {
            _stopwatch.Restart();
        }

        public double DeltaTimeSeconds => DeltaTimeMilliSeconds/TICKS_PER_SECOND;
        public double DeltaTimeMilliSeconds => (_stopwatch.ElapsedTicks/(float)Stopwatch.Frequency)*10;
    }
}