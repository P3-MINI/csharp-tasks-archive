using System;
using System.Text;

namespace POL_Lab05
{
    /*
        Etap_1 (0.5 Pkt): stwórz strukturę 'Point' reprezentującą punkt w przestrzeni R^2 (powinna zawierać dwie składowe typu double).
            * Zaimplementuj publiczny konstruktor przyjmujący odpowiednie argumenty. Zainicjalizuj odpowiednie pola struktury.
            * Zaimplementuj publiczną metodę 'Length' obliczającą długość promienia wodzącego punktu.
            * Zaimplementuj publiczną metodę 'ToString' odziedziczoną po klasie 'Object' wypisującą punkt w postaci '[X,Y]'.
    */

    public struct Point
    {
        public double X;
        public double Y;

        public Point(double _x, double _y)
        {
            this.X = _x;
            this.Y = _y;
        }

        public double Length()
        {
            return Math.Sqrt(this.X * this.X + this.Y * this.Y);
        }

        public override string ToString()
        {
            return $"[{this.X},{this.Y}]";
        }
    }

    /*
        Etap_2 (1.0 pkt): Stwórz abstrakcyjną klasę 'Figure' reprezentującą figurę geometryczną w przestrzeni R^2.
            * Powinna zawierać dostępną dla klas pochodnych tablicę punktów.
            * Zaimplementuj publiczny konstruktor przyjmujący zmienną liczbę punktów.
                Zainicjalizuj posiadaną tablicę oraz zapisz przekazane w argumentach punkty.
            * Zaimplementuj publiczną metodę 'ComputeCenter' obliczającą środek figury.
            * Zaimplementuj publiczną wirtualną metodę 'Description' zwracającą opis figury w positaci 'Points: [x1,y1] [x2,y2] ...'.
                Skorzystać z już zaimplementowanej metody 'ToString' klasy 'Point'.
                Skorzystaj z klasy 'StringBuilder' aby zoptymalizować operacje na napisach.
                     - Klasa 'StringBuilder' reprezentuje modyfikowalny ciąg znaków podobny do typu 'String'.
                     - Dodanie nowych napisów do instancji klasy 'StringBuilder' odbywa się za pomocą metody 'Append'.
                     - Wydobycie wartości typu 'String' odbywa się za pomocą metody 'ToString'.
                    Uwaga: Możesz użyć zwykłych operacji na klasie 'String', lecz nie jest to rekomendowane ze względu na wydajność aplikacji.
            * Stwórz publiczną abstrakcyjną metodę 'Area' obliczającą powierzchnię figury.
    */
    public abstract class Figure
    {
        protected Point[] Points;

        public Figure(params Point[] _points)
        {
            this.Points = new Point[_points.Length];

            for (int i = 0; i < this.Points.Length; i++)
                this.Points[i] = _points[i];
        }

        public Point ComputeCenter()
        {
            Point resultPoint = new Point(0, 0);

            /* Studenci tutaj używają pętli for, ponieważ foreach jeszcze nie było. */
            foreach (Point point in this.Points)
            {
                resultPoint.X += point.X;
                resultPoint.Y += point.Y;
            }

            resultPoint.X /= this.Points.Length;
            resultPoint.Y /= this.Points.Length;

            return resultPoint;
        }

        public abstract double Area();

        public virtual string Description()
        {
            /* Jeśli studenci mieliby pytania można powiedzieć, żeby przeczytali dokumentację lub opowiedzieć podstawy na tablicy. */

            /* Akceptowalne mogą być użycia klasy String, ponieważ StringBuilder jeszcze nie było. */
            StringBuilder stringBuilder = new StringBuilder("Points:");

            /* Studenci tutaj używają pętli for, ponieważ foreach jeszcze nie było. */
            foreach (Point point in this.Points)
                stringBuilder.Append($" {point}");

            return stringBuilder.ToString();
        }
    }

    /*
        Etap_3 (2.0 pkt): Stwórz klasy 'Circle' oraz 'Rectangle' dziedziczące po klasie 'Figure'.
            *  Klasa 'Circle' (1.0 pkt):
                * Powinna zawierać prywatne pole typu double reprezentujące promień.
                * Zaimplementuj publiczny konstruktor przyjmujący punkt reprezentujący środek koła oraz promień.
                    Wywołaj odpowiednio konstruktor klasy bazowej przekazując otrzymany w argumentach środek.
                    Zainicjalizuj promień przekazaną w argumentach wartością.
                * Zaimplementuj publiczną metodę 'MaxPoint' zwracającą odległość najdalszego od środka układy współrzędnych punktu koła.
                    Zwróć sumę długości promienia wodzącego środka koła oraz promienia.
                * Zaimplementuj metodę dokonującą dekonstrukcji (C# 7.0) koła na jego środek (typu Point) i promień.
                * Zaimplementuj publiczną metodę 'Area' odziedziczoną z klasy bazowej.
                * Zaimplementuj publiczną metodę 'Description' opisującą koło w następujący sposób "Circle with center at: 'center' and radius: 'radius'".
            * Klasa 'Rectangle' (1.0 pkt):
                * Nie powinna zawierać żadnych składowych.
                * Zaimplementuj publiczny konstruktor przyjmujący krotkę (C# 7.0) zawierającą dwa naprzeciwległe punkty (NW i SE) prostokąta.
                    Wywołaj odpowiednio konstruktor klasy bazowej przekazując otrzymane w argumentach punkty.
                * Zaimplementuj metodę dokonującą dekonstrukcji (C# 7.0) prostokąta na definiujące go dwa punkty.
                * Zaimplementuj publiczną metodę 'Area' odziedziczoną z klasy bazowej.
                    Długości boków oblicz korzystając z odpowiednich punktów (brakujące punkty oblicz zakładając, że boki prostokąta są równoległe do osi układu współrzędnych).
                * Zaimplementuj publiczną metodę 'Description' opisującą prostokąt w następujący sposób "Rectangle with" dopisując punkty wywołując metodę klasy bazowej.
    */
    public class Circle : Figure
    {
        private double Radius;

