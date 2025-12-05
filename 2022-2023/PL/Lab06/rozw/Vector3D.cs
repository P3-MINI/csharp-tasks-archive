#define STAGE1
#define STAGE2
#define STAGE3
#define STAGE4

using System;

namespace Vector3D
{
    struct Vector3D
    {
#if STAGE1
        private double valueX;
        private double valueY;
        private double valueZ;

        public double X
        {
            get { return valueX; }
            set { valueX = value; }
        }

        public double Y
        {
            get { return valueY; }
            set { valueY = value; }
        }

        public double Z
        {
            get { return valueZ; }
            set { valueZ = value; }
        }

        public Vector3D(double x, double y, double z)
        {
            this.valueX = x;
            this.valueY = y;
            this.valueZ = z;
        }

        public bool Equals(Vector3D A)
        {
            return X == A.X && Y == A.Y && Z == A.Z;
        }

        public override string ToString()
        {
            return $"Vector3D(X = {X}, Y = {Y}, Z = {Z})";
        }

        public override bool Equals(object obj)
        {
            return obj is Vector3D B && Equals(B);
        }

        public static bool operator ==(Vector3D A, Vector3D B)
        {
            return A.Equals(B);
        }

        public static bool operator !=(Vector3D A, Vector3D B)
        {
            return !A.Equals(B);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode(); /* return this.x.GetHashCode() ^ this.y.GetHashCode() ^ this.z.GetHashCode(); */
        }

#endif

#if STAGE2

        //statyczne obiekty klasy
        public static readonly Vector3D Zero = new Vector3D(0.0, 0.0, 0.0);
        public static readonly Vector3D One = new Vector3D(1.0, 1.0, 1.0);
        public static readonly Vector3D UnitX = new Vector3D(1.0, 0.0, 0.0);
        public static readonly Vector3D UnitY = new Vector3D(0.0, 1.0, 0.0);
        public static readonly Vector3D UnitZ = new Vector3D(0.0, 0.0, 1.0);

        public double Length
        {
            get
            {
                return Math.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z); /* return Math.Sqrt(this.Length2) */
            }
        }

        //Dot product
        public static double Dot(Vector3D A, Vector3D B)
        {
            return A.X * B.X + A.Y * B.Y + A.Z * B.Z;
        }
#endif

#if STAGE3
        //dekonstrukcja wektora
        public void Deconstruct(out double x, out double y, out double z)
        {
            x = this.X; y = this.Y; z = this.Z;
        }

        public static implicit operator Vector3D((double x, double y, double z) vectorTuple) => new Vector3D(vectorTuple.x, vectorTuple.y, vectorTuple.z);

        //indeksator
        public double this[int index] /* Note: Cannot Use Indexers Within X,Y,Z Properties Since That Would Cause StackOverflowException */
        {
            set
            {
                switch (index)
                {
                    case 0: X = value; break;
                    case 1: Y = value; break;
                    case 2: Z = value; break;
                    default: throw new IndexOutOfRangeException();
                }
            }

            get
            {
                switch (index)
                {
                    case 0: return X;
                    case 1: return Y;
                    case 2: return Z;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }
#endif

#if STAGE4
        public static Vector3D MakeUnit(double x, double y, double z)
        {
            return new Vector3D(x, y, z).Normalized();
        }

        public Vector3D Normalized()
        {
            var length = this.Length;

            if (length == 0)
                return new Vector3D(0, 0, 0);

            return new Vector3D(this.X / length, this.Y / length, this.Z / length);
        }

        public bool AlmostEquals(Vector3D A, double epsilon = 1e-7)
        {
            return Math.Abs(X - A.X) < epsilon && Math.Abs(Y - A.Y) < epsilon && Math.Abs(Z - A.Z) < epsilon; /* return (A - this).Length2 < tolerance * tolerance; */
        }

        public bool IsNaN()
        {
            return double.IsNaN(X) || double.IsNaN(Y) || double.IsNaN(Z);
        }
#endif
    }
}
