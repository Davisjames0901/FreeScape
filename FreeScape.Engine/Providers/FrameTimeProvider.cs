using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FreeScape.Engine.Providers
{
    public class FrameTimeProvider
    {
        private const double MS_PER_SECOND = 1000;
        private long _lastTicks;
        private readonly Stopwatch _stopwatch;
        private readonly Queue<double> _lastMss;
        
        public FrameTimeProvider()
        {
            _stopwatch = Stopwatch.StartNew();
            _lastMss = new Queue<double>();
        }
        
        internal void Tick()
        {
            //_lastMss.Enqueue(DeltaTimeMilliSeconds);
            //if (_lastMss.Count > 1000)
            //{
            //    Console.WriteLine($"Last 1000 frames statistics. Average: {_lastMss.Average():F}ms; Min: {_lastMss.Min():F}ms; Max: {_lastMss.Max():F}ms");
            //    _lastMss.Clear();
            //}
            _lastTicks = _stopwatch.ElapsedTicks;
            _stopwatch.Restart();
        }

        public double DeltaTimeSeconds => DeltaTimeMilliSeconds/MS_PER_SECOND;
        public double DeltaTimeMilliSeconds => (_lastTicks / (float)Stopwatch.Frequency) * 1000;
    }
}