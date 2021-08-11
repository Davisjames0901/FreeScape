using System.Collections.Generic;
using System.Linq;
using FreeScape.Engine.Providers;
using NUnit.Framework;

namespace FreeScape.Engine.Tests.ProviderTests
{
    public class FrameTimeTests
    {
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
            for (var i = 0; i < 20; i++)
            {
                _frameTime.Tick();
                AccurateWaiter.WaitMs(1);
                _frameTime.Tick();
                times.Add(_frameTime.DeltaTimeMilliSeconds);
            }
            Assert.AreEqual(1d, times.Average(), 0.01);
        }

        [Test]
        public void AccuratelyMeasures1Second()
        {
            var times = new List<double>();
            for (var i = 0; i < 20; i++)
            {
                _frameTime.Tick();
                AccurateWaiter.WaitMs(1000);
                _frameTime.Tick();
                times.Add(_frameTime.DeltaTimeMilliSeconds);
            }
            Assert.AreEqual(1, times.Average(), 0.01);
        }
    }
}