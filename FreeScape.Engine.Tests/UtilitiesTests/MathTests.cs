using System.Numerics;
using FreeScape.Engine.Utilities;
using NUnit.Framework;

namespace FreeScape.Engine.Tests.UtilitiesTests
{
    public class MathTests
    {
        public const double ACCEPTABLE_ACCURACY = 0.000001;

        [Test]
        public void HeadingFromAngleIsUnitVector()
        {
            var vec = Maths.GetHeadingVectorFromDegrees(45);
            var normal = Vector2.Normalize(vec);
            VectorAsserts.AreEqual(normal, vec, ACCEPTABLE_ACCURACY);
        }

        [Test]
        public void HeadingVectorFromAngleIsAccurate()
        {
            var vec = Maths.GetHeadingVectorFromDegrees(0);
            VectorAsserts.AreEqual(Vector2.UnitX, vec, ACCEPTABLE_ACCURACY);
        }

        [Test]
        public void HeadingForDirectionIsUnit()
        {
            var vec = Maths.GetHeadingVectorFromMovement(true, false, false, true);
            var normal = Vector2.Normalize(vec);
            VectorAsserts.AreEqual(normal, vec, ACCEPTABLE_ACCURACY);
        }

        [Test]
        public void NearEqualsIsTrue()
        {
            var a = new Vector2(1, 0);
            var b = Vector2.Zero;
            
            //<1, 0> is exactly 1 away from <0, 0> so this should be true
            Assert.IsTrue(Maths.NearEquals(a, b, 1.0f));
        }
    }
}