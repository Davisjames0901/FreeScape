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
        
        public static Vector2 GetHeadingVectorFromDegrees(float degrees)
        {
            var radian = degrees * Math.PI / 180;
            return new Vector2((float)Math.Cos(radian), (float)Math.Sin(radian));
        }
        
        public static Vector2 GetHeadingVectorFromMovement(bool up, bool down, bool left, bool right)
        {
            var vec = new Vector2(GetXMovement(left, right), GetYMovement(up, down));
            //Detects a diagonal so we have to divide by two so speed isnt doubled
            if (vec.X != 0 && vec.Y != 0)
                return vec / 2;
            return vec;
        }
        
        private static int GetXMovement(bool left, bool right)
        {
            if (left == right)
                return 0;
            if (left)
                return -1;
            return 1;
        }

        private static int GetYMovement(bool up, bool down)
        {
            if (up == down)
                return 0;
            if (up)
                return -1;
            return 1;
        }

        public static Vector2 GetVelocity(Vector2 headingVector, float speed, float deltaTime)
        {
            return headingVector * speed * deltaTime;
        }

        public static Vector2 Floor(Vector2 v) => new Vector2((float)Math.Floor(v.X), (float)Math.Floor(v.Y));
        public static Vector2 Ceiling(Vector2 v) => new Vector2((float)Math.Ceiling(v.X), (float)Math.Ceiling(v.Y));
        public static Vector2 Reverse(this Vector2 v) => new Vector2(v.Y, v.X);

        public static bool IsGreaterThan(this Vector2 a, Vector2 b) => a.X > b.X && a.Y > b.Y;
        public static bool IsGreaterThanOrEquals(this Vector2 a, Vector2 b) => a.X >= b.X && a.Y >= b.Y;
        public static bool IsLessThan(this Vector2 a, Vector2 b) => IsGreaterThan(b, a);
        public static bool IsLessThanOrEquals(this Vector2 a, Vector2 b) => IsGreaterThanOrEquals(b, a);
    }


}
