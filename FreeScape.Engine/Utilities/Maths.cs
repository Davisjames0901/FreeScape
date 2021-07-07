﻿using SFML.System;
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
        public static bool NearEquals(Vector2f a, Vector2f b, float maxEpsilon)
        {
            if (Math.Abs(b.X - a.X) < maxEpsilon && Math.Abs(b.Y - a.Y) < maxEpsilon)
            {
                return true;
            }

            return false;
        }
        public static bool NearEquals(Vector3f a, Vector3f b, float maxEpsilon)
        {
            if (Math.Abs(b.X - a.X) < maxEpsilon && Math.Abs(b.Y - a.Y) < maxEpsilon && Math.Abs(b.Z - a.Z) < maxEpsilon)
            {
                return true;
            }

            return false;
        }
        public static Vector3f Lerp(Vector3f a, Vector3f b, float by, float maxEpsilon)
        {
            if (NearEquals(b, a, maxEpsilon))
            {
                return b;
            }
            var x = Lerp(a.X, b.X, by);
            var y = Lerp(a.Y, b.Y, by);
            var z = Lerp(a.Z, b.Z, by);
            return new Vector3f(x, y, z);
        }

        public static Vector2f Vector2ITo2F(Vector2i vec)
        {
            return new Vector2f(vec.X, vec.Y);
        }
    }


}
