using SFML.System;
using System;

namespace FreeScape.Engine.Utilities
{
    public static class Maths
    {
        public static float Lerp(float a, float b, float by)
        {
            return a + (b - a) * by;
        }

        public static Vector2f Lerp(Vector2f a, Vector2f b, float by, float maxEpsilon)
        {

            if (NearEquals(b, a, maxEpsilon))
            {
                return b;
            }

            var x = Lerp(a.X, b.X, by);
            var y = Lerp(a.Y, b.Y, by);
            return new Vector2f(x, y);
        }
        public static bool NearEquals(float a, float b, float maxEpsilon)
        {
            if (Math.Abs(b - a) < maxEpsilon)
            {
                return true;
            }

            return false;
        }
        public static bool NearEquals(Vector2f a, Vector2f b, float maxEpsilon)
        {
            if (NearEquals(a.X, b.X, maxEpsilon) && NearEquals(a.Y, b.Y, maxEpsilon))
            {
                return true;
            }

            return false;
        }

        public static Vector2f Vector2ITo2F(Vector2i vec)
        {
            return new Vector2f(vec.X, vec.Y);
        }
    }


}
