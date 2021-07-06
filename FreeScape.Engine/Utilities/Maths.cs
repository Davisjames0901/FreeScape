using SFML.System;

namespace FreeScape.Engine.Utilities
{
    public static class Maths
    {
        public static float Lerp(float a, float b, float by)
        {
            return (a + (b - a) * by);
        }

        public static Vector2f Lerp(Vector2f a, Vector2f b, float by)
        {

            float firstX = a.X, firstY = a.Y;
            float secondX = b.X, secondY = b.Y;

            float NearZeroX = firstX < secondX ? 0.01f : -0.01f;
            float NearZeroY = firstY < secondY ? 0.01f : -0.01f;
            
            var NearZero2f = new Vector2f(NearZeroX, NearZeroY);
            var Diff = b - a;
            
            if ((secondX > firstX && Diff.X <= NearZero2f.X || secondX < firstX && Diff.X >= NearZero2f.X) && (secondY > firstY && Diff.Y <= NearZero2f.Y || secondY < firstY && Diff.Y >= NearZero2f.Y))
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
