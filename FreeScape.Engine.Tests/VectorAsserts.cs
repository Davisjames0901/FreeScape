using System.Numerics;
using NUnit.Framework;

namespace FreeScape.Engine.Tests
{
    public static class VectorAsserts
    {
        public static void AreEqual(Vector2 expected, Vector2 actual, double maximumDelta = 0)
        {
            Assert.AreEqual(expected.X, actual.X, maximumDelta, $"Expected {expected} +/-{maximumDelta}\nbut was: {actual}");
            Assert.AreEqual(expected.Y, actual.Y, maximumDelta, $"Expected {expected} +/-{maximumDelta}\nbut was: {actual}");
        }
    }
}