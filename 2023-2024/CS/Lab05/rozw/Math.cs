using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab05
{
    struct Vector2
    {
        private float X;
        private float Y; 

        public Vector2(float x, float y)
        {
            X = x; 
            Y = y;
        }

        public Vector2(float s)
        {
            X = Y = s;
        }

        // Akceptujemy metodę Add lub operator+/operator-
        public static Vector2 Add(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }

        //public static Vector2 operator+(Vector2 left, Vector2 right)
        //{
        //    return new Vector2(left.X + right.X, left.Y + right.Y);
        //}
        //public static Vector2 operator-(Vector2 left, Vector2 right)
        //{
        //    return new Vector2(left.X - right.X, left.Y - right.Y);
        //}

        public static Vector2 Min(Vector2 left, Vector2 right)
        {
            return new Vector2(Math.Min(left.X, right.X), Math.Min(left.Y, right.Y));
        }

        public static Vector2 Max(Vector2 left, Vector2 right)
        {
            return new Vector2(Math.Max(left.X, right.X), Math.Max(left.Y, right.Y));
        }

        public override string ToString()
        {
            return $"Point({X}, {Y})";
        }
    }

    struct BoundingBox
    {
        public Vector2 MinimumPoint;
        public Vector2 MaximumPoint;

        public BoundingBox(Vector2 minimumPoint, Vector2 maximumPoint)
        {
            MinimumPoint = minimumPoint;
            MaximumPoint = maximumPoint;
        }

        public override string ToString()
        {
            return $"BoundingBox: Minimum in {MinimumPoint}, Maximum in {MaximumPoint}";
        }

        public static BoundingBox MergeBoundingBoxes(BoundingBox left, BoundingBox right)
        {
            return new BoundingBox(Vector2.Min(left.MinimumPoint, right.MinimumPoint), Vector2.Max(left.MaximumPoint, right.MaximumPoint));
        }

    }
}
