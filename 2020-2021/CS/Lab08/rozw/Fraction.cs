#define STAGE1
#define STAGE2
#define STAGE3

using System;

namespace Lab8
{
    public struct Fraction
    {
#if STAGE1
        private long numerator;
        private long denominator;

        public long Numerator
        {
            get
            {
                return numerator;
            }
            set
            {
                numerator = value;
                Simplify();
            }
        }

        public long Denominator
        {
            get
            {
                return denominator;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentException();
                denominator = value;
                Simplify();
            }
        }

        public Fraction(long numerator, long denominator = 1)
        {
            this.numerator = numerator;
            if (denominator <= 0)
                throw new ArgumentException();
            this.denominator = denominator;

            Simplify();
        }

        public override string ToString()
        {
            var whole = numerator / denominator;
            var num = numerator - whole * denominator;
            var sign = numerator > 0;

            var str = string.Empty;
            if (!sign)
            {
                str += "-";
                num = -num;
                whole = -whole;
            }
            if (num == 0)
                str += $"[{whole}]";
            else if (whole != 0)
                str += $"[{whole} {num}/{denominator}]";
            else
                str += $"[{num}/{denominator}]";

            return str;
        }

        private void Simplify()
        {
            var gcd = GCD(Math.Abs(numerator), denominator);

            numerator = numerator / gcd;
            denominator = denominator / gcd;
        }

        private static long GCD(long a, long b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }
            return a == 0 ? b : a;
        }

        public Fraction Reciprocal()
        {
            return numerator > 0 ? new Fraction(denominator, numerator) : new Fraction(-denominator, -numerator) ;
        }

#endif
#if STAGE2

        public static implicit operator Fraction(long l)
        {
            return new Fraction(l);
        }

        public static explicit operator double(Fraction f)
        {
            return (double)f.numerator / f.denominator;
        }

        public static explicit operator long(Fraction f)
        {
            return f.numerator / f.denominator;
        }

        public static Fraction operator +(Fraction f1, Fraction f2)
        {
            var gcd = GCD(f1.Denominator, f2.Denominator);

            var n = f1.Numerator * (f2.Denominator / gcd) + f2.Numerator * (f1.Denominator / gcd);
            var d = f1.Denominator * (f2.Denominator / gcd);

            return new Fraction(n, d);
        }

        public static Fraction operator -(Fraction f)
        {
            return new Fraction(-f.Numerator, f.Denominator);
        }

        public static Fraction operator -(Fraction f1, Fraction f2)
        {
            return f1 + -f2;
        }

        public static Fraction operator *(Fraction f1, Fraction f2)
        {
            var gcd1 = GCD(f1.numerator, f2.denominator);
            var gcd2 = GCD(f2.numerator, f1.denominator);

            return new Fraction((f1.numerator / gcd1) * (f2.numerator / gcd2), (f1.denominator / gcd2) * (f2.denominator / gcd1));
        }

        public static Fraction operator /(Fraction f1, Fraction f2)
        {
            return f1 * f2.Reciprocal();
        }

#endif
#if STAGE3

        public static bool operator >(Fraction f1, Fraction f2)
        {
            return (f1 - f2).numerator > 0;
        }
        public static bool operator <(Fraction f1, Fraction f2)
        {
            return f2 > f1;
        }

        public static bool operator >=(Fraction f1, Fraction f2)
        {
            return !(f1 < f2);
        }

        public static bool operator <=(Fraction f1, Fraction f2)
        {
            return !(f1 > f2);
        }

        public static bool operator ==(Fraction f1, Fraction f2)
        {
            return f1.Equals(f2);
        }

        public static bool operator !=(Fraction f1, Fraction f2)
        {
            return !f1.Equals(f2);
        }

        public override int GetHashCode()
        {
            return (int)(numerator ^ denominator);
        }

        public override bool Equals(object obj)
        {
            return obj is Fraction f2 && numerator == f2.numerator && denominator == f2.denominator;
        }

#endif
    }
}
