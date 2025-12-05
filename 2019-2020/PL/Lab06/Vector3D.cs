#define STAGE1
#define STAGE2
#define STAGE3

using System;

// Zaimplementuj typ Vector3D reprezentujący wektor w przestrzeni trójwymiarowej.
//
// Konstruktor ma przyjmować trzy parametry typu double x, y, z.
//
// Właściwości X, Y, Z zwracają poszczególne składowe wektora oraz pozwalają na ich modyfikację.
//
// Metoda Equals porównuje wektory po składowych, a metoda ToString zwraca stringa w postaci:
// "Vector3D(X = x, Y = y, Z = z)" gdzie x,y,z to wartości składowych wektora w domyślnym formacie doubla.
//
// Typ pozwala na standardowe operacje dodawania i odejmowania wektorów oraz mnożenia i dzielenia przez skalar.
//
// Typ wektor zawiera pięć statycznych pól Zero (wektor o 3 elementach zerowych), One (wektor o 3 elementach o wartości 1)
// oraz trzy wektory jednostkowe UnitX, UnitY, UnitZ.
//
// Właściwość Length zwraca długość wektora w metryce euklidesowej, a Length2 jej wartość w kwadracie.
//
// Funkcje Dot oraz Cross w wersji zarówno statycznej jak i instancyjnej metody zwracają odpowiednio 
// iloczyn skalarny oraz iloczyn wektorowy.
//
// Funkcja Normalize zwraca znormalizowaną wartość wektora, a Normalized normalizuje podany wektor 
// (obie funkcje w przypadku wektora zerowego zwracają wektor zerowy).
//
// Indeksator pozwala na pobranie wartości trzech składowych (dla wartości z poza zakresu indeksu [0-2]
// powinien zgłosić wyjątek IndexOutOfRangeException, wyjątki w C# zgłasza się tak samo jak w c++ np. throw new Exception()).
//
// Dekompozytor pozwala na dekompozycję wektora na krotkę trzech wartości typu double.
//
// Konwerter z niejawny trzyelementowej krotki na obiekt typu Vector3D oraz konwerter jawny na trzyelementową krotkę.
//
// Typ Vector3D posiada dwie fabryki MakeUnit (zwraca jednostkowy wektor w kierunku podanym w parametrach) 
// oraz LERP (zwraca liniową interpolację dwóch wektorów).
//
// Funkcja AlmostEquals sprawdza czy dwa wektory są w zadanej odległości pomiędzy sobą, a AlmostZero w odległości od Zera.
// Obie funkcje pozwalają na wybranie tolerancji z jaką działają - domyślnie jest ona równa 1e-7.
//
// Funkcja IsNaN sprawdza czy co najmniej jedna składowa wektora nie jest liczbą.

// Etap 1: (1pkt)
//         konstruktor, właściwości X, Y, Z, operator dodawania, operator porównania, metoda ToString
//         oraz inne niezbędne metody (kompilator nie może generować żadnych ostrzeżeń).
// Etap 2: (2pkt)
//         operator mnożenia oraz dzielenia skalarnego, operator odejmowania, operator unarny,
//         statyczne pola klasy Zero, One, UnitX, UnitY, UnitZ,
//         właściwość Length oraz Length2
//         funkcje Dot i Cross
// Etap 3: (2pkt)
//         funkcje Normalize/Normalized, indeksator, dekompozytor, fabryka MakeUnit oraz LERP,
//         konwertery, funkcje AlmostEquals, AlmostZero, IsNaN


namespace Vector3D
{
    struct Vector3D
    {
#if STAGE1
        private double x;
        private double y;
        private double z;

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public double Z
        {
            get { return z; }
            set { z = value; }
        }

