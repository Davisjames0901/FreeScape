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

        public static Vector2 Lerp(this Vector2 a, Vector2 b, float by, float maxEpsilon = 0.1f)
        {
            if (a.NearEquals(b, maxEpsilon))
                return b;
            
            return Vector2.Lerp(a, b, by);
        }
        public static bool NearEquals(this Vector2 a, Vector2 b, float maxEpsilon = 0.1f)
        {
            var diff = Vector2.Abs(a - b);
            if (diff.X < maxEpsilon && diff.Y < maxEpsilon)
                return true;
            
            return false;
        }
    }


}
