using SFML.System;
using System;
using System.Numerics;

namespace FreeScape.Engine.Utilities
{
    public static class Maths
    {
        public static float Lerp(float a, float b, float by)
        {
            return a + (b - a) * by;
        }

        public static Vector2 Lerp(Vector2 a, Vector2 b, float by, float maxEpsilon)
        {

            if (NearEquals(b, a, maxEpsilon))
            {
                return b;
            }

            var x = Lerp(a.X, b.X, by);
            var y = Lerp(a.Y, b.Y, by);
            return new Vector2(x, y);
        }
        public static bool NearEquals(float a, float b, float maxEpsilon)
        {
            if (Math.Abs(a) < maxEpsilon && Math.Abs(b) < maxEpsilon)
            {
                return true;
            }

            return false;
        }
        public static bool NearEquals(Vector2 a, Vector2 b, float maxEpsilon)
        {
            var dif = a - b;
            if (Math.Abs(dif.X) < maxEpsilon && Math.Abs(dif.Y) < maxEpsilon)
            {
                return true;
            }

            return false;
        }
    }


}
