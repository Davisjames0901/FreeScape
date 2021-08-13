using System.Collections.Generic;
using FreeScape.Engine.Providers;
using NUnit.Framework;

namespace FreeScape.Engine.Tests.ProviderTests
{
    public class FrameTimeTests
    {
        private const int NUMER_OF_RUNS = 20;
        private const double ACCEPTABLE_ACCURACY = 0.1;
        private const double MAXIMUM_DELTA = 0.4;
        private FrameTimeProvider _frameTime;

        [SetUp]
        public void Setup()
        {
            _frameTime = new FrameTimeProvider();
        }

        [Test]
        public void AccuratelyMeasures1Millisecond()
        {
            var times = new List<double>();
            for (var i = 0; i < NUMER_OF_RUNS; i++)
            {
                _frameTime.Tick();
                AccurateWaiter.WaitMs(1);
                _frameTime.Tick();
                times.Add(_frameTime.DeltaTimeMilliSeconds);
            }
            CollectionAsserts.AverageIsEqual(times, 1, ACCEPTABLE_ACCURACY);
            CollectionAsserts.AssertInBounds(times, 1, MAXIMUM_DELTA);
        }

        [Test]
        public void AccuratelyMeasures1Second()
        {
            var times = new List<double>();
            for (var i = 0; i < NUMER_OF_RUNS; i++)
            {
                _frameTime.Tick();
                AccurateWaiter.WaitMs(1000);
                _frameTime.Tick();
                times.Add(_frameTime.DeltaTimeSeconds);
            }
            CollectionAsserts.AverageIsEqual(times, 1, ACCEPTABLE_ACCURACY);
            CollectionAsserts.AssertInBounds(times, 1, MAXIMUM_DELTA);
        }
    }
}