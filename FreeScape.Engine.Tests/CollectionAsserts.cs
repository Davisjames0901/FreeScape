using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace FreeScape.Engine.Tests
{
    public static class CollectionAsserts
    {
        public static void AssertInBounds(IEnumerable<double> source, double average, double maximumDelta)
        {
            var min = source.Min();
            var max = source.Max();
            Assert.Greater(min, average - maximumDelta, $"Found a value ({min}) which was lower than the permitted lower bound of ${average - maximumDelta}");
            Assert.Less(max, average + maximumDelta, $"Found a value ({max}) which was higher than the permitted upper bound of ${average + maximumDelta}");
        }

        public static void AverageIsEqual(IEnumerable<double> source, double expectedValue, double maximumDelta = 0)
        {
            Assert.AreEqual(expectedValue, source.Average(), maximumDelta);
        }
    }
}