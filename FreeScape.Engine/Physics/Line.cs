using System;
using System.Numerics;

namespace FreeScape.Engine.Physics
{
    public class Line
    {
        public Vector2 Start { get; }
        public Vector2 End { get; }

        public Line(Vector2 start, Vector2 end)
        {
            Start = start;
            End = end;
        }
        
        public Vector2? Intersection(Line otherLine)
        {
            var a1 = End.Y - Start.Y;
            var b1 = Start.X - End.X;
            var c1 = End.X * Start.Y - Start.X * End.Y;

            /* Compute r3 and r4.
             */

            var r3 = a1 * otherLine.Start.X + b1 * otherLine.Start.Y + c1;
            var r4 = a1 * otherLine.End.X + b1 * otherLine.End.Y + c1;

            /* Check signs of r3 and r4.  If both point 3 and point 4 lie on
             * same side of line 1, the line segments do not intersect.
             */

            if (r3 != 0 && r4 != 0 && Math.Sign(r3) == Math.Sign(r4))
            {
                return null; // DONT_INTERSECT
            }

            /* Compute a2, b2, c2 */

            var a2 = otherLine.End.Y - otherLine.Start.Y;
            var b2 = otherLine.Start.X - otherLine.End.X;
            var c2 = otherLine.End.X * otherLine.Start.Y - otherLine.Start.X * otherLine.End.Y;

            /* Compute r1 and r2 */

            var r1 = a2 * Start.X + b2 * Start.Y + c2;
            var r2 = a2 * End.X + b2 * End.Y + c2;

            /* Check signs of r1 and r2.  If both point 1 and point 2 lie
             * on same side of second line segment, the line segments do
             * not intersect.
             */
            if (r1 != 0 && r2 != 0 && Math.Sign(r1) == Math.Sign(r2))
            {
                return (null); // DONT_INTERSECT
            }

            /* Line segments intersect: compute intersection point. 
             */

            var denom = a1 * b2 - a2 * b1;
            if (denom == 0)
            {
                return null; //( COLLINEAR );
            }
            var offset = denom < 0 ? -denom / 2 : denom / 2;

            /* The denom/2 is to get rounding instead of truncating.  It
             * is added or subtracted to the numerator, depending upon the
             * sign of the numerator.
             */

            var num = b1 * c2 - b2 * c1;
            var x = (num < 0 ? num - offset : num + offset) / denom;

            num = a2 * c1 - a1 * c2;
            var y = (num < 0 ? num - offset : num + offset) / denom;
            return new Vector2(x - 0.5f, y - 0.5f);
        }
    }
}