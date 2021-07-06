using SFML.System;

namespace FreeScape.Engine.Utilities
{
    public static class Maths
    {
        public static float Lerp(float a, float b, float by)
        {
            return a + (b - a) * by;
        }

        public static Vector2f Lerp(Vector2f a, Vector2f b, float by)
        {

            float firstX = a.X, firstY = a.Y;
            float secondX = b.X, secondY = b.Y;

            var nearZeroX = firstX < secondX ? 0.01f : -0.01f;
            var nearZeroY = firstY < secondY ? 0.01f : -0.01f;
            
            var nearZero2f = new Vector2f(nearZeroX, nearZeroY);
            var diff = b - a;
            
            if ((secondX > firstX && diff.X <= nearZero2f.X || secondX < firstX && diff.X >= nearZero2f.X) && (secondY > firstY && diff.Y <= nearZero2f.Y || secondY < firstY && diff.Y >= nearZero2f.Y))
            {
                return b;
            }

            var x = Lerp(a.X, b.X, by);
            var y = Lerp(a.Y, b.Y, by);
            return new Vector2f(x, y);
        }
        public static Vector3f Lerp(Vector3f a, Vector3f b, float by)
        {
            var x = Lerp(a.X, b.X, by);
            var y = Lerp(a.Y, b.Y, by);
            var z = Lerp(a.Z, b.Z, by);
            return new Vector3f(x, y, z);
        }
    }


}