        //konstruktor
        public Vector3D(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        //basic op
        public static Vector3D operator +(Vector3D A, Vector3D B)
        {
            return new Vector3D(A.x + B.x, A.y + B.y, A.z + B.z);
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
            return base.GetHashCode();
            //return this.x.GetHashCode() ^ this.y.GetHashCode() ^ this.z.GetHashCode();
        }

#endif

#if STAGE2
        public double Length2
        {
            get { return this.x * this.x + this.y * this.y + this.z * this.z; }
        }

        public double Length
        {
            get
            {
                return Math.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
            }
        }

        //statyczne obiekty klasy
        public static readonly Vector3D Zero = new Vector3D(0.0, 0.0, 0.0);
        public static readonly Vector3D One = new Vector3D(1.0, 1.0, 1.0);
        public static readonly Vector3D UnitX = new Vector3D(1.0, 0.0, 0.0);
        public static readonly Vector3D UnitY = new Vector3D(0.0, 1.0, 0.0);
        public static readonly Vector3D UnitZ = new Vector3D(0.0, 0.0, 1.0);

        public static Vector3D operator -(Vector3D A, Vector3D B)
        {
            return new Vector3D(A.x - B.x, A.y - B.y, A.z - B.z);
        }

        public static Vector3D operator -(Vector3D A)
        {
            return new Vector3D(-A.x, -A.y, -A.z); // or return Zero - A;
        }

        //scalar mulitplication
        public static Vector3D operator *(double s, Vector3D A)
        {
            return new Vector3D(s * A.x, s * A.y, s * A.z);
        }

        public static Vector3D operator *(Vector3D A, double s)
        {
            return s * A;
        }

        public static Vector3D operator /(Vector3D A, double s)
        {
            return new Vector3D(A.x / s, A.y / s, A.z / s);
        }

        //Dot product
        public static double Dot(Vector3D A, Vector3D B)
        {
            return A.x * B.x + A.y * B.y + A.z * B.z;
        }

        public double Dot(Vector3D B)
        {
            return Dot(this, B); // or return this.x * B.x + this.y * B.y + this.z * B.z;
        }

#endif

#if STAGE3
        // metody "factory"
        public static Vector3D MakeUnit(double x, double y, double z)
        {
            return new Vector3D(x, y, z).Normalized();
        }

        public static Vector3D LERP(Vector3D A, Vector3D B, double t = 0.5)
        {
            return (A * t) + (B * (1.0 - t));
        }

        //dekonstrukcja wektora
        public void Deconstruct(out double x, out double y, out double z)
        {
            x = this.x; y = this.y; z = this.z;
        }

        public static implicit operator Vector3D((double x, double y, double z) v)
        {
            return new Vector3D(v.x, v.y, v.z);
        }

        public static explicit operator (double, double, double) (Vector3D v)
        {
            return (v.x, v.y, v.z);
        }

        //indeksator
        public double this[int i]
        {
            set
            {
                if (i == 0) x = value;
                else if (i == 1) y = value;
                else if (i == 2) z = value;
                else throw new IndexOutOfRangeException();
            }
            get
            {
                if (i == 0) return x;
                if (i == 1) return y;
                if (i == 2) return z;
                throw new IndexOutOfRangeException();
            }
        }

        public Vector3D Normalized()
        {
            var len = this.Length;
            if (len == 0)
                return new Vector3D(0, 0, 0);
            return new Vector3D(this.x / len, this.y / len, this.z / len);
        }

        public void Normalize()
        {
            var len = this.Length;
            if (len != 0)
            {
                this.x /= len;
                this.y /= len;
                this.z /= len;
            }
        }

        public bool AlmostEquals(Vector3D A, double tolerance = 1e-7)
        {
            //return (A - this).Length2 < tolerance * tolerance;
            return Math.Abs(x - A.x) < tolerance && Math.Abs(y - A.y) < tolerance && Math.Abs(z - A.z) < tolerance;
        }

        public bool AlmostZero(double tolerance = 1e-7)
        {
            //return (Zero - this).Length2 < tolerance * tolerance;
            return Math.Abs(x) < tolerance && Math.Abs(y) < tolerance && Math.Abs(z) < tolerance;
        }

        public bool IsNaN()
        {
            return double.IsNaN(x) || double.IsNaN(y) || double.IsNaN(z);
        }
        public static bool IsNaN(Vector3D A)
        {
            return A.IsNaN();
        }
#endif
    }
}
