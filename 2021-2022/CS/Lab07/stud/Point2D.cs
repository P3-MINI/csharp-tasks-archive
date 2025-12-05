using System;

namespace EN_Lab_07
{
    public struct Point2D
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"Point2D({X:f3};{Y:f3})";
        }

        #region Operators

        public static Point2D operator -(Point2D p1,Point2D p2)
        {
            return new Point2D(p1.X-p2.X,p1.Y-p2.Y);
        }

        // ToDo
        // Define needed aritmetical operators

        public static bool operator ==(Point2D p1, Point2D p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Point2D p1, Point2D p2)
        {
            return !p1.Equals(p2);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Point2D))
                return false;
            var p = (Point2D)obj;
            return X == p.X && Y == p.Y;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        #endregion

        #region Conversions

        public static implicit operator Point2D((double, double) tup)
        {
            return new Point2D(tup.Item1, tup.Item2);
        }

        public static implicit operator (double, double)(Point2D p)
        {
            return (p.X, p.Y);
        }

        public static implicit operator double[](Point2D p)
        {
            return new double[] { p.X, p.Y };
        }

        public static explicit operator Point2D(double[] arr)
        {
            if (arr.Length == 2)
                return new Point2D(arr[0], arr[1]);
            return new Point2D(0, 0);
        }

        #endregion

        #region Properties

        public double Length
        {
            get
            {
                return Math.Sqrt(X * X + Y * Y);
            }
        }

        public static Point2D Zero { get; } = new Point2D(0, 0);

        public static Point2D UnitX { get; } = new Point2D(1, 0);

        public static Point2D UnitY { get; } = new Point2D(0, 1);

        public double this[int idx]
        {
            get
            {
                switch(idx)
                {
                    case 0: return X;
                    case 1: return Y;
                    default: throw new IndexOutOfRangeException();
                }
            }
            set
            {
                switch(idx)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        #endregion
    }
}