using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab05
{
    struct Vector3
    {
        private float X;
        private float Y; 
        private float Z;

        public Vector3(float x, float y, float z)
        {
            X = x; Y = y; Z = z;
        }

        public Vector3(float s)
        {
            X = Y = Z = s;
        }

        // Akceptujemy metodę Add lub operator+/operator-
        public static Vector3 Add(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        //public static Vector3 operator+(Vector3 left, Vector3 right)
        //{
        //    return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        //}
        //public static Vector3 operator-(Vector3 left, Vector3 right)
        //{
        //    return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        //}

        public static Vector3 Min(Vector3 left, Vector3 right)
        {
            return new Vector3(Math.Min(left.X, right.X), Math.Min(left.Y, right.Y), Math.Min(left.Z, right.Z));
        }

        public static Vector3 Max(Vector3 left, Vector3 right)
        {
            return new Vector3(Math.Max(left.X, right.X), Math.Max(left.Y, right.Y), Math.Max(left.Z, right.Z));
        }

        public override string ToString()
        {
            return $"Point({X}, {Y}, {Z})";
        }
    }

    struct BoundingBox
    {
        public Vector3 MinimumPoint;
        public Vector3 MaximumPoint;

        public BoundingBox(Vector3 minimumPoint, Vector3 maximumPoint)
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
            return new BoundingBox(Vector3.Min(left.MinimumPoint, right.MinimumPoint), Vector3.Max(left.MaximumPoint, right.MaximumPoint));
        }

    }
}