        public Circle(Point _center, double _radius) : base(_center)
        {
            this.Radius = _radius;
        }

        public void Deconstruct(out Point center, out double radius)
        {
            (center, radius) = (base.Points[0], this.Radius);
        }

        public double MaxPoint()
        {
            Point center = base.Points[0];

            return (center.Length() + this.Radius);
        }

        public override double Area()
        {
            return (Math.PI * this.Radius * this.Radius);
        }

        public override string Description()
        {
            Point center = base.Points[0];

            return $"Circle With Center At: [{center.X},{center.Y}] And Radius: {this.Radius}";
        }
    }

    public class Rectangle : Figure
    {
        public Rectangle((Point, Point) points) : base(points.Item1, points.Item2) { }

        public void Deconstruct(out Point point1, out Point point2)
        {
            (point1, point2) = (base.Points[0], base.Points[1]);
        }

        public override double Area()
        {
            Point point1 = base.Points[0];
            Point point2 = base.Points[1];

            Point point3 = new Point(point1.X, point2.Y);
            Point point4 = new Point(point2.X, point1.Y);

            Point aSide = new Point(point1.X - point4.X, point1.Y - point4.Y);
            Point bSide = new Point(point2.X - point4.X, point2.Y - point4.Y);

            return (aSide.Length() * bSide.Length());
        }

        public override string Description()
        {
            return ("Rectangle With " + base.Description());
        }
    }

    /*
        Etap_4 (1.5 pkt): Stwórz klasę 'EuclidianSpace'. Za każdy poniższy punkt (metodę lub konstruktor) otrzymasz 0.5 pkt.
            * (0.5 pkt) Powinna zawierać dwie prywatne tablice reprezentujące odpowiednio punkty ('Points') oraz figury geometryczne ('Figure').
                Zaimplementuj publiczny konstruktor przyjmujący tablicę punktów oraz zmienną liczbę figur geometrycznych. Zainicjalizuj tablice przekazanymi w argumentach wartościami.
            * (0.5 pkt) Zaimplementuj publiczną metodę 'CreateCircle' zwracającą koło o środku w środku układu współrzędnych oraz promieniu równym sumie długości najdalszych punktów wszystkich kół.
                Wykorzystaj pętlę for oraz operatory sprawdzania typu i rzutowania.
            * (0.5 pkt) Zaimplementuj publiczną metodę 'GetTheFurthestRectangles' zwracającą krotkę (C# 7.0) zawierającą dwa prostokąty znajdujące się w największej odległości od siebie nawzajem. 
                Metoda powinna zwracać odległość pomiędzy prostokątami jako parametr wyjściowy. Wykorzystaj operatory sprawdzania typu i rzutowania.
                Uwaga: Jako odległość prostokątów rozumiemy odległość między ich środkami.
    */
    public class EuclidianSpace
    {
        private Point[] Points;
        private Figure[] Figures;

        public EuclidianSpace(Point[] _points, params Figure[] _figures)
        {
            this.Points = new Point[_points.Length];

            for (int i = 0; i < this.Points.Length; i++)
                this.Points[i] = _points[i];

            this.Figures = new Figure[_figures.Length];

            for (int i = 0; i < this.Figures.Length; i++)
                this.Figures[i] = _figures[i];
        }

        public Circle CreateCircle()
        {
            double resultRadius = 0.0;

            /* Studenci tutaj używają pętli for, ponieważ foreach jeszcze nie było. */
            foreach (Figure figure in this.Figures)
            {
                if (figure is Circle)
                {
                    resultRadius += (figure as Circle).MaxPoint();
                }
            }

            return new Circle(new Point(0, 0), resultRadius);
        }

        public (Rectangle, Rectangle) GetTheFurthestRectangles(out double _distance)
        {
            _distance = 0.0;

            Rectangle rectangle1 = null;
            Rectangle rectangle2 = null;

            for (int i = 0; i < this.Figures.Length; i++)
                for (int k = i + 1; k < this.Figures.Length; k++)
                {
                    if (this.Figures[i] is Rectangle && this.Figures[k] is Rectangle)
                    {
                        Point rectangleCenter1 = (this.Figures[i] as Rectangle).ComputeCenter();
                        Point rectangleCenter2 = (this.Figures[k] as Rectangle).ComputeCenter();

                        Point rectangleVector = new Point(rectangleCenter1.X - rectangleCenter2.X, rectangleCenter1.Y - rectangleCenter2.Y);

                        double currentDistance = rectangleVector.Length();

                        if (currentDistance > _distance)
                        {
                            _distance = currentDistance;

                            rectangle1 = (this.Figures[i] as Rectangle);
                            rectangle2 = (this.Figures[k] as Rectangle);
                        }
                    }
                }

            return (rectangle1, rectangle2);
        }
    }
}
