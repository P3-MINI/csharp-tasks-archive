using System;

namespace Lab05_pl
{
    public abstract class Shape2D
    {
        private static int _objectsCount = 0;
        protected readonly int ObjectNumber;

        protected Shape2D()
        {
            ObjectNumber = _objectsCount++;
            Console.WriteLine($"Shape2D ({ObjectNumber}) created");
        }

        ~Shape2D()
        {
            Console.WriteLine($"Shape2D ({ObjectNumber}) destroyed");
        }

        public abstract double CalculateArea();

        public virtual string PrintShape2D()
        {
            return "Shape(Shape2D)";
        }
    }

    public class Circle : Shape2D
    {
        private double _radius;

        public Circle(double radius) : base()
        {
            _radius = radius;
            Console.WriteLine($"Circle ({ObjectNumber}) with radius={_radius} created");
        }

        ~Circle()
        {
            Console.WriteLine($"Circle ({ObjectNumber}) destroyed");
        }

        public override double CalculateArea()
        {
            return Math.PI * _radius * _radius;
        }

        public override string PrintShape2D()
        {
            return $"Circle(r={_radius})";
        }
    }

    public abstract class Shape3D
    {
        protected Shape2D _baseShape;
        protected double _height;
        public readonly int ObjId;

        private static int _numberOfCreatedObjects;

        protected Shape3D(Shape2D baseShape, double height)
        {
            _baseShape = baseShape;
            _height = height;
            ObjId = _numberOfCreatedObjects++;
        }

        public abstract double CalculateCapacity();

        public virtual string PrintShape3D()
        {
            return $"Shape3D with base {_baseShape.PrintShape2D()} and height: {_height}";
        }

        public Shape3D MakeCopy()
        {
            switch (this)
            {
                case Cone:
                    return ((Cone) this).Copy();
                    break;
                case Cylinder:
                    return ((Cylinder) this).Copy();
                    break;
                default:
                    return null;
            }
        }

        public static double GetNumberOfCreatedObjects()
        {
            return _numberOfCreatedObjects;
        }
    }

    public class Cone : Shape3D
    {
        private static int _numberOfCreatedCones;

        public Cone(Circle baseShape, double height) : base(baseShape, height)
        {
            _numberOfCreatedCones++;
        }

        public override double CalculateCapacity()
        {
            return _baseShape.CalculateArea() * _height / 3;
        }

        public override string PrintShape3D()
        {
            return $"Cone(h={_height})(Id: {ObjId * 100}) with base: {_baseShape.PrintShape2D()}";
        }

        public Shape3D Copy()
        {
            return new Cone((Circle) _baseShape, _height);
        }
    }

    public class Cylinder : Shape3D
    {
        private static int _numberOfCreatedCylinders;
        public readonly int CylinderId;

        public Cylinder(Circle baseShape, double height) : base(baseShape, height)
        {
            CylinderId = _numberOfCreatedCylinders++;
        }

        public override double CalculateCapacity()
        {
            return _baseShape.CalculateArea() * _height;
        }

        public override string PrintShape3D()
        {
            return $"Cylinder(h={_height})(Id: {CylinderId}) with base: {_baseShape.PrintShape2D()} and height: {_height}";
        }

        public Cylinder Copy()
        {
            return new Cylinder((Circle) _baseShape, _height);
        }
    }
}