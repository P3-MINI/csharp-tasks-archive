using System.Numerics;

namespace Lab15Retake
{
    internal struct Vector3D<T> where T : INumber<T>
    {
        public T X { get; private set; }
        public T Y { get; private set; }
        public T Z { get; private set; }

        public Vector3D(T x, T y, T z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public static Vector3D<T> operator+(Vector3D<T> lhs, Vector3D<T> rhs)
        {
            return new Vector3D<T>(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);
        }

        public static Vector3D<T> operator -(Vector3D<T> lhs, Vector3D<T> rhs)
        {
            return new Vector3D<T>(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);
        }

        public static Vector3D<T> operator *(Vector3D<T> lhs, Vector3D<T> rhs)
        {
            return new Vector3D<T>(lhs.X * rhs.X, lhs.Y * rhs.Y, lhs.Z * rhs.Z);
        }

        public static Vector3D<T> operator /(Vector3D<T> lhs, Vector3D<T> rhs)
        {
            return  new Vector3D<T>(lhs.X / rhs.X, lhs.Y / rhs.Y, lhs.Z / rhs.Z);
        }

        public override string ToString()
        {
            return $"[{X:0.00};{Y:0.00};{Z:0.00}]";
        }
    }
}
