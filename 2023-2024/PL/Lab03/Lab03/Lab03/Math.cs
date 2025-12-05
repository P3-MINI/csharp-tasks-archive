using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab03
{
    #region Geometry
    internal struct Point2
    {
        // Krótko opowiadamy o właściwościach, że jest to po prostu metoda dostępu do składowej.
        // Że można je implementować samemu (ale to na wykładzie i później na laboratorium).
        // A że te domyślne (auto-properties) robią pod spodem zmienną z _nazwaZmiennej.
        public float X { get; set; }
        public float Y { get; set; }

        public Point2(float x, float y)
        {
            this.X = x; this.Y = y;
        }

        // Zwracamy uwagę na pisanie operatorów - tylko wersja statyczna jest poprawna.
        public static Point2 operator+(Point2 p1, Point2 p2)
        {
            return new Point2(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static Point2 operator-(Point2 p1, Point2 p2)
        {
            return new Point2(p1.X - p2.X, p1.Y - p2.Y);
        }

        // Opowiadamy o ToString, co robi i co dzieje się jeśli go nie napiszemy.
        // Można go zakomentować i pokazać output z Main.
        public override string ToString()
        {
            return $"Point({X},{Y})";
        }
    }
    #endregion

    #region Figures
    internal class Figure
    {
        // Zwrócić uwagę na osobny modyfikator dostępu dla właściwości.
        // Pokazać, ze w drugą stronę nie działa (private Point2 Center { get; public set; }).

        public Point2 Center { get; protected set; }

        public Figure(Point2 center)
        {
            Center = center;
        }
    }

    // Zwrócić uwagę jak dziedziczymy i jak wywoływany jest wtedy konstruktor.
    internal class Circle : Figure
    {
        public float Radius { get; set; }

        public Circle(Point2 center, float radius) : base(center)
        {
            Radius = radius;
        }
    }

    internal class Rectangle : Figure
    {
        public float A { get; set; }
        public float B { get; set; }

        public Rectangle(Point2 center, float a, float b) : base(center)
        {
            A = a;
            B = b;
        }

        public void Scale(float sa, float sb)
        {
            A *= sa;
            B *= sb;
        }

        // Zwrócić uwagę, ze mamy dostęp do składowej z klasy bazowej.
        // Pokazać, że zmiana na private set psuje tę implementację.
        public void Move(Point2 sc)
        {
            Center += sc;
        }

        public override string ToString()
        {
            return $"Rectangle({Center}-{A},{B}";
        }
    }
    #endregion
}
