using System;
using System.Collections.Generic;
using System.Numerics;

namespace FreeScape.Engine.Physics
{


    public struct GJKCollisionInfo
    {
        public bool collided;
        public Vector2[] simplex;
    }

    public struct Edge
    {
        public Vector2 normal;
        public int index;
        public double distance;
    }
    public struct GJKEPACollisonInfo
    {
        public Edge closest;
        public GJKCollisionInfo info;

        public Vector2 GetPenetrationVector()
        {
            return closest.normal * (float)closest.distance;
        }
    }
    public static class GJKCollision
    {
        static uint IndexOfFurthestPoint(Vector2[] vertices, Vector2 d)
        {
            uint index = 0;
            float maxProduct = Vector2.Dot(d, vertices[index]);
            for (uint i = 1; i < vertices.Length; i++)
            {
                float product = Vector2.Dot(d, vertices[i]); // may be negative
                if (product > maxProduct)
                {
                    maxProduct = product;
                    index = i;
                }
            }
            return index;
        }

        public static Vector2 Support(Vector2[] vertices1, // first shape
                Vector2[] vertices2, // second shape
                Vector2 d)// direction
        {

            // get furthest point of first body along an arbitrary direction
            uint i = IndexOfFurthestPoint(vertices1, d);

            // get furthest point of second body along the opposite direction
            // note that this time direction is negated
            uint j = IndexOfFurthestPoint(vertices2, -d);

            // return the Minkowski sum of two points to see if bodies 'overlap'
            // note that the second point-vector is negated, a + (-b) = c
            return Vector2.Add(vertices1[i], -vertices2[j]);
        }

        static Vector2 AveragePoint(Vector2[] vertices)
        {
            Vector2 avg = Vector2.Zero;
            for (uint i = 0; i < vertices.Length; i++)
            {
                avg.X += vertices[i].X;
                avg.Y += vertices[i].Y;
            }
            avg.X /= vertices.Length;
            avg.Y /= vertices.Length;
            return avg;
        }

        static Vector2 TripleProduct(Vector2 a, Vector2 b, Vector2 c)
        {

            Vector2 r;

            float ac = a.X * c.X + a.Y * c.Y; // perform a.dot(c)
            float bc = b.X * c.X + b.Y * c.Y; // perform b.dot(c)

            // perform b * a.dot(c) - a * b.dot(c)
            r.X = b.X * ac - a.X * bc;
            r.Y = b.Y * ac - a.Y * bc;
            return r;
        }

        public static bool GJKCheckCollision(Vector2[] vertices1, Vector2[] vertices2, out int iter_count)
        {
            iter_count = 0;
            uint index = 0; // index of current vertex of simplex
            Vector2 a, b, c, direction, ao, ab, ac, abperp, acperp;
            Vector2[] simplex = new Vector2[3];


            Vector2 position1 = AveragePoint(vertices1); // not a CoG but
            Vector2 position2 = AveragePoint(vertices2); // it's ok for GJK )

            // initial direction from the center of 1st body to the center of 2nd body
            direction = Vector2.Subtract(position1, position2);

            // if initial direction is zero – set it to any arbitrary axis (we choose X)
            if ((direction.X == 0) && (direction.Y == 0)) direction.X = 1.0f;

            // set the first support as initial point of the new simplex
            a = simplex[0] = Support(vertices1, vertices2, direction);

            if (Vector2.Dot(a, direction) <= 0)
                return false; // no collision

            direction = Vector2.Negate(a); // The next search direction is always towards the origin, so the next search direction is negate(a)

            while (true)
            {
                iter_count++;

                a = simplex[++index] = Support(vertices1, vertices2, direction);

                if (Vector2.Dot(a, direction) <= 0)
                    return false; // no collision

                ao = Vector2.Negate(a); // from point A to Origin is just negative A

                // simplex has 2 points (a line segment, not a triangle yet)
                if (index < 2)
                {
                    b = simplex[0];
                    ab = Vector2.Subtract(b, a); // from point A to B
                    direction = TripleProduct(ab, ao, ab); // normal to AB towards Origin
                    if (direction.LengthSquared() == 0)
                        direction = new Vector2(ab.Y, -ab.X);
                    continue; // skip to next iteration
                }

                b = simplex[1];
                c = simplex[0];
                ab = Vector2.Subtract(b, a); // from point A to B
                ac = Vector2.Subtract(c, a); // from point A to C

                acperp = TripleProduct(ab, ac, ac);

                if (Vector2.Dot(acperp, ao) >= 0)
                {

                    direction = acperp; // new direction is normal to AC towards Origin

                }
                else
                {

                    abperp = TripleProduct(ac, ab, ab);

                    if (Vector2.Dot(abperp, ao) < 0)
                        return true; // collision

                    simplex[0] = simplex[1]; // swap first element (point C)

                    direction = abperp; // new direction is normal to AB towards Origin
                }

                simplex[1] = simplex[2]; // swap element in the middle (point B)
                --index;
            }

        }

