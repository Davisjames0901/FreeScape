using System.Diagnostics;
using System;

namespace FreeScape.Engine.Providers
{
    public class FrameTimeProvider
    {
        private readonly Stopwatch _stopwatch;
        private readonly Stopwatch _renderStopwatch;
        private const double TICKS_PER_SECOND = 100000000;
        private double lastTickTime;

        
        public FrameTimeProvider()
        {
            _stopwatch = Stopwatch.StartNew();
            _renderStopwatch = Stopwatch.StartNew();
        }
        
        internal void Tick()
        {
            lastTickTime = _stopwatch.ElapsedTicks;
            _stopwatch.Restart();
        }

        internal void RenderTick()
        {
            _renderStopwatch.Restart();
        }

        public double DeltaTimeSeconds => DeltaTimeMilliSeconds/TICKS_PER_SECOND;
        public double DeltaTimeMilliSeconds => (lastTickTime / (float)Stopwatch.Frequency) * 10;
    }
}