using System;
using System.Numerics;

namespace FreeScape.Engine.Core.Utilities
{
    public static class Maths
    {
        /// <summary>
        /// Preforms interpolation between two numbers based on the given weighting
        /// </summary>
        /// <param name="a">The source</param>
        /// <param name="b">The target</param>
        /// <param name="amount">A value between 0 and 1 that indicates the weighting of b</param>
        /// <returns>The interpolated value</returns>
        public static float Lerp(float a, float b, float amount)
        {
            return a + (b - a) * amount;
        }

        /// <summary>
        /// Preforms linear interpolation between two vectors based on the given weighting
        /// </summary>
        /// <param name="a">The source vector</param>
        /// <param name="b">The target vector</param>
        /// <param name="amount">A value between 0 and 1 that indicates the weighting of vector b</param>
        /// <param name="maxEpsilon">The minimum distance, stops the interpolation</param>
        /// <returns>The interpolated vector</returns>
        public static Vector2 Lerp(this Vector2 a, Vector2 b, float amount, float maxEpsilon = 0.1f)
        {
            if (a.NearEquals(b, maxEpsilon))
                return b;
            return Vector2.Lerp(a, b, amount);
        }
        
        /// <summary>
        /// Checks to see if vector A is close to vector B
        /// </summary>
        /// <param name="a">Vector A</param>
        /// <param name="b">Vector B</param>
        /// <param name="maxEpsilon">The minimum distance</param>
        /// <returns></returns>
        public static bool NearEquals(this Vector2 a, Vector2 b, float maxEpsilon = 0.1f)
        {
            var diff = Vector2.Abs(a - b);
            return diff.Length() <= maxEpsilon;
        }
        
        /// <summary>
        /// Creates a heading unit vector given an angle in degrees
        /// </summary>
        /// <param name="degrees">The desired angle in degrees</param>
        /// <returns>Heading vector</returns>
        public static Vector2 GetHeadingVectorFromDegrees(float degrees)
        {
            var radian = degrees * Math.PI / 180;
            return new Vector2((float)Math.Cos(radian), (float)Math.Sin(radian));
        }
        
        /// <summary>
        /// Creates a heading unit vector given cardinal directions
        /// </summary>
        /// <param name="up"></param>
        /// <param name="down"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>Heading vector</returns>
        public static Vector2 GetHeadingVectorFromMovement(bool up, bool down, bool left, bool right)
        {
            var vec = new Vector2(GetXMovement(left, right), GetYMovement(up, down));
            //Detects a diagonal so we have to divide by two so speed isnt doubled
            if (vec.X != 0 && vec.Y != 0)
                return vec * 0.70710677f;
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

        /// <summary>
        /// Converts a heading vector into the amount of distance that should have been covered given a speed and amount of time
        /// </summary>
        /// <param name="headingVector">Direction</param>
        /// <param name="speed">Speed</param>
        /// <param name="deltaTime">Time passed in ms</param>
        /// <returns>Distance covered</returns>
        public static Vector2 GetDistance(Vector2 headingVector, float speed, float deltaTime)
        {
            return headingVector * speed * deltaTime;
        }

        /// <summary>
        /// Floors the components of a vector2
        /// </summary>
        /// <param name="v"></param>
        /// <returns>The floored vector</returns>
        public static Vector2 Floor(Vector2 v) => new Vector2((float)Math.Floor(v.X), (float)Math.Floor(v.Y));
        
        /// <summary>
        /// Ceilings the components of a vector2
        /// </summary>
        /// <param name="v"></param>
        /// <returns>The cielinged vector</returns>
        public static Vector2 Ceiling(Vector2 v) => new Vector2((float)Math.Ceiling(v.X), (float)Math.Ceiling(v.Y));
        
        /// <summary>
        /// Reverses the components of a vector2
        /// </summary>
        /// <param name="v"></param>
        /// <returns>The reversed vector</returns>
        public static Vector2 Reverse(this Vector2 v) => new Vector2(v.Y, v.X);

        /// <summary>
        /// Compares if the matching components of vector A are greater than vector B
        /// </summary>
        /// <param name="a">Left side of comparison</param>
        /// <param name="b">Right side of comparison</param>
        /// <returns></returns>
        public static bool IsGreaterThan(this Vector2 a, Vector2 b) => a.X > b.X && a.Y > b.Y;
        
        /// <summary>
        /// Compares if the matching components of vector A are greater than or equal to vector B
        /// </summary>
        /// <param name="a">Left side of comparison</param>
        /// <param name="b">Right side of comparison</param>
        /// <returns></returns>
        public static bool IsGreaterThanOrEquals(this Vector2 a, Vector2 b) => a.X >= b.X && a.Y >= b.Y;
        
        /// <summary>
        /// Compares if the matching components of vector A are less than vector B
        /// </summary>
        /// <param name="a">Left side of comparison</param>
        /// <param name="b">Right side of comparison</param>
        /// <returns></returns>
        public static bool IsLessThan(this Vector2 a, Vector2 b) => IsGreaterThan(b, a);
        
        /// <summary>
        /// Compares if the matching components of vector A are less than or equal to vector B
        /// </summary>
        /// <param name="a">Left side of comparison</param>
        /// <param name="b">Right side of comparison</param>
        /// <returns></returns>
        public static bool IsLessThanOrEquals(this Vector2 a, Vector2 b) => IsGreaterThanOrEquals(b, a);
    }


}
