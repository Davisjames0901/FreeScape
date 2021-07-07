using System.Diagnostics;

namespace FreeScape.Engine.Providers
{
    public class FrameTimeProvider
    {
        private readonly Stopwatch _stopwatch;
        private const double TICKS_PER_SECOND = 1000000000.0;
        private const double TICKS_PER_MS = 1000000.0;
        public FrameTimeProvider()
        {
            _stopwatch = Stopwatch.StartNew();
        }
        
        internal void Tick()
        {
            _stopwatch.Restart();
        }

        public double DeltaTimeSeconds => DeltaTimeTicks/TICKS_PER_SECOND;
        public double DeltaTimeMilliSeconds => DeltaTimeTicks/TICKS_PER_MS;
        public float DeltaTimeTicks => (_stopwatch.ElapsedTicks/(float)Stopwatch.Frequency)*10000000;
    }
}