        public static GJKCollisionInfo GJKCollisionCheckWithInfo(Vector2[] vertices1, Vector2[] vertices2, out int iter_count)
        {
            GJKCollisionInfo Result = new GJKCollisionInfo();
            iter_count = 0;
            uint index = 0; // index of current vertex of simplex
            Vector2 a, b, c, d, ao, ab, ac, abperp, acperp;
            Vector2[] simplex = new Vector2[3];


            Vector2 position1 = AveragePoint(vertices1); // not a CoG but
            Vector2 position2 = AveragePoint(vertices2); // it's ok for GJK )

            // initial direction from the center of 1st body to the center of 2nd body
            d = Vector2.Subtract(position1, position2);

            // if initial direction is zero – set it to any arbitrary axis (we choose X)
            if ((d.X == 0) && (d.Y == 0)) d.X = 1.0f;

            // set the first support as initial point of the new simplex
            a = simplex[0] = Support(vertices1, vertices2, d);

            if (Vector2.Dot(a, d) <= 0)
            {
                Result.collided = false;
                Result.simplex = simplex;
                return Result; // no collision
            }
            d = Vector2.Negate(a); // The next search direction is always towards the origin, so the next search direction is negate(a)

            while (true)
            {
                iter_count++;

                a = simplex[++index] = Support(vertices1, vertices2, d);

                if (Vector2.Dot(a, d) <= 0)
                {

                    Result.collided = false;
                    Result.simplex = simplex;
                    break;
                }
                ao = Vector2.Negate(a); // from point A to Origin is just negative A

                // simplex has 2 points (a line segment, not a triangle yet)
                if (index < 2)
                {
                    b = simplex[0];
                    ab = Vector2.Subtract(b, a); // from point A to B
                    d = TripleProduct(ab, ao, ab); // normal to AB towards Origin
                    if (d.LengthSquared() == 0)
                        d = new Vector2(ab.Y, -ab.X);
                    continue; // skip to next iteration
                }

                b = simplex[1];
                c = simplex[0];
                ab = Vector2.Subtract(b, a); // from point A to B
                ac = Vector2.Subtract(c, a); // from point A to C

                acperp = TripleProduct(ab, ac, ac);

                if (Vector2.Dot(acperp, ao) >= 0)
                {

                    d = acperp; // new direction is normal to AC towards Origin

                }
                else
                {

                    abperp = TripleProduct(ac, ab, ab);

                    if (Vector2.Dot(abperp, ao) < 0)
                    {

                        Result.collided = true;
                        Result.simplex = simplex;
                        break;
                    }

                    simplex[0] = simplex[1]; // swap first element (point C)

                    d = abperp; // new direction is normal to AB towards Origin
                }

                simplex[1] = simplex[2]; // swap element in the middle (point B)
                --index;
            }
            return Result;
        }

        static Edge FindClosestEdge(List<Vector2> s)
        {
            Edge closest = new Edge();
            // prime the distance of the edge to the max
            closest.distance = double.MaxValue;
            // s is the passed in simplex
            for (int i = 0; i < s.Count; i++)
            {
                // compute the next points index
                int j = i + 1 == s.Count ? 0 : i + 1;
                // get the current point and the next one
                Vector2 a = s[i];
                Vector2 b = s[j];
                // create the edge vector
                Vector2 e = b - (a); // or a.to(b);
                                        // get the vector from the origin to a
                Vector2 oa = a; // or a - ORIGIN
                                // get the vector from the edge towards the origin
                Vector2 n = TripleProduct(e, oa, e);
                // normalize the vector
                n = Vector2.Normalize(n);
                // calculate the distance from the origin to the edge
                double d = Vector2.Dot(n, a); // could use b or a here
                                                // check the distance against the other distances
                if (d < closest.distance)
                {
                    // if this edge is closer then use it
                    closest.distance = d;
                    closest.normal = n;
                    closest.index = j;
                }
            }
            // return the closest edge we found
            return closest;
        }

        public static GJKEPACollisonInfo GJKEPACollisionCheckWithInfo(Vector2[] vertices1, Vector2[] vertices2, out int iter_count)
        {
            GJKEPACollisonInfo Result = new GJKEPACollisonInfo();
            const double TOLERANCE = double.Epsilon;
            const int MaxIterations = 100;
            GJKCollisionInfo info = GJKCollisionCheckWithInfo(vertices1, vertices2, out iter_count);
            List<Vector2> simplex = new List<Vector2>(info.simplex);
            // loop to find the collision information
            Edge e = new Edge();
            Vector2 p = new Vector2();
            if (info.collided)
            {
                for (int i = 0; i < MaxIterations; i++)
                {
                    // obtain the feature (edge for 2D) closest to the 
                    // origin on the Minkowski Difference
                    e = FindClosestEdge(simplex);
                    // obtain a new support point in the direction of the edge normal
                    p = Support(vertices1, vertices2, e.normal);
                    // check the distance from the origin to the edge against the
                    // distance p is along e.normal
                    double d = Vector2.Dot(p, e.normal);
                    if (d - e.distance < TOLERANCE)
                    {
                        // the tolerance should be something positive close to zero (ex. 0.00001)

                        // if the difference is less than the tolerance then we can
                        // assume that we cannot expand the simplex any further and
                        // we have our solution
                        Result.closest.normal = -1 * e.normal;
                        Result.closest.distance = d;
                        Result.closest.index = e.index;
                        break;
                    }
                    else
                    {
                        // we haven't reached the edge of the Minkowski Difference
                        // so continue expanding by adding the new point to the simplex
                        // in between the points that made the closest edge
                        simplex.Insert(e.index, p);
                    }
                }
            }
            Result.closest.normal = -1 * e.normal;
            Result.closest.distance = Vector2.Dot(p, e.normal);
            Result.closest.index = e.index;
            Result.info.simplex = simplex.ToArray();
            Result.info = info;
            return Result;
        }
    }
}
